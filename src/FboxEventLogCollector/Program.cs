using System.ComponentModel;
using AdysTech.CredentialManager;
using FboxSharp;
using Microsoft.Extensions.Configuration;

namespace FboxEventLogCollector
{
	internal class Program
	{
		static async Task Main(string[] args)
		{
			Console.WriteLine("FboxEventLogCollector started...");


			IConfiguration config = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json")
				//.AddEnvironmentVariables()
				.Build();

			string credManTarget = config["CredManTarget"];
			string csvFilePath = config["TargetFilePath"];

			var cred = CredentialManager.GetCredentials(credManTarget);
			if (cred == null)
			{
				Console.WriteLine($"Credentials for target '{credManTarget}' not found!");
				Environment.Exit(-1);
			}

			FboxConnectionSettings loginData = new FboxConnectionSettings(cred.UserName, cred.Password);
			FboxClient client = new FboxClient(loginData);
			string sid = String.Empty;
			try
			{
				sid = await client.GetSessionIdAsync();
			}
			catch (FboxSharpException ex)
			{
				Console.WriteLine(ex.Message);
				Environment.Exit(-1);
			}

			string log = await client.GetAllLogEntriesJsonAsync(sid);
			var entries = FboxLogEntry.ParseAllLogEntriesJson(log);
			var ascendingNewEntries = entries.OrderBy(item => item.Timestamp).ToList();
			LogEntriesCsvHelper.AppendOnlyNewEntries(csvFilePath, ascendingNewEntries);

			Console.WriteLine("finished successfully");

		}
	}
}
