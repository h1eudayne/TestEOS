using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ExamClient
{
    /// <summary>
    /// Represents a single exam question with 4 answer options.
    /// </summary>
    public class Question
    {
        public string Text { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string CorrectAnswer { get; set; } // "A", "B", "C", or "D"

        /// <summary>
        /// Returns all 4 options as a string array.
        /// </summary>
        public string[] GetOptions()
        {
            return new string[] { OptionA, OptionB, OptionC, OptionD };
        }
    }

    /// <summary>
    /// Contains the result of a single question attempt.
    /// </summary>
    public class QuestionResult
    {
        public int QuestionNumber { get; set; }
        public string QuestionText { get; set; }
        public string SelectedAnswer { get; set; } // "A","B","C","D" or "-" (skipped)
        public string CorrectAnswer { get; set; }
        public string Status { get; set; } // "CORRECT", "WRONG", "SKIPPED"
    }

    /// <summary>
    /// Contains the overall exam result summary.
    /// </summary>
    public class ExamResult
    {
        public string StudentName { get; set; }
        public string ExamCode { get; set; }
        public DateTime ExamDate { get; set; }
        public int TotalQuestions { get; set; }
        public int Correct { get; set; }
        public int Wrong { get; set; }
        public int Unanswered { get; set; }
        public double ScorePercent { get; set; }
        public List<QuestionResult> Details { get; set; }

        public ExamResult()
        {
            Details = new List<QuestionResult>();
            ExamDate = DateTime.Now;
        }
    }

    /// <summary>
    /// Static helper class containing all testable exam logic.
    /// Extracted from frmEnglishExamClient to allow unit testing.
    /// </summary>
    public static class ExamHelper
    {
        private static readonly string[] AnswerLetters = { "A", "B", "C", "D" };

        /// <summary>
        /// Load questions from a CSV file.
        /// Format: question,optionA,optionB,optionC,optionD,correctAnswer
        /// First row is treated as header and skipped.
        /// </summary>
        /// <param name="csvPath">Path to the CSV file.</param>
        /// <returns>List of parsed Question objects.</returns>
        /// <exception cref="FileNotFoundException">Thrown when csvPath does not exist.</exception>
        public static List<Question> LoadQuestionsFromCSV(string csvPath)
        {
            if (string.IsNullOrEmpty(csvPath))
                throw new ArgumentNullException("csvPath");

            if (!File.Exists(csvPath))
                throw new FileNotFoundException("CSV file not found: " + csvPath, csvPath);

            List<Question> questions = new List<Question>();
            string[] lines = File.ReadAllLines(csvPath);

            // Skip header (first line)
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                if (string.IsNullOrEmpty(line)) continue;

                string[] parts = line.Split(',');
                if (parts.Length >= 6)
                {
                    Question q = new Question
                    {
                        Text = parts[0].Trim(),
                        OptionA = parts[1].Trim(),
                        OptionB = parts[2].Trim(),
                        OptionC = parts[3].Trim(),
                        OptionD = parts[4].Trim(),
                        CorrectAnswer = parts[5].Trim().ToUpper()
                    };
                    questions.Add(q);
                }
                // Rows with fewer than 6 columns are silently skipped
            }

            return questions;
        }

        /// <summary>
        /// Calculate the exam score based on questions, selected answers, and correct answers.
        /// </summary>
        /// <param name="questions">List of exam questions.</param>
        /// <param name="selectedAnswers">
        /// Array of selected answer indices (0=A, 1=B, 2=C, 3=D, -1=skipped).
        /// Must have same length as questions.
        /// </param>
        /// <returns>An ExamResult with score details and per-question breakdown.</returns>
        public static ExamResult CalculateScore(List<Question> questions, int[] selectedAnswers)
        {
            return CalculateScore(questions, selectedAnswers, "unknown", "N/A");
        }

        /// <summary>
        /// Calculate the exam score with student info.
        /// </summary>
        public static ExamResult CalculateScore(
            List<Question> questions,
            int[] selectedAnswers,
            string studentName,
            string examCode)
        {
            if (questions == null) throw new ArgumentNullException("questions");
            if (selectedAnswers == null) throw new ArgumentNullException("selectedAnswers");

            ExamResult result = new ExamResult
            {
                StudentName = studentName ?? "unknown",
                ExamCode = examCode ?? "N/A",
                TotalQuestions = questions.Count,
                ExamDate = DateTime.Now
            };

            int correct = 0;
            int wrong = 0;
            int unanswered = 0;

            for (int i = 0; i < questions.Count; i++)
            {
                string selected = (i < selectedAnswers.Length && selectedAnswers[i] >= 0 && selectedAnswers[i] < 4)
                    ? AnswerLetters[selectedAnswers[i]]
                    : "-";

                string correctAns = questions[i].CorrectAnswer ?? "?";
                string status;

                if (selected == "-")
                {
                    unanswered++;
                    status = "SKIPPED";
                }
                else if (selected.Equals(correctAns, StringComparison.OrdinalIgnoreCase))
                {
                    correct++;
                    status = "CORRECT";
                }
                else
                {
                    wrong++;
                    status = "WRONG";
                }

                result.Details.Add(new QuestionResult
                {
                    QuestionNumber = i + 1,
                    QuestionText = questions[i].Text,
                    SelectedAnswer = selected,
                    CorrectAnswer = correctAns,
                    Status = status
                });
            }

            result.Correct = correct;
            result.Wrong = wrong;
            result.Unanswered = unanswered;
            result.ScorePercent = (questions.Count > 0)
                ? ((double)correct / questions.Count * 100)
                : 0;

            return result;
        }

        /// <summary>
        /// Save exam result to a .dat file with detailed per-question breakdown.
        /// </summary>
        /// <param name="filePath">Full path to the output .dat file.</param>
        /// <param name="result">The ExamResult to save.</param>
        public static void SaveResultToDat(string filePath, ExamResult result)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException("filePath");
            if (result == null)
                throw new ArgumentNullException("result");

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("=== EXAM RESULT ===");
            sb.AppendLine("Student: " + result.StudentName);
            sb.AppendLine("ExamCode: " + result.ExamCode);
            sb.AppendLine("Date: " + result.ExamDate.ToString("yyyy-MM-dd HH:mm:ss"));
            sb.AppendLine("Total Questions: " + result.TotalQuestions);
            sb.AppendLine("Correct: " + result.Correct);
            sb.AppendLine("Wrong: " + result.Wrong);
            sb.AppendLine("Unanswered: " + result.Unanswered);
            sb.AppendLine("Score: " + result.ScorePercent.ToString("F1") + "%");
            sb.AppendLine("---");

            foreach (QuestionResult qr in result.Details)
            {
                sb.AppendLine("Q" + qr.QuestionNumber + ": " + qr.QuestionText +
                    " | Selected: " + qr.SelectedAnswer +
                    " | Correct: " + qr.CorrectAnswer +
                    " | " + qr.Status);
            }

            sb.AppendLine("=== END ===");

            File.WriteAllText(filePath, sb.ToString());
        }

        /// <summary>
        /// Generate the default .dat filename for a student.
        /// </summary>
        public static string GenerateResultFileName(string studentName)
        {
            string name = string.IsNullOrEmpty(studentName) ? "unknown" : studentName;
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            return "ExamResult_" + name + "_" + timestamp + ".dat";
        }

        /// <summary>
        /// Generate 5 default mock questions (hardcoded math questions).
        /// Used as fallback when CSV file is not available.
        /// </summary>
        public static List<Question> GetDefaultMockQuestions()
        {
            return new List<Question>
            {
                new Question { Text = "1 + 1 = ?", OptionA = "A. 1", OptionB = "B. 2", OptionC = "C. 3", OptionD = "D. 4", CorrectAnswer = "B" },
                new Question { Text = "2 + 2 = ?", OptionA = "A. 3", OptionB = "B. 4", OptionC = "C. 5", OptionD = "D. 6", CorrectAnswer = "B" },
                new Question { Text = "5 - 3 = ?", OptionA = "A. 1", OptionB = "B. 2", OptionC = "C. 3", OptionD = "D. 4", CorrectAnswer = "B" },
                new Question { Text = "3 * 3 = ?", OptionA = "A. 6", OptionB = "B. 7", OptionC = "C. 8", OptionD = "D. 9", CorrectAnswer = "D" },
                new Question { Text = "10 / 2 = ?", OptionA = "A. 3", OptionB = "B. 4", OptionC = "C. 5", OptionD = "D. 6", CorrectAnswer = "C" }
            };
        }
    }
}
