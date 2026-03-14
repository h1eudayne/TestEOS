using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using IRemote;
using QuestionLib;

// Namespace MUST be "ExamClient" to match AuthenticateForm line 195:
//   Type fType = a.GetType("ExamClient.frmEnglishExamClient");
namespace ExamClient
{
    /// <summary>
    /// Pixel-perfect replica of the original EOS exam client form.
    /// Matches the exact layout from the original EOS application.
    /// </summary>
    public class frmEnglishExamClient : Form, IExamclient
    {
        // ===================== DATA =====================
        private EOSData examData;
        private Timer countdownTimer;
        private int timeLeftSeconds = 3600;
        private int currentQuestion = 0;
        private int totalQuestions = 60;
        private int totalMarks = 60;
        private string[] questionTexts;
        private string[][] answerOptions;
        private int[] selectedAnswers;
        private string[] correctAnswers; // A, B, C, or D
        private List<Question> loadedQuestions; // Questions loaded via ExamHelper

        // ===================== HEADER ROW 1 =====================
        private CheckBox chkFinishExam;
        private Button btnFinish;
        private Panel pnlCam1;
        private Panel pnlCam2;
        private Panel pnlCam3;
        private Panel pnlCam4;

        // ===================== HEADER ROW 2 =====================
        private Label lblMachineLabel;
        private Label lblMachineValue;
        private Label lblStudentLabel;
        private Label lblStudentValue;

        // ===================== HEADER ROW 3 =====================
        private Label lblServerLabel;
        private Label lblServerValue;
        private Label lblExamCodeLabel;
        private Label lblExamCodeValue;

        // ===================== HEADER ROW 4 =====================
        private Label lblDurationLabel;
        private Label lblDurationValue;
        private Label lblOpenCodeLabel;
        private TextBox txtOpenCode;
        private Button btnShareQuestion;

        // ===================== HEADER ROW 5 =====================
        private Label lblQMarkLabel;
        private Label lblQMarkValue;
        private Label lblTotalMarksLabel;
        private Label lblTotalMarksValue;
        private Label lblVolLabel;
        private NumericUpDown nudVol;

        // ===================== HEADER ROW 6 =====================
        private Label lblFontLabel;
        private ComboBox cboFont;
        private Label lblSizeLabel;
        private NumericUpDown nudSize;
        private Label lblTimeLeftLabel;
        private Label lblTimeLeftValue;

        // ===================== HEADER PANEL =====================
        private Panel pnlHeader;

        // ===================== TAB =====================
        private TabControl tabExam;
        private TabPage tabMultipleChoices;

        // ===================== CONTENT (inside tab) =====================
        private Label lblSectionTitle;      // "Multiple choices 1/60"
        private Label lblAnswerHeader;      // "Answer" (red)
        private CheckBox chkA;
        private CheckBox chkB;
        private CheckBox chkC;
        private CheckBox chkD;
        private Panel pnlSeparator;        // vertical line
        private Panel pnlQuestionArea;      // right side panel
        private Label lblInstruction;       // "(Choose 1 answer)"
        private Label lblQuestionBody;      // question text
        private Label lblOptA;
        private Label lblOptB;
        private Label lblOptC;
        private Label lblOptD;

        // ===================== BOTTOM =====================
        private Button btnNext;

        // ===================== CONSTRUCTOR =====================
        public frmEnglishExamClient()
        {
            BuildUI();
            LoadMockQuestions();
            DisplayQuestion(0);
        }

        // ===================== IExamclient =====================
        public void SetExamData(EOSData ed)
        {
            this.examData = ed;

            string login = (ed.RegData != null) ? ed.RegData.Login : "N/A";
            string machine = (ed.RegData != null) ? ed.RegData.Machine : "N/A";
            string examCode = (ed.RegData != null) ? ed.RegData.ExamCode : "N/A";

            lblMachineValue.Text = machine;
            lblStudentValue.Text = login;
            lblExamCodeValue.Text = examCode;

            if (ed.ExamPaper != null)
            {
                timeLeftSeconds = ed.ExamPaper.Duration * 60;
                lblDurationValue.Text = ed.ExamPaper.Duration + " minutes";
                totalMarks = ed.ExamPaper.NoOfQuestion;
                lblTotalMarksValue.Text = totalMarks.ToString();
                totalQuestions = ed.ExamPaper.NoOfQuestion;
                BuildQuestionsFromPaper(ed.ExamPaper);
            }
            else
            {
                timeLeftSeconds = 60 * 60;
                lblDurationValue.Text = "60 minutes";
            }

            if (ed.ServerInfomation != null)
            {
                string srvName = ed.ServerInfomation.IP;
                if (ed.ServerInfomation.Port > 0)
                    srvName += ":" + ed.ServerInfomation.Port;
                lblServerValue.Text = srvName;
            }

            selectedAnswers = new int[totalQuestions];
            for (int i = 0; i < totalQuestions; i++) selectedAnswers[i] = -1;

            currentQuestion = 0;
            DisplayQuestion(0);
            UpdateTimer();
            countdownTimer.Start();
        }

