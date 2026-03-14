using ExamClient;

namespace EOSClient.Tests
{

    [TestFixture]
    public class ResultFileOutputTests
    {
        private string _testDataDir = null!;

        [OneTimeSetUp]
        public void Setup()
        {
            _testDataDir = Path.Combine(Path.GetTempPath(), "eos_result_" + Guid.NewGuid().ToString("N")[..8]);
            Directory.CreateDirectory(_testDataDir);
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            if (Directory.Exists(_testDataDir))
                Directory.Delete(_testDataDir, true);
        }



        [Test]
        [Property("QaseTitle", "SaveResultToDat creates file at specified path")]
        public void SaveResultToDat_ValidInput_CreatesFile()
        {
            var result = CreateSampleResult(3, 1, 1);
            var filePath = Path.Combine(_testDataDir, "test_output_1.dat");

            ExamHelper.SaveResultToDat(filePath, result);

            Assert.That(File.Exists(filePath), Is.True);
        }

        [Test]
        [Property("QaseTitle", "SaveResultToDat file contains student name")]
        public void SaveResultToDat_ValidInput_ContainsStudentName()
        {
            var result = CreateSampleResult(3, 1, 1);
            result.StudentName = "NguyenVanA";
            var filePath = Path.Combine(_testDataDir, "test_output_2.dat");

            ExamHelper.SaveResultToDat(filePath, result);
            var content = File.ReadAllText(filePath);

            Assert.That(content, Does.Contain("Student: NguyenVanA"));
        }

        [Test]
        [Property("QaseTitle", "SaveResultToDat file contains exam code")]
        public void SaveResultToDat_ValidInput_ContainsExamCode()
        {
            var result = CreateSampleResult(3, 1, 1);
            result.ExamCode = "ENG001";
            var filePath = Path.Combine(_testDataDir, "test_output_3.dat");

            ExamHelper.SaveResultToDat(filePath, result);
            var content = File.ReadAllText(filePath);

            Assert.That(content, Does.Contain("ExamCode: ENG001"));
        }

        [Test]
        [Property("QaseTitle", "SaveResultToDat file contains correct score")]
        public void SaveResultToDat_ValidInput_ContainsCorrectScore()
        {
            var result = CreateSampleResult(3, 1, 1);
            var filePath = Path.Combine(_testDataDir, "test_output_4.dat");

            ExamHelper.SaveResultToDat(filePath, result);
            var content = File.ReadAllText(filePath);

            Assert.That(content, Does.Contain("Correct: 3"));
            Assert.That(content, Does.Contain("Wrong: 1"));
            Assert.That(content, Does.Contain("Unanswered: 1"));
        }

        [Test]
        [Property("QaseTitle", "SaveResultToDat file contains score percentage")]
        public void SaveResultToDat_ValidInput_ContainsScorePercent()
        {
            var result = CreateSampleResult(3, 1, 1);
            result.ScorePercent = 60.0;
            var filePath = Path.Combine(_testDataDir, "test_output_5.dat");

            ExamHelper.SaveResultToDat(filePath, result);
            var content = File.ReadAllText(filePath);

            Assert.That(content, Does.Contain("Score: 60.0%"));
        }

        [Test]
        [Property("QaseTitle", "SaveResultToDat file contains question details")]
        public void SaveResultToDat_ValidInput_ContainsQuestionDetails()
        {
            var result = CreateSampleResult(1, 0, 0);
            result.Details.Add(new QuestionResult
            {
                QuestionNumber = 1,
                QuestionText = "What is 2+2?",
                SelectedAnswer = "C",
                CorrectAnswer = "C",
                Status = "CORRECT"
            });
            var filePath = Path.Combine(_testDataDir, "test_output_6.dat");

            ExamHelper.SaveResultToDat(filePath, result);
            var content = File.ReadAllText(filePath);

            Assert.That(content, Does.Contain("Q1: What is 2+2?"));
            Assert.That(content, Does.Contain("CORRECT"));
        }

        [Test]
        [Property("QaseTitle", "SaveResultToDat file has header and footer markers")]
        public void SaveResultToDat_ValidInput_HasHeaderAndFooter()
        {
            var result = CreateSampleResult(0, 0, 0);
            var filePath = Path.Combine(_testDataDir, "test_output_7.dat");

            ExamHelper.SaveResultToDat(filePath, result);
            var content = File.ReadAllText(filePath);

            Assert.That(content, Does.Contain("=== EXAM RESULT ==="));
            Assert.That(content, Does.Contain("=== END ==="));
        }

        [Test]
        [Property("QaseTitle", "SaveResultToDat with null path throws ArgumentNullException")]
        public void SaveResultToDat_NullPath_ThrowsException()
        {
            var result = CreateSampleResult(0, 0, 0);

            Assert.Throws<ArgumentNullException>(() =>
            {
                ExamHelper.SaveResultToDat(null!, result);
            });
        }

        [Test]
        [Property("QaseTitle", "SaveResultToDat with null result throws ArgumentNullException")]
        public void SaveResultToDat_NullResult_ThrowsException()
        {
            var filePath = Path.Combine(_testDataDir, "should_not_exist.dat");

            Assert.Throws<ArgumentNullException>(() =>
            {
                ExamHelper.SaveResultToDat(filePath, null!);
            });
        }



        [Test]
        [Property("QaseTitle", "GenerateResultFileName contains student name")]
        public void GenerateResultFileName_ValidName_ContainsStudentName()
        {
            var fileName = ExamHelper.GenerateResultFileName("NguyenVanA");

            Assert.That(fileName, Does.StartWith("ExamResult_NguyenVanA_"));
            Assert.That(fileName, Does.EndWith(".dat"));
        }

        [Test]
        [Property("QaseTitle", "GenerateResultFileName with null uses unknown")]
        public void GenerateResultFileName_NullName_UsesUnknown()
        {
            var fileName = ExamHelper.GenerateResultFileName(null!);

            Assert.That(fileName, Does.StartWith("ExamResult_unknown_"));
        }

        [Test]
        [Property("QaseTitle", "GenerateResultFileName with empty string uses unknown")]
        public void GenerateResultFileName_EmptyName_UsesUnknown()
        {
            var fileName = ExamHelper.GenerateResultFileName("");

            Assert.That(fileName, Does.StartWith("ExamResult_unknown_"));
        }

        [Test]
        [Property("QaseTitle", "GenerateResultFileName has dat extension")]
        public void GenerateResultFileName_AnyName_HasDatExtension()
        {
            var fileName = ExamHelper.GenerateResultFileName("Test");

            Assert.That(Path.GetExtension(fileName), Is.EqualTo(".dat"));
        }



        private ExamResult CreateSampleResult(int correct, int wrong, int unanswered)
        {
            return new ExamResult
            {
                StudentName = "TestUser",
                ExamCode = "TEST001",
                TotalQuestions = correct + wrong + unanswered,
                Correct = correct,
                Wrong = wrong,
                Unanswered = unanswered,
                ScorePercent = (correct + wrong + unanswered) > 0
                    ? ((double)correct / (correct + wrong + unanswered) * 100)
                    : 0
            };
        }
    }
}
