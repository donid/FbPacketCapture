using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using AdysTech.CredentialManager;
using CsvHelper;
using CsvHelper.Configuration;
using FboxSharp;

namespace UnitTests
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestWebClient()
		{
			FboxConnectionSettings loginData = new FboxConnectionSettings("","");
			FboxUrlBuilder urlBuilder = new FboxUrlBuilder(loginData);

#pragma warning disable SYSLIB0014 // Type or member is obsolete
			WebClient wc = new WebClient();
#pragma warning restore SYSLIB0014 // Type or member is obsolete

			wc.BaseAddress = urlBuilder.GetBaseUrl().ToString();
			string result = wc.DownloadString(loginData.Path);
			string result2 = wc.DownloadString(new Uri(loginData.Path,UriKind.Relative));
			// despite of the misleading exception, this ctor always assumes UriKind.Absolute !!!
			string result3 = wc.DownloadString(new Uri("/"+loginData.Path));

		}


		[TestMethod]
		public void TestSessionInfoEmpty()
		{
			// this is the response-xml when we don't specify the 'sid' parameter, or when we send an invalid SID
			string xml = File.ReadAllText("SessionInfo_empty_SID.xml");
			FboxSessionInfo si = new FboxSessionInfo(xml);
			var ri = si.Rights;
			var usrs = si.Users;
		}

		[TestMethod]
		public void TestSessionInfoValid()
		{
			string xml = File.ReadAllText("SessionInfo_valid_SID.xml");
			FboxSessionInfo si = new FboxSessionInfo(xml);
			var ri = si.Rights;
			var usrs = si.Users;
		}

		[TestMethod]
		public void TestAvmLoginCode()
		{
			NetworkCredential cred = GetCredentials();
			var settings = new FboxConnectionSettings(cred.UserName, cred.Password);
			FboxClient fritzLogin = new FboxClient(settings);
			string sid= fritzLogin.CreateNewSession();


			FboxSessionInfo resp = fritzLogin.TerminateSession(sid);
		}



		[TestMethod]
		public void TestReadLastEventsFromCsv()
		{
			const string csvFilePath = @"d:\fritz_eventlog.csv";
			var lastEntries = LogEntriesCsvHelper.ReadLastEntries(csvFilePath, 6);
		}

		[TestMethod]
		public void TestReadEventsFromCsv()
		{
			const string csvFilePath = @"d:\fritz_eventlog.csv";
			var entries = LogEntriesCsvHelper.ReadAllEntries(csvFilePath).ToList();
		}


		[TestMethod]
		public void TestWriteOnlyNewEventsToCsv()
		{
			string log = File.ReadAllText(@"d:\fritzbox_logentries_lua_response_example_new.json");
			var entries = FboxLogEntry.ParseAllLogEntriesJson(log);

			var currentFritzBoxEntries = entries.OrderBy(item => item.Timestamp).ToList();
			const string csvFilePath = @"d:\fritz_eventlog.csv";

			LogEntriesCsvHelper.AppendOnlyNewEntries(csvFilePath, currentFritzBoxEntries);
		}


		[TestMethod]
		public void TestWriteEventsToCsv()
		{
			string log = File.ReadAllText(@"d:\fritzbox_logentries_lua_response_example_new.json");
			var entries = FboxLogEntry.ParseAllLogEntriesJson(log);

			var ascendingNewEntries = entries.OrderBy(item => item.Timestamp).ToList();
			const string csvfilePath = @"d:\fritz_eventlog.csv";

			LogEntriesCsvHelper.AppendAllEntries(csvfilePath, ascendingNewEntries);
		}

		[TestMethod]
		public async Task TestGetAllLogEntriesAsync()
		{
			var cred = GetCredentials();
			FboxConnectionSettings loginData = new FboxConnectionSettings(cred.UserName, cred.Password);
			FboxClient client = new FboxClient(loginData);
			string sid = await client.GetSessionIdAsync();

			FboxSessionInfo info = await client.GetSessionInfoForSidAsync(sid);

			string log = await client.GetAllLogEntriesJsonAsync(sid);


			// todo (do not add private IP-addresses, usernames, hostnames, telephone-numbers to git repository!)
			//File.WriteAllText(@"d:\fritzbox_logentries_lua_response_example_new.json", log);
			//string log = await File.ReadAllTextAsync(@"d:\fritzbox_logentries_lua_response_example.json");

			var entries = FboxLogEntry.ParseAllLogEntriesJson(log);
			foreach (FboxLogEntry le in entries)
			{
				Debug.WriteLine($"{le.EventTypeId};{le.Category};{le.Timestamp};{le.LogMessage}");
			}
		}


		[TestMethod]
		public void TestReverseNameLookup()
		{
			var he1 = Dns.GetHostEntry("9.9.9.9");
			var he2 = Dns.GetHostEntry("8.8.8.8");
			//var he3 = Dns.GetHostEntry("");
			var he4 = Dns.GetHostEntry("192.168.178.41");

			// SocketException var he5 = Dns.GetHostEntry("1.2.3.4");
		}

		[TestMethod]
		public void TestEnumerateCredentials()
		{
			//var cred = new NetworkCredential("delete_me_User", "secrettesttext");
			var all = CredentialManager.EnumerateCredentials();
		}


		private static NetworkCredential GetCredentials()
		{
			return CredentialManager.GetCredentials("FbPacketCapture_User_FritzBox");
		}
	}
}
