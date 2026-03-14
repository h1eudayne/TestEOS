using ExamClient;

namespace EOSClient.Tests
{

    [TestFixture]
    public class ScoringLogicTests
    {
        private List<Question> _sampleQuestions = null!;

        [SetUp]
        public void Setup()
        {
            _sampleQuestions = new List<Question>
            {
                new Question { Text = "Q1", OptionA = "A", OptionB = "B", OptionC = "C", OptionD = "D", CorrectAnswer = "A" },
                new Question { Text = "Q2", OptionA = "A", OptionB = "B", OptionC = "C", OptionD = "D", CorrectAnswer = "B" },
                new Question { Text = "Q3", OptionA = "A", OptionB = "B", OptionC = "C", OptionD = "D", CorrectAnswer = "C" },
                new Question { Text = "Q4", OptionA = "A", OptionB = "B", OptionC = "C", OptionD = "D", CorrectAnswer = "D" },
                new Question { Text = "Q5", OptionA = "A", OptionB = "B", OptionC = "C", OptionD = "D", CorrectAnswer = "A" },
            };
        }

        [Test]
        [Property("QaseTitle", "All correct answers returns 5/5")]
        public void CalculateScore_AllCorrect_ReturnsFullScore()
        {

            int[] answers = { 0, 1, 2, 3, 0 };

            var result = ExamHelper.CalculateScore(_sampleQuestions, answers);

            Assert.That(result.Correct, Is.EqualTo(5));
            Assert.That(result.Wrong, Is.EqualTo(0));
            Assert.That(result.TotalQuestions, Is.EqualTo(5));
        }

        [Test]
        [Property("QaseTitle", "All wrong answers returns 0 correct")]
        public void CalculateScore_AllWrong_Returns0Correct()
        {
            int[] answers = { 1, 0, 0, 0, 1 };

            var result = ExamHelper.CalculateScore(_sampleQuestions, answers);

            Assert.That(result.Correct, Is.EqualTo(0));
            Assert.That(result.Wrong, Is.EqualTo(5));
        }

        [Test]
        [Property("QaseTitle", "Partial correct answers returns accurate count")]
        public void CalculateScore_PartialCorrect_ReturnsAccurate()
        {

            int[] answers = { 0, 1, 2, 0, 1 };

            var result = ExamHelper.CalculateScore(_sampleQuestions, answers);

            Assert.That(result.Correct, Is.EqualTo(3));
            Assert.That(result.Wrong, Is.EqualTo(2));
        }

        [Test]
        [Property("QaseTitle", "Score with student name and exam code")]
        public void CalculateScore_WithStudentInfo_ContainsMetadata()
        {
            int[] answers = { 0, 1, 2, 3, 0 };

            var result = ExamHelper.CalculateScore(_sampleQuestions, answers, "NguyenVanA", "ENG001");

            Assert.That(result.StudentName, Is.EqualTo("NguyenVanA"));
            Assert.That(result.ExamCode, Is.EqualTo("ENG001"));
            Assert.That(result.Correct, Is.EqualTo(5));
        }

        [Test]
        [Property("QaseTitle", "Empty question list returns 0")]
        public void CalculateScore_EmptyQuestions_Returns0()
        {
            var emptyQuestions = new List<Question>();
            int[] answers = Array.Empty<int>();

            var result = ExamHelper.CalculateScore(emptyQuestions, answers);

            Assert.That(result.Correct, Is.EqualTo(0));
            Assert.That(result.TotalQuestions, Is.EqualTo(0));
        }
    }
}
