using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FboxSharp
{
	public enum LogCategory
	{
		Unknown = 0,
		System = 1,
		Internet = 2,
		Telephone = 3,
		Wifi = 4,
		Usb = 5
	}


	[Serializable]
	public class FboxLogEntry
	{

		public DateTime Timestamp { get; init; }
		public string LogMessage { get; init; } = String.Empty;
		public LogCategory Category { get; init; }=LogCategory.Unknown;
		public int EventTypeId { get; init; }=0;


		public static readonly string cFboxTimestampFormat = "dd.MM.yy HH:mm:ss";
		public static readonly string cLogFileTimestampFormat = "yyyy-MM-dd HH:mm:ss";


		public static IReadOnlyList<FboxLogEntry> ParseAllLogEntriesJson(string allLogEntriesJson)
		{
			JsonDocument doc = JsonDocument.Parse(allLogEntriesJson);
			JsonElement[] entries = doc.RootElement.GetProperty("mq_log").EnumerateArray().ToArray(); // use ToArray, otherwise problems/exceptions might be hard to debug
			List<FboxLogEntry> logEntries = new List<FboxLogEntry>();
			foreach (JsonElement jsonElement in entries)
			{
				JsonElement[] parts = jsonElement.EnumerateArray().ToArray();

				int.TryParse(parts[2].GetString(), out int lc);
				LogCategory logCategory = FboxLogEntry.LogCategoryFromInt(lc);

				string? timeAndMessage = parts[0].GetString();
				if (timeAndMessage == null)
				{
					continue;
				}
				int.TryParse(parts[1].GetString(), out int eventTypeId);

				logEntries.Add(FboxLogEntry.CreateFromFritzLogMessage(timeAndMessage, logCategory, eventTypeId));
			}
			return logEntries.AsReadOnly();
		}

		public static FboxLogEntry CreateFromFritzLogLine(string line)
		{
			return CreateFromFritzLogMessage(line, LogCategory.Unknown,0);
		}

		public static FboxLogEntry CreateFromFritzLogMessage(string message, LogCategory category, int eventTypeId)
		{
			// e.g.: '15.05.19 05:38:42 IPv6-Präfix wurde erfolgreich bezogen. Neues Präfix: 2abc:4a0:d078:3ba0::/60'
			string timestampString = message.Substring(0, cFboxTimestampFormat.Length);
			string logTextString = message.Substring(cFboxTimestampFormat.Length).Trim();
			var result = new FboxLogEntry()
			{
				Timestamp = DateTime.ParseExact(timestampString, cFboxTimestampFormat, CultureInfo.InvariantCulture),
				LogMessage = logTextString,
				Category=category,
				EventTypeId=eventTypeId
			};
			return result;
		}

		// has to match format of ToLogFileLine
		//public static FboxLogEntry CreateFromLogFileLine(string line)
		//{
		//	string timestampString = line.Substring(0, cLogFileTimestampFormat.Length);
		//	string logTextString = line.Substring(cLogFileTimestampFormat.Length).Trim();
		//	var result = new FboxLogEntry()
		//	{
		//		Timestamp = DateTime.ParseExact(timestampString, cLogFileTimestampFormat, CultureInfo.InvariantCulture),
		//		LogMessage = logTextString
		//	};
		//	return result;
		//}

		// has to match format of CreateFromLogFileLine
		//public string ToLogFileLine()
		//{
		//	return $"{Timestamp.ToString(cLogFileTimestampFormat)} {LogMessage}";
		//}

		public override string ToString()
		{
			return $"{Timestamp} {LogMessage}";
		}

		//public bool IsEntryNewer(FritzLogEntry lastExistingEntry)
		//{
		//	var diffSeconds = (this.Timestamp - lastExistingEntry.Timestamp).TotalSeconds;
		//	if (diffSeconds < -1.6)
		//	{
		//		return false;
		//	}
		//	else if (diffSeconds > 1.6)
		//	{
		//		return true;
		//	}

		//	return this.LogText != lastExistingEntry.LogText;
		//}

		// FritzBox sometimes changes the timestamp (randomly?) of an entry between multiple queries by +/- 1second
		public bool IsSameEntry(FboxLogEntry lastOldEntry)
		{
			double diffSeconds = (this.Timestamp - lastOldEntry.Timestamp).TotalSeconds;
			return Math.Abs(diffSeconds) < 1.6 && this.LogMessage == lastOldEntry.LogMessage;
		}

		public static LogCategory LogCategoryFromInt(int logCategory)
		{
			if (logCategory < (int)LogCategory.System || logCategory > (int)LogCategory.Usb)
			{
				return LogCategory.Unknown;
			}
			return (LogCategory)logCategory;
		}

		// currentFritzBoxEntries must be sorted by timestamp (ascending)
		public static int CalculateNumberOfOldEntries(IEnumerable<FboxLogEntry> currentFritzBoxEntries, FboxLogEntry lastOldEntry)
		{
			int result = 0;
			int currentIndex = 0;
			foreach (FboxLogEntry logEntry in currentFritzBoxEntries)
			{
				++currentIndex;
				if (logEntry.IsSameEntry(lastOldEntry))
				{
					result = currentIndex;
				}
			}
			return result;
		}

	}
}
