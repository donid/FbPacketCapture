using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommandLine;

namespace FbPacketCapture
{
	public class CommandLineOptions
	{

		//[Value(0, MetaName = "EncryptedPasswordFromBackupFile")]
		[Option('t', "CredManTarget", Required = false, Default = "FbPacketCapture_User_FritzBox", HelpText = "Target-name of the credentials stored in Windows Credential-Manager to be used for FritzBox log-in")]
		public string CredManTarget { get; } = "FbPacketCapture_User_FritzBox";

		[Option('w', "WiresharkExePath", Required = false, Default = @"C:\Program Files\Wireshark\Wireshark.exe", HelpText = "Full path to 'Wireshark.exe'")]
		public string WiresharkExePath { get; } = @"C:\Program Files\Wireshark\Wireshark.exe";


		public CommandLineOptions(string credManTarget, string wiresharkExePath)
		{
			CredManTarget = credManTarget;
			WiresharkExePath = wiresharkExePath;
		}


		/*
		[Usage(ApplicationAlias = "yourapp")]
		public static IEnumerable<Example> Examples
		{
			get
			{
				return new List<Example>()
				{
					new Example(
						"example description",
						new CommandLineOptions("hjhg/(=", @"c:\wordlist.txt")
						)
				};
			}
		}
		*/

	}
}