        // ===================== QUESTION DATA =====================
        private void BuildQuestionsFromPaper(Paper paper)
        {
            List<string> qt = new List<string>();
            List<string[]> ao = new List<string[]>();
            ArrayList[] sections = new ArrayList[] {
                paper.ReadingQuestions, paper.GrammarQuestions,
                paper.MatchQuestions, paper.IndicateMQuestions, paper.FillBlankQuestions
            };
            foreach (ArrayList sec in sections)
            {
                if (sec != null)
                {
                    for (int i = 0; i < sec.Count; i++)
                    {
                        qt.Add("Question " + (qt.Count + 1));
                        ao.Add(new string[] { "A.", "B.", "C.", "D." });
                    }
                }
            }
            if (qt.Count > 0)
            {
                totalQuestions = qt.Count;
                questionTexts = qt.ToArray();
                answerOptions = ao.ToArray();
            }
        }

        private void LoadMockQuestions()
        {
            // Try to load from CSV file first using ExamHelper
            string csvPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "questions.csv");
            if (File.Exists(csvPath))
            {
                loadedQuestions = ExamHelper.LoadQuestionsFromCSV(csvPath);
            }
            else
            {
                // Fallback: use default mock questions from ExamHelper
                loadedQuestions = ExamHelper.GetDefaultMockQuestions();
            }

            ApplyQuestionsToUI(loadedQuestions);
        }

        /// <summary>
        /// Load questions from a CSV file using ExamHelper.
        /// Kept public for backward compatibility.
        /// </summary>
        public void LoadQuestionsFromCSV(string csvPath)
        {
            loadedQuestions = ExamHelper.LoadQuestionsFromCSV(csvPath);
            ApplyQuestionsToUI(loadedQuestions);
        }

        /// <summary>
        /// Apply a list of Question objects to the form's internal arrays for UI display.
        /// </summary>
        private void ApplyQuestionsToUI(List<Question> questions)
        {
            if (questions == null || questions.Count == 0)
            {
                totalQuestions = 1;
                questionTexts = new string[] { "No questions loaded." };
                answerOptions = new string[][] { new string[] { "A. N/A", "B. N/A", "C. N/A", "D. N/A" } };
                correctAnswers = new string[] { "A" };
            }
            else
            {
                totalQuestions = questions.Count;
                questionTexts = new string[totalQuestions];
                answerOptions = new string[totalQuestions][];
                correctAnswers = new string[totalQuestions];

                for (int i = 0; i < totalQuestions; i++)
                {
                    questionTexts[i] = questions[i].Text;
                    answerOptions[i] = questions[i].GetOptions();
                    correctAnswers[i] = questions[i].CorrectAnswer;
                }
            }

            selectedAnswers = new int[totalQuestions];
            for (int i = 0; i < totalQuestions; i++) selectedAnswers[i] = -1;

            totalMarks = totalQuestions;
            if (lblTotalMarksValue != null)
                lblTotalMarksValue.Text = totalMarks.ToString();
        }

        private void DisplayQuestion(int idx)
        {
            if (idx < 0 || idx >= totalQuestions) return;
            currentQuestion = idx;

            lblSectionTitle.Text = "Multiple choices 1/60";
            if (totalQuestions > 0)
            {
                 lblSectionTitle.Text = "Multiple choices " + (idx + 1) + "/" + totalQuestions;
            }
            lblQMarkValue.Text = (idx + 1).ToString();

            lblQuestionBody.Text = questionTexts[idx];
            lblOptA.Text = answerOptions[idx][0];
            lblOptB.Text = answerOptions[idx][1];
            lblOptC.Text = answerOptions[idx][2];
            lblOptD.Text = answerOptions[idx][3];

            // Restore previously selected answer without firing events
            chkA.CheckedChanged -= OnAnswerCheck;
            chkB.CheckedChanged -= OnAnswerCheck;
            chkC.CheckedChanged -= OnAnswerCheck;
            chkD.CheckedChanged -= OnAnswerCheck;

            chkA.Checked = (selectedAnswers[idx] == 0);
            chkB.Checked = (selectedAnswers[idx] == 1);
            chkC.Checked = (selectedAnswers[idx] == 2);
            chkD.Checked = (selectedAnswers[idx] == 3);

            chkA.CheckedChanged += OnAnswerCheck;
            chkB.CheckedChanged += OnAnswerCheck;
            chkC.CheckedChanged += OnAnswerCheck;
            chkD.CheckedChanged += OnAnswerCheck;
        }

