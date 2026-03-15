using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using EncryptData;
using IRemote;

namespace MockEOSServer
{
    /// <summary>
    /// Mock EOS Server - starts a .NET Remoting TCP server
    /// that accepts any login credentials for development/testing.
    /// </summary>
    class Program
    {
        // Must match the encryption key used by AuthenticateForm
        private const string ENCRYPT_KEY = "04021976";
        // Must match AuthenticateForm.version field
        private const string VERSION_GUID = "C3AF3F4B-EA15-4EDA-9750-C0214649FEC8";

        static void Main(string[] args)
        {
            int port = 8080;
            if (args.Length > 0)
            {
                int.TryParse(args[0], out port);
            }

            Console.WriteLine("=========================================");
            Console.WriteLine("   Mock EOS Server v1.0");
            Console.WriteLine("   Accepts ANY login credentials");
            Console.WriteLine("=========================================");
            Console.WriteLine();

            // Check if MockExamClient.dll exists
            string guiDllPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MockExamClient.dll");
            if (!File.Exists(guiDllPath))
            {
                Console.WriteLine("[WARNING] MockExamClient.dll not found!");
                Console.WriteLine("[WARNING] Copy MockExamClient.dll to: " + AppDomain.CurrentDomain.BaseDirectory);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("[OK] MockExamClient.dll found (" + new FileInfo(guiDllPath).Length + " bytes)");
            }

            // Start .NET Remoting TCP server
            try
            {
                TcpChannel channel = new TcpChannel(port);
                ChannelServices.RegisterChannel(channel, false);

                // Register the mock server at endpoint "Server"
                // This matches AuthenticateForm.TryConnect: tcp://IP:Port/Server
                RemotingConfiguration.RegisterWellKnownServiceType(
                    typeof(MockRemoteServer),
                    "Server",
                    WellKnownObjectMode.Singleton);

                Console.WriteLine("[OK] Server started on tcp://0.0.0.0:" + port + "/Server");
                Console.WriteLine();

                // Generate EOS_Server_Info.dat for localhost
                GenerateServerInfoFile(port);

                Console.WriteLine();
                Console.WriteLine("Waiting for connections... (Press Enter to stop)");
                Console.WriteLine("=========================================");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR] Failed to start server: " + ex.Message);
                Console.WriteLine();
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Auto-generate EOS_Server_Info.dat for localhost connection
        /// </summary>
        static void GenerateServerInfoFile(int port)
        {
            try
            {
                ServerInfo si = new ServerInfo();
                si.IP = "127.0.0.1";
                si.Port = port;
                si.PortName = "Mock Server (localhost)";
                si.Public_IP = "";
                si.Version = VERSION_GUID;

                // Disable other endpoints
                si.SecondaryPort = -1;
                si.ThirdPort = -1;
                si.FourthPort = -1;
                si.FifthPort = -1;

                byte[] data = EncryptSupport.ObjectToByteArray(si);
                string outputFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EOS_Server_Info.dat");
                EncryptSupport.EncryptQuestions_SaveToFile(outputFile, data, ENCRYPT_KEY);

                Console.WriteLine("[OK] Generated EOS_Server_Info.dat (copy this to EOSClient folder)");
                Console.WriteLine("     Location: " + Path.GetFullPath(outputFile));

                // Auto-copy to EOSClient output directory
                try
                {
                    string eosClientDir = Path.Combine(
                        Path.Combine(
                            Path.Combine(
                                Path.Combine(
                                    Path.Combine(
                                        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".."),
                                    ".."),
                                ".."),
                            "EOSClient"),
                        "bin"),
                    "Debug");
                    eosClientDir = Path.GetFullPath(eosClientDir);
                    if (Directory.Exists(eosClientDir))
                    {
                        string destFile = Path.Combine(eosClientDir, "EOS_Server_Info.dat");
                        File.Copy(outputFile, destFile, true);
                        Console.WriteLine("[OK] Auto-copied EOS_Server_Info.dat to EOSClient: " + destFile);
                    }
                    else
                    {
                        Console.WriteLine("[INFO] EOSClient directory not found at: " + eosClientDir);
                        Console.WriteLine("[INFO] Please manually copy EOS_Server_Info.dat to EOSClient bin folder.");
                    }
                }
                catch (Exception copyEx)
                {
                    Console.WriteLine("[WARNING] Could not auto-copy to EOSClient: " + copyEx.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[WARNING] Could not auto-generate EOS_Server_Info.dat: " + ex.Message);
            }
        }
    }
}
