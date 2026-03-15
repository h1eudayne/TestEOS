using System;
using System.Collections.Generic;
using System.IO;
using IRemote;
using QuestionLib;

namespace MockEOSServer
{
    /// <summary>
    /// Mock implementation of IRemoteServer with CSV-based credential validation.
    /// Returns RegisterStatus.NEW if credentials match, LOGIN_FAILED otherwise.
    /// </summary>
    public class MockRemoteServer : MarshalByRefObject, IRemoteServer
    {
        private static byte[] cachedGuiData = null;
        private static int cachedOriginSize = 0;

        // Cached credentials from CSV file
        private static List<CredentialEntry> credentials = null;
        private static readonly object credLock = new object();

        public bool Ping()
        {
            return true;
        }

        public EOSData ConductExam(RegisterData rd)
        {
            Console.WriteLine("[LOGIN] User: " + rd.Login + " | ExamCode: " + rd.ExamCode + " | Machine: " + rd.Machine + " | Version: " + rd.ClientVersion);

            EOSData ed = new EOSData();

            // Validate credentials against CSV file
            if (!ValidateCredentials(rd.ExamCode, rd.Login, rd.Password))
            {
                Console.WriteLine("[DENIED] Invalid credentials for user: " + rd.Login);
                ed.Status = RegisterStatus.LOGIN_FAILED;
                return ed;
            }

            Console.WriteLine("[OK] Credentials validated for user: " + rd.Login);
            ed.Status = RegisterStatus.NEW;

            // Load and compress the MockExamClient.dll as GUI assembly
            try
            {
                if (cachedGuiData == null)
                {
                    string guiDllPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MockExamClient.dll");
                    if (!File.Exists(guiDllPath))
                    {
                        Console.WriteLine("[ERROR] MockExamClient.dll not found at: " + guiDllPath);
                        Console.WriteLine("[ERROR] Make sure MockExamClient.dll is in the same directory as MockEOSServer.exe");
                        ed.Status = RegisterStatus.REGISTER_ERROR;
                        return ed;
                    }

                    byte[] rawDll = File.ReadAllBytes(guiDllPath);
                    cachedOriginSize = rawDll.Length;
                    cachedGuiData = GZipHelper.Compress(rawDll);
                    Console.WriteLine("[OK] MockExamClient.dll loaded (" + cachedOriginSize + " bytes -> " + cachedGuiData.Length + " compressed)");
                }

                ed.GUI = cachedGuiData;
                ed.OriginSize = cachedOriginSize;
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR] Failed to load GUI: " + ex.Message);
                ed.Status = RegisterStatus.REGISTER_ERROR;
                return ed;
            }

            Console.WriteLine("[OK] Login accepted -> Status: NEW");
            return ed;
        }

        public SubmitStatus Submit(SubmitPaper submitPaper, ref string msg)
        {
            Console.WriteLine("[SUBMIT] Received submission");
            msg = "OK";
            return SubmitStatus.OK;
        }

        // Keep the remote object alive indefinitely
        public override object InitializeLifetimeService()
        {
            return null;
        }

        // ===================== CREDENTIAL VALIDATION =====================

        /// <summary>
        /// Validate credentials against the credentials.csv file.
        /// CSV format: examCode,username,password
        /// </summary>
        public static bool ValidateCredentials(string examCode, string username, string password)
        {
            LoadCredentials();

            foreach (CredentialEntry entry in credentials)
            {
                if (string.Equals(entry.ExamCode, examCode, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(entry.Username, username, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(entry.Password, password, StringComparison.Ordinal))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Load credentials from CSV file. Thread-safe with caching.
        /// </summary>
        private static void LoadCredentials()
        {
            lock (credLock)
            {
                // Reload every time to pick up changes
                credentials = new List<CredentialEntry>();
                string csvPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "credentials.csv");

                if (!File.Exists(csvPath))
                {
                    Console.WriteLine("[WARNING] credentials.csv not found at: " + csvPath);
                    Console.WriteLine("[WARNING] No logins will be accepted!");
                    return;
                }

                string[] lines;
                using (var fs = new FileStream(csvPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var reader = new StreamReader(fs))
                {
                    var allText = reader.ReadToEnd();
                    lines = allText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                }
                for (int i = 1; i < lines.Length; i++) // Skip header row
                {
                    string line = lines[i].Trim();
                    if (string.IsNullOrEmpty(line)) continue;

                    string[] parts = line.Split(',');
                    if (parts.Length >= 3)
                    {
                        credentials.Add(new CredentialEntry
                        {
                            ExamCode = parts[0].Trim(),
                            Username = parts[1].Trim(),
                            Password = parts[2].Trim()
                        });
                    }
                }
                Console.WriteLine("[OK] Loaded " + credentials.Count + " credential(s) from credentials.csv");
            }
        }
    }

    /// <summary>
    /// Simple data class for storing credential entries from CSV.
    /// </summary>
    public class CredentialEntry
    {
        public string ExamCode { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