        // ===================== EVENT HANDLERS =====================
        private void OnAnswerCheck(object sender, EventArgs e)
        {
            CheckBox c = sender as CheckBox;
            if (c == null || !c.Checked) return;

            // Radio-button behavior: uncheck others
            chkA.CheckedChanged -= OnAnswerCheck;
            chkB.CheckedChanged -= OnAnswerCheck;
            chkC.CheckedChanged -= OnAnswerCheck;
            chkD.CheckedChanged -= OnAnswerCheck;

            if (c != chkA) chkA.Checked = false;
            if (c != chkB) chkB.Checked = false;
            if (c != chkC) chkC.Checked = false;
            if (c != chkD) chkD.Checked = false;

            chkA.CheckedChanged += OnAnswerCheck;
            chkB.CheckedChanged += OnAnswerCheck;
            chkC.CheckedChanged += OnAnswerCheck;
            chkD.CheckedChanged += OnAnswerCheck;

            // Save answer
            if (chkA.Checked) selectedAnswers[currentQuestion] = 0;
            else if (chkB.Checked) selectedAnswers[currentQuestion] = 1;
            else if (chkC.Checked) selectedAnswers[currentQuestion] = 2;
            else if (chkD.Checked) selectedAnswers[currentQuestion] = 3;
        }

        private void OnNextClick(object sender, EventArgs e)
        {
            int next = currentQuestion + 1;
            if (next >= totalQuestions) next = 0;
            DisplayQuestion(next);
        }

