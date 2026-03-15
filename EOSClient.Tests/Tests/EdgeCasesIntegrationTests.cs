using ExamClient;

namespace EOSClient.Tests
{

    [TestFixture]
    public class EdgeCasesIntegrationTests
    {
        private string _testDataDir = null!;

        [OneTimeSetUp]
        public void Setup()
        {
            _testDataDir = Path.Combine(Path.GetTempPath(), "eos_edge_" + Guid.NewGuid().ToString("N")[..8]);
            Directory.CreateDirectory(_testDataDir);
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            if (Directory.Exists(_testDataDir))
                Directory.Delete(_testDataDir, true);
        }

        private string CreateCsvFile(string content, string fileName = "test.csv")
        {
            var path = Path.Combine(_testDataDir, fileName);
            File.WriteAllText(path, content);
            return path;
        }



        [Test]
        [Property("QaseTitle", "CSV with empty lines between rows skips empty lines")]
        public void LoadCSV_EmptyLinesBetweenRows_SkipsEmptyLines()
        {
            var csv = "Header,H,H,H,H,H\n"
                    + "Q1,A,B,C,D,A\n"
                    + "\n"
                    + "Q2,A,B,C,D,B\n"
                    + "\n"
                    + "Q3,A,B,C,D,C\n";
            var path = CreateCsvFile(csv, "edge_empty_lines.csv");

            var questions = ExamHelper.LoadQuestionsFromCSV(path);

            Assert.That(questions, Has.Count.EqualTo(3));
        }

        [Test]
        [Property("QaseTitle", "CSV with fewer than 6 columns silently skips invalid row")]
        public void LoadCSV_FewerColumns_SkipsInvalidRow()
        {
            var csv = "Header,H,H,H,H,H\n"
                    + "Q1,A,B,C,D,A\n"
                    + "Invalid,Only,Three\n"
                    + "Q2,A,B,C,D,B\n";
            var path = CreateCsvFile(csv, "edge_few_cols.csv");

            var questions = ExamHelper.LoadQuestionsFromCSV(path);

            Assert.That(questions, Has.Count.EqualTo(2));
        }

        [Test]
        [Property("QaseTitle", "CSV with lowercase answer is converted to uppercase")]
        public void LoadCSV_LowercaseAnswer_ConvertedToUppercase()
        {
            var csv = "Header,H,H,H,H,H\nQ1,A,B,C,D,c\n";
            var path = CreateCsvFile(csv, "edge_lowercase.csv");

            var questions = ExamHelper.LoadQuestionsFromCSV(path);

            Assert.That(questions[0].CorrectAnswer, Is.EqualTo("C"));
        }

        [Test]
        [Property("QaseTitle", "CSV with whitespace-trimmed fields are cleaned")]
        public void LoadCSV_WhitespaceInFields_AreTrimmed()
        {
            var csv = "Header,H,H,H,H,H\n  Q1  ,  A  ,  B  ,  C  ,  D  ,  A  \n";
            var path = CreateCsvFile(csv, "edge_whitespace.csv");

            var questions = ExamHelper.LoadQuestionsFromCSV(path);

            Assert.That(questions[0].Text, Is.EqualTo("Q1"));
            Assert.That(questions[0].OptionA, Is.EqualTo("A"));
            Assert.That(questions[0].CorrectAnswer, Is.EqualTo("A"));
        }

