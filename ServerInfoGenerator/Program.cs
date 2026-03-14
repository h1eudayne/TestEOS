using System;
using System.IO;
using EncryptData;
using IRemote;

namespace ServerInfoGenerator
{
    /// <summary>
    /// Console tool to generate EOS_Server_Info.dat with custom server configuration.
    /// Usage: ServerInfoGenerator.exe [outputPath]
    /// If no outputPath specified, generates in current directory.
    /// </summary>
    class Program
    {
        // Encryption key must match the one used by AuthenticateForm (DES 8-byte key)
        private const string ENCRYPT_KEY = "04021976";

        // Version GUID must match AuthenticateForm.version field
        private const string VERSION_GUID = "C3AF3F4B-EA15-4EDA-9750-C0214649FEC8";

        static void Main(string[] args)
        {
            Console.WriteLine("=== EOS Server Info Generator ===");
            Console.WriteLine("This tool generates EOS_Server_Info.dat for EOSClient login.");
            Console.WriteLine();

            // Determine output path
            string outputDir = (args.Length > 0) ? args[0] : ".";
            string outputFile = Path.Combine(outputDir, "EOS_Server_Info.dat");

            // Collect server configuration from user
            ServerInfo si = CollectServerInfo();

            // Serialize and encrypt
            try
            {
                byte[] data = EncryptSupport.ObjectToByteArray(si);
                bool success = EncryptSupport.EncryptQuestions_SaveToFile(outputFile, data, ENCRYPT_KEY);

                if (success)
                {
                    Console.WriteLine();
                    Console.WriteLine("[OK] File generated successfully: " + Path.GetFullPath(outputFile));
                    Console.WriteLine("[OK] File size: " + new FileInfo(outputFile).Length + " bytes");
                    Console.WriteLine();
                    PrintSummary(si);
                }
                else
                {
                    Console.WriteLine("[ERROR] Failed to generate file.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR] " + ex.Message);
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static ServerInfo CollectServerInfo()
        {
            ServerInfo si = new ServerInfo();

            // Set required version GUID
            si.Version = VERSION_GUID;

            Console.WriteLine("--- Primary Server (Required) ---");
            si.IP = ReadInput("  Server IP", "192.168.1.100");
            si.Port = ReadIntInput("  Server Port", 8080);
            si.PortName = ReadInput("  Display Name", "EOS Server 1");
            si.Public_IP = ReadInput("  Public IP (empty if same LAN)", "");

            Console.WriteLine();
            bool addMore = ReadYesNo("Add Secondary Server?", false);
            if (addMore)
            {
                si.SecondaryIP = ReadInput("  Secondary IP", "");
                si.SecondaryPort = ReadIntInput("  Secondary Port", -1);
                si.SecondPortName = ReadInput("  Display Name", "EOS Server 2");
                si.Secondary_Public_IP = ReadInput("  Public IP", "");

                addMore = ReadYesNo("Add Third Server?", false);
                if (addMore)
                {
                    si.ThirdIP = ReadInput("  Third IP", "");
                    si.ThirdPort = ReadIntInput("  Third Port", -1);
                    si.ThirdPortName = ReadInput("  Display Name", "EOS Server 3");
                    si.Third_Public_IP = ReadInput("  Public IP", "");

                    addMore = ReadYesNo("Add Fourth Server?", false);
                    if (addMore)
                    {
                        si.FourthIP = ReadInput("  Fourth IP", "");
                        si.FourthPort = ReadIntInput("  Fourth Port", -1);
                        si.FourthPortName = ReadInput("  Display Name", "EOS Server 4");
                        si.Fourth_Public_IP = ReadInput("  Public IP", "");

                        addMore = ReadYesNo("Add Fifth Server?", false);
                        if (addMore)
                        {
                            si.FifthIP = ReadInput("  Fifth IP", "");
                            si.FifthPort = ReadIntInput("  Fifth Port", -1);
                            si.FifthPortName = ReadInput("  Display Name", "EOS Server 5");
                            si.Fifth_Public_IP = ReadInput("  Public IP", "");
                        }
                        else
                        {
                            si.FifthPort = -1;
                        }
                    }
                    else
                    {
                        si.FourthPort = -1;
                        si.FifthPort = -1;
                    }
                }
                else
                {
                    si.ThirdPort = -1;
                    si.FourthPort = -1;
                    si.FifthPort = -1;
                }
            }
            else
            {
                si.SecondaryPort = -1;
                si.ThirdPort = -1;
                si.FourthPort = -1;
                si.FifthPort = -1;
            }

            return si;
        }

        static string ReadInput(string prompt, string defaultValue)
        {
            if (!string.IsNullOrEmpty(defaultValue))
                Console.Write(prompt + " [" + defaultValue + "]: ");
            else
                Console.Write(prompt + ": ");

            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
                return defaultValue;
            return input.Trim();
        }

        static int ReadIntInput(string prompt, int defaultValue)
        {
            string val = ReadInput(prompt, defaultValue.ToString());
            int result;
            if (int.TryParse(val, out result))
                return result;
            return defaultValue;
        }

        static bool ReadYesNo(string prompt, bool defaultValue)
        {
            string def = defaultValue ? "Y" : "N";
            Console.Write(prompt + " [" + def + "]: ");
            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
                return defaultValue;
            return input.Trim().ToUpper().StartsWith("Y");
        }

        static void PrintSummary(ServerInfo si)
        {
            Console.WriteLine("=== Configuration Summary ===");
            Console.WriteLine("  Version:    " + si.Version);
            Console.WriteLine("  Primary:    " + si.IP + ":" + si.Port + " (" + si.PortName + ")");
            if (si.SecondaryPort != -1)
                Console.WriteLine("  Secondary:  " + si.SecondaryIP + ":" + si.SecondaryPort + " (" + si.SecondPortName + ")");
            if (si.ThirdPort != -1)
                Console.WriteLine("  Third:      " + si.ThirdIP + ":" + si.ThirdPort + " (" + si.ThirdPortName + ")");
            if (si.FourthPort != -1)
                Console.WriteLine("  Fourth:     " + si.FourthIP + ":" + si.FourthPort + " (" + si.FourthPortName + ")");
            if (si.FifthPort != -1)
                Console.WriteLine("  Fifth:      " + si.FifthIP + ":" + si.FifthPort + " (" + si.FifthPortName + ")");
        }
    }
}