        private void OnFinishClick(object sender, EventArgs e)
        {
            if (!chkFinishExam.Checked)
            {
                MessageBox.Show("Please check 'I want to finish the exam' before submitting.",
                    "Finish Exam", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int answered = 0;
            for (int i = 0; i < totalQuestions; i++)
                if (selectedAnswers[i] >= 0) answered++;

            DialogResult r = MessageBox.Show(
                "You answered " + answered + "/" + totalQuestions + " questions.\nSubmit now?",
                "Confirm Finish", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes) DoSubmit();
        }

        private void DoSubmit()
        {
            countdownTimer.Stop();
            btnFinish.Enabled = false;
            btnNext.Enabled = false;

            // Save results to .dat file
            string resultFile = SaveResultToDat();

            if (examData != null && examData.ServerInfomation != null)
            {
                try
                {
                    string url = "tcp://" + examData.ServerInfomation.IP + ":" +
                        examData.ServerInfomation.Port + "/Server";
                    IRemoteServer srv = (IRemoteServer)Activator.GetObject(typeof(IRemoteServer), url);
                    if (srv != null)
                    {
                        SubmitPaper sp = new SubmitPaper();
                        sp.LoginId = examData.RegData != null ? examData.RegData.Login : "";
                        sp.TimeLeft = timeLeftSeconds;
                        sp.Finish = true;
                        sp.SubmitTime = DateTime.Now;
                        string msg = "";
                        srv.Submit(sp, ref msg);
                    }
                }
                catch { }
            }
            MessageBox.Show("Your exam has been submitted successfully!\nResult saved to: " + resultFile + "\nYou may now close this window.",
                "Exam Submitted", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Save exam results to a .dat file using ExamHelper.
        /// Delegates scoring and file generation to ExamHelper for testability.
        /// </summary>
        private string SaveResultToDat()
        {
            string login = (examData != null && examData.RegData != null) ? examData.RegData.Login : "unknown";
            string examCode = (examData != null && examData.RegData != null) ? examData.RegData.ExamCode : "N/A";
            string fileName = ExamHelper.GenerateResultFileName(login);
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            try
            {
                // Use ExamHelper for scoring
                List<Question> questions = loadedQuestions ?? ExamHelper.GetDefaultMockQuestions();
                ExamResult result = ExamHelper.CalculateScore(questions, selectedAnswers, login, examCode);

                // Use ExamHelper for file saving
                ExamHelper.SaveResultToDat(filePath, result);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving result file: " + ex.Message,
                    "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return filePath;
        }

        private void UpdateTimer()
        {
            int m = timeLeftSeconds / 60;
            int s = timeLeftSeconds % 60;
            lblTimeLeftValue.Text = string.Format("{0:D2}:{1:D2}", m, s);
            // Keep blue color always (original UI never changes timer color)
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            timeLeftSeconds--;
            if (timeLeftSeconds <= 0)
            {
                countdownTimer.Stop();
                timeLeftSeconds = 0;
                MessageBox.Show("Time is up! Your exam will be submitted automatically.",
                    "Time's Up", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DoSubmit();
                return;
            }
            UpdateTimer();
        }

        private void OnFontSettingChanged(object sender, EventArgs e)
        {
            try
            {
                Font f = new Font(cboFont.Text, (float)nudSize.Value);
                lblQuestionBody.Font = f;
                lblOptA.Font = f;
                lblOptB.Font = f;
                lblOptC.Font = f;
                lblOptD.Font = f;
                lblInstruction.Font = new Font(cboFont.Text, (float)nudSize.Value, FontStyle.Italic);
            }
            catch { }
        }

        private void OnFormClosing2(object sender, FormClosingEventArgs e)
        {
            if (btnFinish != null && btnFinish.Enabled)
            {
                DialogResult r = MessageBox.Show(
                    "You haven't submitted your exam yet!\nAre you sure you want to close?",
                    "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (r == DialogResult.No) { e.Cancel = true; return; }
            }
            if (countdownTimer != null) countdownTimer.Stop();
            Application.Exit();
            Environment.Exit(0);
        }

        private void OnResize2(object sender, EventArgs e)
        {
            int w = this.ClientSize.Width;
            int h = this.ClientSize.Height;

            // Tab fills remaining space below header
            tabExam.Location = new Point(0, pnlHeader.Height);
            tabExam.Size = new Size(w, h - pnlHeader.Height);

            // Next button at bottom-left of tab page
            if (btnNext != null && tabMultipleChoices != null)
            {
                int tabH = tabMultipleChoices.ClientSize.Height;
                btnNext.Location = new Point(5, tabH - 30);
            }

            // Question area inside tab
            if (pnlQuestionArea != null && tabMultipleChoices != null)
            {
                int tabW = tabMultipleChoices.ClientSize.Width;
                int tabH = tabMultipleChoices.ClientSize.Height;
                pnlQuestionArea.Size = new Size(tabW - 62, tabH - 60);
                lblQuestionBody.Width = pnlQuestionArea.Width - 20;
                pnlSeparator.Height = tabH - 55;
            }

            // Webcam panels - 2x2 grid anchored to right edge of HEADER
            // Original layout: 2 columns of red panels with white cross gap
            // Total width ~110px, placed at very top-right of header
            int headerW = pnlHeader.Width;
            int flagRight = headerW - 6;  // 6px margin from right edge
            // Column 1: 34px wide, Column 2: 52px wide, gap between = 4px
            // Row height: 28px each, gap between rows = 4px
            pnlCam1.Location = new Point(flagRight - 52 - 4 - 34, 2);        // top-left
            pnlCam2.Location = new Point(flagRight - 52, 2);                  // top-right  
            pnlCam3.Location = new Point(flagRight - 52 - 4 - 34, 2 + 28 + 4); // bottom-left
            pnlCam4.Location = new Point(flagRight - 52, 2 + 28 + 4);          // bottom-right
        }

        // ====================================================
        //                  BUILD ENTIRE UI
        // ====================================================
        private void BuildUI()
        {
            this.SuspendLayout();

            Font defaultFont = new Font("Microsoft Sans Serif", 8.25f);
            Font boldFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
            Font blueBoldUnder = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold | FontStyle.Underline);
            Font questionFont = new Font("Microsoft Sans Serif", 10f);

            // ========================
            // FORM SETTINGS
            // ========================
            this.ClientSize = new Size(1024, 600);
            this.MinimumSize = new Size(800, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "EOS Exam Client";
            this.BackColor = Color.White;
            this.Font = defaultFont;
            this.FormClosing += new FormClosingEventHandler(OnFormClosing2);
            this.Resize += new EventHandler(OnResize2);

            // ========================
            // HEADER PANEL - 105px tall
            // ========================
            pnlHeader = new Panel();
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Height = 105;
            pnlHeader.BackColor = SystemColors.Control;

            // ---------- ROW 1 (y=2): Finish checkbox + button + webcam ----------
            chkFinishExam = new CheckBox();
            chkFinishExam.Text = "I want to finish the exam";
            chkFinishExam.Location = new Point(125, 2);
            chkFinishExam.Size = new Size(155, 17);
            chkFinishExam.Font = defaultFont;
            pnlHeader.Controls.Add(chkFinishExam);

            btnFinish = new Button();
            btnFinish.Text = "Finish";
            btnFinish.Location = new Point(282, 0);
            btnFinish.Size = new Size(52, 21);
            btnFinish.Font = defaultFont;
            btnFinish.BackColor = Color.LightYellow;
            btnFinish.Click += new EventHandler(OnFinishClick);
            pnlHeader.Controls.Add(btnFinish);

            // Webcam red panels (Denmark flag - 4 panels, 2x2 grid)
            // Positioned at top-right of header, ABOVE the timer
            // Original sizes: left column=34x28, right column=52x28
            // Gap between columns=4px, gap between rows=4px
            // Total: (34+4+52)x(28+4+28) = 90x60
            pnlCam1 = new Panel(); pnlCam1.BackColor = Color.Red;
            pnlCam1.Size = new Size(34, 28);
            pnlCam1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pnlHeader.Controls.Add(pnlCam1);

            pnlCam2 = new Panel(); pnlCam2.BackColor = Color.Red;
            pnlCam2.Size = new Size(52, 28);
            pnlCam2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pnlHeader.Controls.Add(pnlCam2);

            pnlCam3 = new Panel(); pnlCam3.BackColor = Color.Red;
            pnlCam3.Size = new Size(34, 28);
            pnlCam3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pnlHeader.Controls.Add(pnlCam3);

            pnlCam4 = new Panel(); pnlCam4.BackColor = Color.Red;
            pnlCam4.Size = new Size(52, 28);
            pnlCam4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pnlHeader.Controls.Add(pnlCam4);

            // ---------- ROW 2 (y=21): Machine + Student ----------
            lblMachineLabel = new Label();
            lblMachineLabel.Text = "Machine:";
            lblMachineLabel.Location = new Point(4, 21);
            lblMachineLabel.AutoSize = true;
            lblMachineLabel.Font = boldFont;
            pnlHeader.Controls.Add(lblMachineLabel);

            lblMachineValue = new Label();
            lblMachineValue.Text = "DESKTOP-NFKTEOM";
            lblMachineValue.Location = new Point(62, 21);
            lblMachineValue.AutoSize = true;
            lblMachineValue.Font = boldFont;
            lblMachineValue.ForeColor = Color.Blue;
            pnlHeader.Controls.Add(lblMachineValue);

            lblStudentLabel = new Label();
            lblStudentLabel.Text = "Student:";
            lblStudentLabel.Location = new Point(180, 21);
            lblStudentLabel.AutoSize = true;
            lblStudentLabel.Font = defaultFont;
            pnlHeader.Controls.Add(lblStudentLabel);

            lblStudentValue = new Label();
            lblStudentValue.Text = "kienbtse05182";
            lblStudentValue.Location = new Point(225, 21);
            lblStudentValue.AutoSize = true;
            lblStudentValue.Font = boldFont;
            lblStudentValue.ForeColor = Color.Blue;
            pnlHeader.Controls.Add(lblStudentValue);

            // ---------- ROW 3 (y=38): Server + Exam Code ----------
            lblServerLabel = new Label();
            lblServerLabel.Text = "Server:";
            lblServerLabel.Location = new Point(8, 38);
            lblServerLabel.AutoSize = true;
            lblServerLabel.Font = defaultFont;
            pnlHeader.Controls.Add(lblServerLabel);

            lblServerValue = new Label();
            lblServerValue.Text = "Eng_EOS_02022";
            lblServerValue.Location = new Point(50, 38);
            lblServerValue.AutoSize = true;
            lblServerValue.Font = blueBoldUnder;
            lblServerValue.ForeColor = Color.Blue;
            pnlHeader.Controls.Add(lblServerValue);

            lblExamCodeLabel = new Label();
            lblExamCodeLabel.Text = "Exam Code:";
            lblExamCodeLabel.Location = new Point(155, 38);
            lblExamCodeLabel.AutoSize = true;
            lblExamCodeLabel.Font = defaultFont;
            pnlHeader.Controls.Add(lblExamCodeLabel);

            lblExamCodeValue = new Label();
            lblExamCodeValue.Text = "NWC202";
            lblExamCodeValue.Location = new Point(223, 38);
            lblExamCodeValue.AutoSize = true;
            lblExamCodeValue.Font = blueBoldUnder;
            lblExamCodeValue.ForeColor = Color.Blue;
            pnlHeader.Controls.Add(lblExamCodeValue);

            // ---------- ROW 4 (y=55): Duration + Open Code + Show Question ----------
            lblDurationLabel = new Label();
            lblDurationLabel.Text = "Duration:";
            lblDurationLabel.Location = new Point(4, 55);
            lblDurationLabel.AutoSize = true;
            lblDurationLabel.Font = defaultFont;
            pnlHeader.Controls.Add(lblDurationLabel);

            lblDurationValue = new Label();
            lblDurationValue.Text = "60 minutes";
            lblDurationValue.Location = new Point(57, 55);
            lblDurationValue.AutoSize = true;
            lblDurationValue.Font = blueBoldUnder;
            lblDurationValue.ForeColor = Color.Blue;
            pnlHeader.Controls.Add(lblDurationValue);

            lblOpenCodeLabel = new Label();
            lblOpenCodeLabel.Text = "Open Code:";
            lblOpenCodeLabel.Location = new Point(140, 55);
            lblOpenCodeLabel.AutoSize = true;
            lblOpenCodeLabel.Font = defaultFont;
            pnlHeader.Controls.Add(lblOpenCodeLabel);

            txtOpenCode = new TextBox();
            txtOpenCode.Location = new Point(205, 53);
            txtOpenCode.Size = new Size(80, 20);
            txtOpenCode.Font = defaultFont;
            pnlHeader.Controls.Add(txtOpenCode);

            btnShareQuestion = new Button();
            btnShareQuestion.Text = "Share Question";
            btnShareQuestion.Location = new Point(290, 52);
            btnShareQuestion.Size = new Size(90, 21);
            btnShareQuestion.Font = defaultFont;
            btnShareQuestion.Enabled = false;
            pnlHeader.Controls.Add(btnShareQuestion);

            // ---------- ROW 5 (y=72): Q mark + Total Marks + Vol ----------
            lblQMarkLabel = new Label();
            lblQMarkLabel.Text = "Q mark:";
            lblQMarkLabel.Location = new Point(4, 72);
            lblQMarkLabel.AutoSize = true;
            lblQMarkLabel.Font = defaultFont;
            pnlHeader.Controls.Add(lblQMarkLabel);

            lblQMarkValue = new Label();
            lblQMarkValue.Text = "1";
            lblQMarkValue.Location = new Point(50, 72);
            lblQMarkValue.AutoSize = true;
            lblQMarkValue.Font = boldFont;
            lblQMarkValue.ForeColor = Color.Blue;
            pnlHeader.Controls.Add(lblQMarkValue);

            lblTotalMarksLabel = new Label();
            lblTotalMarksLabel.Text = "Total Marks:";
            lblTotalMarksLabel.Location = new Point(95, 72);
            lblTotalMarksLabel.AutoSize = true;
            lblTotalMarksLabel.Font = defaultFont;
            pnlHeader.Controls.Add(lblTotalMarksLabel);

            lblTotalMarksValue = new Label();
            lblTotalMarksValue.Text = "60";
            lblTotalMarksValue.Location = new Point(168, 72);
            lblTotalMarksValue.AutoSize = true;
            lblTotalMarksValue.Font = boldFont;
            lblTotalMarksValue.ForeColor = Color.Blue;
            pnlHeader.Controls.Add(lblTotalMarksValue);

            lblVolLabel = new Label();
            lblVolLabel.Text = "Vol:";
            lblVolLabel.Location = new Point(210, 72);
            lblVolLabel.AutoSize = true;
            lblVolLabel.Font = defaultFont;
            pnlHeader.Controls.Add(lblVolLabel);

            nudVol = new NumericUpDown();
            nudVol.Location = new Point(233, 70);
            nudVol.Size = new Size(42, 20);
            nudVol.Value = 8;
            nudVol.Minimum = 0;
            nudVol.Maximum = 10;
            nudVol.Font = defaultFont;
            pnlHeader.Controls.Add(nudVol);

            // ---------- ROW 6 (y=89): Font + Size + Time Left ----------
            lblFontLabel = new Label();
            lblFontLabel.Text = "Font:";
            lblFontLabel.Location = new Point(115, 91);
            lblFontLabel.AutoSize = true;
            lblFontLabel.Font = defaultFont;
            pnlHeader.Controls.Add(lblFontLabel);

            cboFont = new ComboBox();
            cboFont.Location = new Point(140, 88);
            cboFont.Size = new Size(120, 21);
            cboFont.DropDownStyle = ComboBoxStyle.DropDown;
            cboFont.Text = "Microsoft Sans Serif";
            cboFont.Font = defaultFont;
            cboFont.Items.AddRange(new object[] {
                "Microsoft Sans Serif", "Arial", "Times New Roman",
                "Tahoma", "Verdana", "Courier New", "Segoe UI" });
            cboFont.SelectedIndexChanged += new EventHandler(OnFontSettingChanged);
            pnlHeader.Controls.Add(cboFont);

            lblSizeLabel = new Label();
            lblSizeLabel.Text = "Size:";
            lblSizeLabel.Location = new Point(266, 91);
            lblSizeLabel.AutoSize = true;
            lblSizeLabel.Font = defaultFont;
            pnlHeader.Controls.Add(lblSizeLabel);

            nudSize = new NumericUpDown();
            nudSize.Location = new Point(293, 88);
            nudSize.Size = new Size(42, 20);
            nudSize.Value = 10;
            nudSize.Minimum = 6;
            nudSize.Maximum = 30;
            nudSize.Font = defaultFont;
            nudSize.ValueChanged += new EventHandler(OnFontSettingChanged);
            pnlHeader.Controls.Add(nudSize);

            lblTimeLeftLabel = new Label();
            lblTimeLeftLabel.Text = "Time Left:";
            lblTimeLeftLabel.Location = new Point(340, 91);
            lblTimeLeftLabel.AutoSize = true;
            lblTimeLeftLabel.Font = defaultFont;
            pnlHeader.Controls.Add(lblTimeLeftLabel);

            // Blue timer display - large bold, positioned to the RIGHT 
            // but NOT overlapping with the flag panels (flag is at top-right)
            lblTimeLeftValue = new Label();
            lblTimeLeftValue.Text = "59:57";
            lblTimeLeftValue.Font = new Font("Microsoft Sans Serif", 45f, FontStyle.Bold);
            lblTimeLeftValue.ForeColor = Color.Blue;
            lblTimeLeftValue.Location = new Point(395, 55);
            lblTimeLeftValue.AutoSize = true;
            lblTimeLeftValue.BringToFront();
            pnlHeader.Controls.Add(lblTimeLeftValue);

            this.Controls.Add(pnlHeader);

            // ========================
            // TAB CONTROL
            // ========================
            tabExam = new TabControl();
            tabExam.Location = new Point(0, 105);
            tabExam.Size = new Size(1024, 465);
            tabExam.Font = defaultFont;

            tabMultipleChoices = new TabPage("Multiple Choices");
            tabMultipleChoices.BackColor = Color.White;
            tabMultipleChoices.UseVisualStyleBackColor = true;
            tabExam.TabPages.Add(tabMultipleChoices);

            // ========================
            // CONTENT AREA (inside tab)
            // ========================

            // Section title: "Multiple choices 1/60" (blue bold)
            lblSectionTitle = new Label();
            lblSectionTitle.Text = "Multiple choices 1/60";
            lblSectionTitle.Location = new Point(60, 6);
            lblSectionTitle.AutoSize = true;
            lblSectionTitle.Font = new Font("Microsoft Sans Serif", 9.5f, FontStyle.Bold);
            lblSectionTitle.ForeColor = Color.Blue;
            tabMultipleChoices.Controls.Add(lblSectionTitle);

            // "Answer" header (red, bold, underlined)
            lblAnswerHeader = new Label();
            lblAnswerHeader.Text = "Answer";
            lblAnswerHeader.Location = new Point(5, 30);
            lblAnswerHeader.AutoSize = true;
            lblAnswerHeader.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold | FontStyle.Underline);
            lblAnswerHeader.ForeColor = Color.Blue;
            tabMultipleChoices.Controls.Add(lblAnswerHeader);

            // Checkboxes A, B, C, D
            // Checkboxes with 26px vertical spacing matching original
            chkA = new CheckBox();
            chkA.Text = "A";
            chkA.Location = new Point(12, 50);
            chkA.AutoSize = true;
            chkA.Font = boldFont;
            chkA.CheckedChanged += new EventHandler(OnAnswerCheck);
            tabMultipleChoices.Controls.Add(chkA);

            chkB = new CheckBox();
            chkB.Text = "B";
            chkB.Location = new Point(12, 76);
            chkB.AutoSize = true;
            chkB.Font = boldFont;
            chkB.CheckedChanged += new EventHandler(OnAnswerCheck);
            tabMultipleChoices.Controls.Add(chkB);

            chkC = new CheckBox();
            chkC.Text = "C";
            chkC.Location = new Point(12, 102);
            chkC.AutoSize = true;
            chkC.Font = boldFont;
            chkC.CheckedChanged += new EventHandler(OnAnswerCheck);
            tabMultipleChoices.Controls.Add(chkC);

            chkD = new CheckBox();
            chkD.Text = "D";
            chkD.Location = new Point(12, 128);
            chkD.AutoSize = true;
            chkD.Font = boldFont;
            chkD.CheckedChanged += new EventHandler(OnAnswerCheck);
            tabMultipleChoices.Controls.Add(chkD);

            // Vertical separator line
            pnlSeparator = new Panel();
            pnlSeparator.BackColor = Color.LightGray;
            pnlSeparator.Location = new Point(54, 28);
            pnlSeparator.Size = new Size(1, 400);
            tabMultipleChoices.Controls.Add(pnlSeparator);

            // Question content panel (right of separator)
            pnlQuestionArea = new Panel();
            pnlQuestionArea.Location = new Point(58, 28);
            pnlQuestionArea.Size = new Size(950, 400);
            pnlQuestionArea.AutoScroll = true;
            pnlQuestionArea.BackColor = Color.White;
            tabMultipleChoices.Controls.Add(pnlQuestionArea);

            // "(Choose 1 answer)" instruction
            lblInstruction = new Label();
            lblInstruction.Text = "(Choose 1 answer)";
            lblInstruction.Location = new Point(4, 2);
            lblInstruction.AutoSize = true;
            lblInstruction.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Italic);
            pnlQuestionArea.Controls.Add(lblInstruction);

            // Question text
            lblQuestionBody = new Label();
            lblQuestionBody.Text = "";
            lblQuestionBody.Location = new Point(4, 22);
            lblQuestionBody.Size = new Size(900, 55);
            lblQuestionBody.Font = questionFont;
            pnlQuestionArea.Controls.Add(lblQuestionBody);

            // Answer options
            int optY = 85;
            int optSpacing = 30;

            lblOptA = new Label();
            lblOptA.Text = "A.";
            lblOptA.Location = new Point(14, optY);
            lblOptA.AutoSize = true;
            lblOptA.Font = questionFont;
            pnlQuestionArea.Controls.Add(lblOptA);

            lblOptB = new Label();
            lblOptB.Text = "B.";
            lblOptB.Location = new Point(14, optY + optSpacing);
            lblOptB.AutoSize = true;
            lblOptB.Font = questionFont;
            pnlQuestionArea.Controls.Add(lblOptB);

            lblOptC = new Label();
            lblOptC.Text = "C.";
            lblOptC.Location = new Point(14, optY + optSpacing * 2);
            lblOptC.AutoSize = true;
            lblOptC.Font = questionFont;
            pnlQuestionArea.Controls.Add(lblOptC);

            lblOptD = new Label();
            lblOptD.Text = "D.";
            lblOptD.Location = new Point(14, optY + optSpacing * 3);
            lblOptD.AutoSize = true;
            lblOptD.Font = questionFont;
            pnlQuestionArea.Controls.Add(lblOptD);

            this.Controls.Add(tabExam);

            // ========================
            // BOTTOM: Next button (inside tab page, bottom-left)
            // ========================
            btnNext = new Button();
            btnNext.Text = "Next";
            btnNext.Location = new Point(5, 390);
            btnNext.Size = new Size(50, 23);
            btnNext.Font = defaultFont;
            btnNext.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnNext.Click += new EventHandler(OnNextClick);
            tabMultipleChoices.Controls.Add(btnNext);

            // ========================
            // COUNTDOWN TIMER
            // ========================
            countdownTimer = new Timer();
            countdownTimer.Interval = 1000;
            countdownTimer.Tick += new EventHandler(OnTimerTick);

            this.ResumeLayout(false);
            this.PerformLayout();

            // Trigger initial resize
            OnResize2(null, null);
        }
    }
}
