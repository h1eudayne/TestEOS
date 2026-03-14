using ExamClient;

namespace EOSClient.Tests
{

    [TestFixture]
    public class CsvQuestionLoaderTests
    {
        private string _testDataDir = null!;

        [OneTimeSetUp]
        public void Setup()
        {
            _testDataDir = Path.Combine(Path.GetTempPath(), "eos_tests_" + Guid.NewGuid().ToString("N")[..8]);
            Directory.CreateDirectory(_testDataDir);
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            if (Directory.Exists(_testDataDir))
                Directory.Delete(_testDataDir, true);
        }

        private string CreateCsvFile(string content, string fileName = "test_questions.csv")
        {
            var path = Path.Combine(_testDataDir, fileName);
            File.WriteAllText(path, content);
            return path;
        }

        [Test]
        [Property("QaseTitle", "Load valid CSV with 5 questions returns correct count")]
        public void LoadQuestionsFromCSV_ValidFile_ReturnsCorrectCount()
        {

            var csv = "Question,OptionA,OptionB,OptionC,OptionD,Answer\n"
                    + "What is 2+2?,2,3,4,5,C\n"
                    + "Capital of France?,London,Paris,Berlin,Rome,B\n"
                    + "Largest ocean?,Atlantic,Indian,Pacific,Arctic,C\n"
                    + "H2O is?,Gold,Silver,Water,Oil,C\n"
                    + "Sun is a?,Planet,Star,Moon,Comet,B\n";
            var path = CreateCsvFile(csv);

            var questions = ExamHelper.LoadQuestionsFromCSV(path);

            Assert.That(questions, Has.Count.EqualTo(5));
        }

        [Test]
        [Property("QaseTitle", "Load valid CSV - question text is parsed correctly")]
        public void LoadQuestionsFromCSV_ValidFile_ParsesQuestionText()
        {
            var csv = "Header,H,H,H,H,H\nWhat is 2+2?,2,3,4,5,C\n";
            var path = CreateCsvFile(csv);

            var questions = ExamHelper.LoadQuestionsFromCSV(path);

            Assert.That(questions[0].Text, Is.EqualTo("What is 2+2?"));
        }

        [Test]
        [Property("QaseTitle", "Load valid CSV - correct answer is parsed correctly")]
        public void LoadQuestionsFromCSV_ValidFile_ParsesCorrectAnswer()
        {
            var csv = "Header,H,H,H,H,H\nWhat is 2+2?,2,3,4,5,C\n";
            var path = CreateCsvFile(csv);

            var questions = ExamHelper.LoadQuestionsFromCSV(path);

            Assert.That(questions[0].CorrectAnswer, Is.EqualTo("C"));
        }

        [Test]
        [Property("QaseTitle", "Load CSV with only header returns empty list")]
        public void LoadQuestionsFromCSV_OnlyHeader_ReturnsEmptyList()
        {
            var path = CreateCsvFile("Header,H,H,H,H,H\n");

            var questions = ExamHelper.LoadQuestionsFromCSV(path);

            Assert.That(questions, Is.Empty);
        }

        [Test]
        [Property("QaseTitle", "Load CSV with nonexistent path throws exception")]
        public void LoadQuestionsFromCSV_FileNotFound_ThrowsException()
        {
            var fakePath = Path.Combine(_testDataDir, "nonexistent.csv");

            Assert.Throws<FileNotFoundException>(() =>
            {
                ExamHelper.LoadQuestionsFromCSV(fakePath);
            });
        }
    }
}
