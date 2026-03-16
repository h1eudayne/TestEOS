using ExamClient;

namespace EOSClient.Tests
{

    [TestFixture]
    public class CredentialValidationTests
    {
        private string _testDataDir = null!;

        [OneTimeSetUp]
        public void Setup()
        {
            _testDataDir = Path.Combine(Path.GetTempPath(), "eos_cred_" + Guid.NewGuid().ToString("N")[..8]);
            Directory.CreateDirectory(_testDataDir);
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            if (Directory.Exists(_testDataDir))
                Directory.Delete(_testDataDir, true);
        }

        private string CreateCredentialCsv(string content, string fileName = "credentials.csv")
        {
            var path = Path.Combine(_testDataDir, fileName);
            File.WriteAllText(path, content);
            return path;
        }


        [Test]
        [Property("QaseTitle", "Load valid credentials CSV returns correct count")]
        public void LoadCredentials_ValidFile_ReturnsCorrectCount()
        {
            var csv = "examCode,username,password\n"
                    + "MATH001,student1,pass123\n"
                    + "MATH001,student2,pass456\n";
            var path = CreateCredentialCsv(csv);

            var creds = CredentialValidator.LoadCredentials(path);

            Assert.That(creds, Has.Count.EqualTo(2));
        }

        [Test]
        [Property("QaseTitle", "Load credentials CSV parses exam code correctly")]
        public void LoadCredentials_ValidFile_ParsesExamCode()
        {
            var csv = "examCode,username,password\nMATH001,student1,pass123\n";
            var path = CreateCredentialCsv(csv);

            var creds = CredentialValidator.LoadCredentials(path);

            Assert.That(creds[0].ExamCode, Is.EqualTo("MATH001"));
            Assert.That(creds[0].Username, Is.EqualTo("student1"));
            Assert.That(creds[0].Password, Is.EqualTo("pass123"));
        }

        [Test]
        [Property("QaseTitle", "Load credentials CSV with only header returns empty")]
        public void LoadCredentials_OnlyHeader_ReturnsEmpty()
        {
            var path = CreateCredentialCsv("examCode,username,password\n");

            var creds = CredentialValidator.LoadCredentials(path);

            Assert.That(creds, Is.Empty);
        }

        [Test]
        [Property("QaseTitle", "Load credentials with nonexistent path throws FileNotFoundException")]
        public void LoadCredentials_FileNotFound_ThrowsException()
        {
            var fakePath = Path.Combine(_testDataDir, "nonexistent.csv");

            Assert.Throws<FileNotFoundException>(() =>
            {
                CredentialValidator.LoadCredentials(fakePath);
            });
        }

        [Test]
        [Property("QaseTitle", "Load credentials with null path throws ArgumentNullException")]
        public void LoadCredentials_NullPath_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                CredentialValidator.LoadCredentials(null!);
            });
        }

        [Test]
        [Property("QaseTitle", "Load credentials skips malformed rows with fewer than 3 columns")]
        public void LoadCredentials_MalformedRows_SkipsInvalidRows()
        {
            var csv = "examCode,username,password\n"
                    + "MATH001,student1,pass123\n"
                    + "invalid_row\n"
                    + "MATH001,student2,pass456\n";
            var path = CreateCredentialCsv(csv);

            var creds = CredentialValidator.LoadCredentials(path);

            Assert.That(creds, Has.Count.EqualTo(2));
        }



        [Test]
        [Property("QaseTitle", "Valid credentials return true")]
        public void ValidateCredentials_ValidCredentials_ReturnsTrue()
        {
            var creds = new List<CredentialEntry>
            {
                new CredentialEntry { ExamCode = "MATH001", Username = "student1", Password = "pass123" }
            };

            var result = CredentialValidator.ValidateCredentials(creds, "MATH001", "student1", "pass123");

            Assert.That(result, Is.True);
        }

        [Test]
        [Property("QaseTitle", "Invalid password returns false")]
        public void ValidateCredentials_WrongPassword_ReturnsFalse()
        {
            var creds = new List<CredentialEntry>
            {
                new CredentialEntry { ExamCode = "MATH001", Username = "student1", Password = "pass123" }
            };

            var result = CredentialValidator.ValidateCredentials(creds, "MATH001", "student1", "WRONG");

            Assert.That(result, Is.False);
        }

        [Test]
        [Property("QaseTitle", "Username validation is case-insensitive")]
        public void ValidateCredentials_CaseInsensitiveUsername_ReturnsTrue()
        {
            var creds = new List<CredentialEntry>
            {
                new CredentialEntry { ExamCode = "MATH001", Username = "student1", Password = "pass123" }
            };

            var result = CredentialValidator.ValidateCredentials(creds, "MATH001", "STUDENT1", "pass123");

            Assert.That(result, Is.True);
        }

        [Test]
        [Property("QaseTitle", "ExamCode validation is case-insensitive")]
        public void ValidateCredentials_CaseInsensitiveExamCode_ReturnsTrue()
        {
            var creds = new List<CredentialEntry>
            {
                new CredentialEntry { ExamCode = "MATH001", Username = "student1", Password = "pass123" }
            };

            var result = CredentialValidator.ValidateCredentials(creds, "math001", "student1", "pass123");

            Assert.That(result, Is.True);
        }

        [Test]
        [Property("QaseTitle", "Password validation is case-sensitive")]
        public void ValidateCredentials_CaseSensitivePassword_ReturnsFalse()
        {
            var creds = new List<CredentialEntry>
            {
                new CredentialEntry { ExamCode = "MATH001", Username = "student1", Password = "pass123" }
            };

            var result = CredentialValidator.ValidateCredentials(creds, "MATH001", "student1", "Pass123");

            Assert.That(result, Is.False);
        }

        [Test]
        [Property("QaseTitle", "Empty credentials list returns false")]
        public void ValidateCredentials_EmptyList_ReturnsFalse()
        {
            var creds = new List<CredentialEntry>();

            var result = CredentialValidator.ValidateCredentials(creds, "MATH001", "student1", "pass123");

            Assert.That(result, Is.False);
        }

        [Test]
        [Property("QaseTitle", "Null credentials list throws ArgumentNullException")]
        public void ValidateCredentials_NullList_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                CredentialValidator.ValidateCredentials(null!, "MATH001", "student1", "pass123");
            });
        }


        [Test]
        [Property("QaseTitle", "ValidateFromFile with valid CSV and matching credentials returns true")]
        public void ValidateFromFile_ValidCsvAndCredentials_ReturnsTrue()
        {
            var csv = "examCode,username,password\nMATH001,student1,pass123\n";
            var path = CreateCredentialCsv(csv);

            var result = CredentialValidator.ValidateFromFile(path, "MATH001", "student1", "pass123");

            Assert.That(result, Is.True);
        }

        [Test]
        [Property("QaseTitle", "ValidateFromFile with valid CSV and wrong credentials returns false")]
        public void ValidateFromFile_ValidCsvWrongCredentials_ReturnsFalse()
        {
            var csv = "examCode,username,password\nMATH001,student1,pass123\n";
            var path = CreateCredentialCsv(csv);

            var result = CredentialValidator.ValidateFromFile(path, "MATH001", "student1", "wrong_pass");

            Assert.That(result, Is.False);
        }
    }
}
