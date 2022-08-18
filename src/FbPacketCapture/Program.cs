using System.Diagnostics;
using System.IO.Pipes;
using System.Net;

using AdysTech.CredentialManager;

using CommandLine;
using FboxSharp;

namespace FbPacketCapture
{

    internal class Program
    {
        private static void Main(string[] args)
        {
            ParserResult<CommandLineOptions> res = Parser.Default.ParseArguments<CommandLineOptions>(args);
            CommandLineOptions options = res.Value;
            if (res.Tag == ParserResultType.NotParsed)
            {
                string errors = string.Join(", ", res.Errors.Select(e => e.Tag.ToString()));
                Console.WriteLine("Commandline parser error(s): " + errors);
                return;
            }

            string wiresharkExePath = options.WiresharkExePath;
            if (!File.Exists(wiresharkExePath))
            {
                Console.WriteLine("WiresharkExePath - file not found: '" + wiresharkExePath + "'");
                return;
            }

            string credManTarget = options.CredManTarget;
            Console.WriteLine($"Using CredManTarget: '{credManTarget}'");

            NetworkCredential savedCredential = CredentialManager.GetCredentials(credManTarget);
            if (savedCredential == null)
            {
                // null: Credentials not found
                Console.WriteLine($"Credentials for Internet or network address '{credManTarget}' not found");

                string dialogCaption = "FbPacketCapture";
                string message = "Enter credentials of a FritzBox user account" + Environment.NewLine + "with permission to change FritzBox settings";

                bool credentialsShouldBeSaved = true;// false: 'save cred' checkbox will be hidden!
                NetworkCredential enteredCredentials = CredentialManager.PromptForCredentials(credManTarget, ref credentialsShouldBeSaved, message, dialogCaption);
                if (enteredCredentials == null)
                {
                    // user clicked 'cancel'
                    Console.WriteLine($"Credentials input was cancelled - stopping.");
                    return;
                }
                if (credentialsShouldBeSaved)
                {
                    CredentialManager.SaveCredentials(credManTarget, enteredCredentials);
                    Console.WriteLine($"Credentials have been saved.");
                }
                savedCredential = enteredCredentials;
            }

            Console.WriteLine("Press any key to exit after capture has stopped");
            Console.WriteLine("Getting session-id...");

            FboxClient fbClient = new FboxClient();

            string sessionId;
            try
            {
                sessionId = fbClient.GetSessionIdAsync(savedCredential.UserName, savedCredential.Password).GetAwaiter().GetResult();
                if (sessionId == string.Empty)
                {
                    Console.WriteLine("Received empty session-id - probably username is wrong");
                    return;
                }
            }
            catch (FboxSharpException ex)
            {
                Console.WriteLine("Unable to get a session-id: " + ExceptionMessages(ex));
                return;
            }

            CancellationTokenSource cts = new CancellationTokenSource();
            string interfaceName = "3-0";// "Routing-Schnittstelle" - see interface_names.txt
            Stream input = fbClient.GetPcapStreamAsync(sessionId, interfaceName, cts).GetAwaiter().GetResult();

            const string pipeName = "FritzCapturePipe";
            var output = new NamedPipeServerStream(pipeName); // Wireshark needs a named pipe
            Console.WriteLine("Starting Wireshark...");
            Process.Start(wiresharkExePath, @"-k -i\\.\pipe\" + pipeName);
            output.WaitForConnection();
            const int bufferSize = 81920;
            input.CopyToAsync(output, bufferSize, cts.Token);

            Console.ReadKey();
            cts.Cancel();
        }

        //recursive!
        private static string ExceptionMessages(Exception ex)
        {
            if (ex.InnerException == null)
            {
                return ex.Message;
            }

            return ex.Message + " / " + ExceptionMessages(ex.InnerException);
        }

    }
}
