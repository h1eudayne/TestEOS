using System;
using System.Collections.Generic;
using System.IO;

namespace ExamClient
{
    /// <summary>
    /// Represents a single credential entry from the CSV file.
    /// </summary>
    public class CredentialEntry
    {
        public string ExamCode { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    /// <summary>
    /// Handles credential validation against a CSV-based credential store.
    /// Extracted from MockRemoteServer to enable unit testing without IRemote dependency.
    /// </summary>
    public static class CredentialValidator
    {
        /// <summary>
        /// Load credentials from a CSV file.
        /// Format: examCode,username,password (first row is header, skipped).
        /// </summary>
        /// <param name="csvPath">Path to the credentials CSV file.</param>
        /// <returns>List of parsed CredentialEntry objects.</returns>
        /// <exception cref="ArgumentNullException">Thrown when csvPath is null or empty.</exception>
        /// <exception cref="FileNotFoundException">Thrown when csvPath does not exist.</exception>
        public static List<CredentialEntry> LoadCredentials(string csvPath)
        {
            if (string.IsNullOrEmpty(csvPath))
                throw new ArgumentNullException("csvPath");

            if (!File.Exists(csvPath))
                throw new FileNotFoundException("Credentials file not found: " + csvPath, csvPath);

            var credentials = new List<CredentialEntry>();
            string[] lines = File.ReadAllLines(csvPath);

            // Skip header (first line)
            for (int i = 1; i < lines.Length; i++)
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

            return credentials;
        }

        /// <summary>
        /// Validate a set of credentials against a list of known entries.
        /// examCode and username are case-insensitive; password is case-sensitive.
        /// </summary>
        /// <param name="credentials">List of valid credential entries.</param>
        /// <param name="examCode">Exam code to validate.</param>
        /// <param name="username">Username to validate.</param>
        /// <param name="password">Password to validate.</param>
        /// <returns>True if credentials match an entry; false otherwise.</returns>
        public static bool ValidateCredentials(
            List<CredentialEntry> credentials,
            string examCode,
            string username,
            string password)
        {
            if (credentials == null) throw new ArgumentNullException("credentials");

            foreach (var entry in credentials)
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
        /// Load credentials from CSV and validate in one step.
        /// </summary>
        public static bool ValidateFromFile(string csvPath, string examCode, string username, string password)
        {
            var credentials = LoadCredentials(csvPath);
            return ValidateCredentials(credentials, examCode, username, password);
        }
    }
}