        [Test]
        [Property("QaseTitle", "LoadQuestionsFromCSV with null path throws ArgumentNullException")]
        public void LoadCSV_NullPath_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                ExamHelper.LoadQuestionsFromCSV(null!);
            });
        }

        [Test]
        [Property("QaseTitle", "LoadQuestionsFromCSV with empty string throws ArgumentNullException")]
        public void LoadCSV_EmptyPath_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                ExamHelper.LoadQuestionsFromCSV("");
            });
        }



        [Test]
        [Property("QaseTitle", "CalculateScore with all skipped answers counts as unanswered")]
        public void CalculateScore_AllSkipped_CountsAsUnanswered()
        {
            var questions = new List<Question>
            {
                new Question { Text = "Q1", CorrectAnswer = "A" },
                new Question { Text = "Q2", CorrectAnswer = "B" },
            };
            int[] answers = { -1, -1 };

            var result = ExamHelper.CalculateScore(questions, answers);

            Assert.That(result.Unanswered, Is.EqualTo(2));
            Assert.That(result.Correct, Is.EqualTo(0));
            Assert.That(result.Wrong, Is.EqualTo(0));
            Assert.That(result.ScorePercent, Is.EqualTo(0));
        }

        [Test]
        [Property("QaseTitle", "CalculateScore with out-of-range answer index treated as skipped")]
        public void CalculateScore_OutOfRangeIndex_TreatedAsSkipped()
        {
            var questions = new List<Question>
            {
                new Question { Text = "Q1", CorrectAnswer = "A" },
            };
            int[] answers = { 5 };

            var result = ExamHelper.CalculateScore(questions, answers);

            Assert.That(result.Unanswered, Is.EqualTo(1));
        }

        [Test]
        [Property("QaseTitle", "CalculateScore with shorter answers array treats extra questions as skipped")]
        public void CalculateScore_ShorterAnswersArray_ExtraQuestionsSkipped()
        {
            var questions = new List<Question>
            {
                new Question { Text = "Q1", CorrectAnswer = "A" },
                new Question { Text = "Q2", CorrectAnswer = "B" },
                new Question { Text = "Q3", CorrectAnswer = "C" },
            };
            int[] answers = { 0 };

            var result = ExamHelper.CalculateScore(questions, answers);

            Assert.That(result.Correct, Is.EqualTo(1));
            Assert.That(result.Unanswered, Is.EqualTo(2));
            Assert.That(result.TotalQuestions, Is.EqualTo(3));
        }

        [Test]
        [Property("QaseTitle", "CalculateScore with null questions throws ArgumentNullException")]
        public void CalculateScore_NullQuestions_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                ExamHelper.CalculateScore(null!, new int[] { 0 });
            });
        }

        [Test]
        [Property("QaseTitle", "CalculateScore with null answers throws ArgumentNullException")]
        public void CalculateScore_NullAnswers_ThrowsException()
        {
            var questions = new List<Question>
            {
                new Question { Text = "Q1", CorrectAnswer = "A" },
            };

            Assert.Throws<ArgumentNullException>(() =>
            {
                ExamHelper.CalculateScore(questions, null!);
            });
        }

        [Test]
        [Property("QaseTitle", "ScorePercent calculation is accurate with 3 of 5 correct")]
        public void CalculateScore_ThreeOfFive_ScoreIs60Percent()
        {
            var questions = new List<Question>
            {
                new Question { Text = "Q1", CorrectAnswer = "A" },
                new Question { Text = "Q2", CorrectAnswer = "B" },
                new Question { Text = "Q3", CorrectAnswer = "C" },
                new Question { Text = "Q4", CorrectAnswer = "D" },
                new Question { Text = "Q5", CorrectAnswer = "A" },
            };

            int[] answers = { 0, 1, 2, 0, 1 };

            var result = ExamHelper.CalculateScore(questions, answers);

            Assert.That(result.ScorePercent, Is.EqualTo(60.0));
        }



        [Test]
        [Property("QaseTitle", "GetDefaultMockQuestions returns exactly 5 questions")]
        public void GetDefaultMockQuestions_Returns5Questions()
        {
            var questions = ExamHelper.GetDefaultMockQuestions();

            Assert.That(questions, Has.Count.EqualTo(5));
        }

        [Test]
        [Property("QaseTitle", "GetDefaultMockQuestions all have non-empty text")]
        public void GetDefaultMockQuestions_AllHaveText()
        {
            var questions = ExamHelper.GetDefaultMockQuestions();

            foreach (var q in questions)
            {
                Assert.That(q.Text, Is.Not.Null.And.Not.Empty);
                Assert.That(q.CorrectAnswer, Is.Not.Null.And.Not.Empty);
            }
        }

        [Test]
        [Property("QaseTitle", "GetDefaultMockQuestions GetOptions returns 4 options per question")]
        public void GetDefaultMockQuestions_GetOptionsReturns4()
        {
            var questions = ExamHelper.GetDefaultMockQuestions();

            foreach (var q in questions)
            {
                Assert.That(q.GetOptions(), Has.Length.EqualTo(4));
            }
        }



        [Test]
        [Property("QaseTitle", "E2E: Load CSV → Calculate Score → Save to .dat file")]
        public void EndToEnd_LoadScoreSave_CompletesSuccessfully()
        {

            var csv = "question,optionA,optionB,optionC,optionD,correctAnswer\n"
                    + "1+1=?,1,2,3,4,B\n"
                    + "2+2=?,3,4,5,6,B\n"
                    + "5-3=?,1,2,3,4,B\n";
            var csvPath = CreateCsvFile(csv, "e2e_questions.csv");


            var questions = ExamHelper.LoadQuestionsFromCSV(csvPath);
            Assert.That(questions, Has.Count.EqualTo(3));


            int[] answers = { 1, 1, 0 };
            var result = ExamHelper.CalculateScore(questions, answers, "TestStudent", "MATH001");
            Assert.That(result.Correct, Is.EqualTo(2));
            Assert.That(result.Wrong, Is.EqualTo(1));


            var datPath = Path.Combine(_testDataDir, "e2e_result.dat");
            ExamHelper.SaveResultToDat(datPath, result);


            Assert.That(File.Exists(datPath), Is.True);
            var content = File.ReadAllText(datPath);
            Assert.That(content, Does.Contain("Student: TestStudent"));
            Assert.That(content, Does.Contain("ExamCode: MATH001"));
            Assert.That(content, Does.Contain("Correct: 2"));
            Assert.That(content, Does.Contain("Wrong: 1"));
        }

        [Test]
        [Property("QaseTitle", "E2E: Default mock questions can be scored and saved")]
        public void EndToEnd_MockQuestions_ScoreAndSave()
        {

            var questions = ExamHelper.GetDefaultMockQuestions();


            int[] answers = { 1, 1, 1, 3, 2 };
            var result = ExamHelper.CalculateScore(questions, answers, "PerfectStudent", "MOCK001");

            Assert.That(result.Correct, Is.EqualTo(5));
            Assert.That(result.ScorePercent, Is.EqualTo(100.0));


            var datPath = Path.Combine(_testDataDir, "e2e_mock_result.dat");
            ExamHelper.SaveResultToDat(datPath, result);

            var content = File.ReadAllText(datPath);
            Assert.That(content, Does.Contain("Score: 100.0%"));
        }

        [Test]
        [Property("QaseTitle", "E2E: Real questions.csv → Score → .dat file matches CSV content")]
        public void EndToEnd_RealCsvFile_DatMatchesCsvContent()
        {

            var sourceCsv = Path.Combine(
                Directory.GetCurrentDirectory(), "..", "..", "..", "..", 
                "MockEOSServer", "questions.csv");
            
            Assert.That(File.Exists(sourceCsv), Is.True, 
                $"Source questions.csv not found at: {sourceCsv}");
            
            var csvPath = CreateCsvFile(File.ReadAllText(sourceCsv), "real_questions.csv");


            var questions = ExamHelper.LoadQuestionsFromCSV(csvPath);
            Assert.That(questions, Has.Count.GreaterThan(0), 
                "CSV file should contain at least 1 question");


            int[] answers = new int[questions.Count];
            for (int i = 0; i < questions.Count; i++)
            {
                switch (questions[i].CorrectAnswer)
                {
                    case "A": answers[i] = 0; break;
                    case "B": answers[i] = 1; break;
                    case "C": answers[i] = 2; break;
                    case "D": answers[i] = 3; break;
                    default: answers[i] = -1; break;
                }
            }


            var result = ExamHelper.CalculateScore(questions, answers, "csv_test_student", "CSV_EXAM");
            Assert.That(result.Correct, Is.EqualTo(questions.Count), 
                "All answers should be correct");
            Assert.That(result.ScorePercent, Is.EqualTo(100.0));
            Assert.That(result.TotalQuestions, Is.EqualTo(questions.Count));


            var datPath = Path.Combine(_testDataDir, "real_csv_result.dat");
            ExamHelper.SaveResultToDat(datPath, result);
            Assert.That(File.Exists(datPath), Is.True, ".dat file should be created");


            var datContent = File.ReadAllText(datPath);
            Assert.That(datContent, Does.Contain("Student: csv_test_student"));
            Assert.That(datContent, Does.Contain("ExamCode: CSV_EXAM"));
            Assert.That(datContent, Does.Contain($"Total Questions: {questions.Count}"));
            Assert.That(datContent, Does.Contain($"Correct: {questions.Count}"));
            Assert.That(datContent, Does.Contain("Wrong: 0"));
            Assert.That(datContent, Does.Contain("Unanswered: 0"));
            Assert.That(datContent, Does.Contain("Score: 100.0%"));


            for (int i = 0; i < questions.Count; i++)
            {
                Assert.That(datContent, Does.Contain($"Q{i + 1}: {questions[i].Text}"),
                    $"Question {i + 1} text should appear in .dat file");
                Assert.That(datContent, Does.Contain($"Correct: {questions[i].CorrectAnswer}"),
                    $"Question {i + 1} correct answer should appear in .dat file");
                Assert.That(datContent, Does.Contain($"Q{i + 1}:").And.Contains("CORRECT"),
                    $"Question {i + 1} should be marked CORRECT");
            }
        }

        [Test]
        [Property("QaseTitle", "E2E: Real questions.csv with partial answers produces correct .dat")]
        public void EndToEnd_RealCsvFile_PartialAnswers_CorrectDat()
        {

            var sourceCsv = Path.Combine(
                Directory.GetCurrentDirectory(), "..", "..", "..", "..", 
                "MockEOSServer", "questions.csv");
            
            var csvPath = CreateCsvFile(File.ReadAllText(sourceCsv), "partial_questions.csv");
            var questions = ExamHelper.LoadQuestionsFromCSV(csvPath);
            Assert.That(questions, Has.Count.GreaterThan(1));


            int[] answers = new int[questions.Count];
            for (int i = 0; i < questions.Count; i++) answers[i] = -1;

            switch (questions[0].CorrectAnswer)
            {
                case "A": answers[0] = 0; break;
                case "B": answers[0] = 1; break;
                case "C": answers[0] = 2; break;
                case "D": answers[0] = 3; break;
            }


            var result = ExamHelper.CalculateScore(questions, answers, "partial_student", "PARTIAL_EXAM");
            Assert.That(result.Correct, Is.EqualTo(1));
            Assert.That(result.Unanswered, Is.EqualTo(questions.Count - 1));


            var datPath = Path.Combine(_testDataDir, "partial_result.dat");
            ExamHelper.SaveResultToDat(datPath, result);

            var datContent = File.ReadAllText(datPath);
            Assert.That(datContent, Does.Contain("Correct: 1"));
            Assert.That(datContent, Does.Contain($"Unanswered: {questions.Count - 1}"));
            Assert.That(datContent, Does.Contain("Q1:").And.Contains("CORRECT"));
        }
    }
}
