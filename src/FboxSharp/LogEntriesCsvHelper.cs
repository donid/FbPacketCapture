using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using FboxSharp;
using MoreLinq;

namespace FboxSharp
{
	public class LogEntriesCsvHelper
	{
		public static CsvConfiguration BuildCsvConfiguration()
		{
			CsvConfiguration configuration = new CsvConfiguration(CultureInfo.InvariantCulture);
			configuration.Delimiter = ";";
			configuration.ShouldQuote = (ShouldQuoteArgs a) => a.FieldType == typeof(string);
			return configuration;
		}

		public static IEnumerable<FboxLogEntry> ReadAllEntries(string csvFilePath)
		{
			CsvConfiguration configuration = BuildCsvConfiguration();
			using (FileStream fileStream = File.OpenRead(csvFilePath))
			using (StreamReader reader = new StreamReader(fileStream))
			using (CsvReader csv = new CsvReader(reader, configuration))
			{
				// cannot extract this to a separate method -> Dispose would be called before enumeration has finished!
				while (csv.Read())
				{
					FboxLogEntry fboxLogEntry = ReadEntry(csv);
					yield return fboxLogEntry;
				}
			}
		}

		// assumes no newlines are embedded in string-fields between the quotes (multiline text) !!!
		// if file ends with empty line(s), the number of returned entries can be smaller than numRawLines
		// or if the file does not contain enough lines / entries
		public static IReadOnlyCollection<FboxLogEntry> ReadLastEntries(string csvFilePath, int numRawLines)
		{
			IEnumerable<string> lines = File.ReadLines(csvFilePath);
			var lastLines = lines.TakeLast(numRawLines);// needs MoreLinq in netstandard2.0 - netcore contains this method out-of-the-box
			string extractedLines = string.Join(Environment.NewLine, lastLines);

			CsvConfiguration configuration = BuildCsvConfiguration();

			List<FboxLogEntry> result = new List<FboxLogEntry>();
			using (StringReader reader = new StringReader(extractedLines))
			using (CsvReader csv = new CsvReader(reader, configuration))
			{
				while (csv.Read())
				{
					FboxLogEntry fboxLogEntry = ReadEntry(csv);
					result.Add(fboxLogEntry);
				}
			}
			return result.AsReadOnly();
		}


		public static void AppendOnlyNewEntries(string csvFilePath, IEnumerable<FboxLogEntry> currentFritzBoxEntries)
		{
			FboxLogEntry? lastOldEntry = null;
			if (File.Exists(csvFilePath))
			{
				lastOldEntry = LogEntriesCsvHelper.ReadLastEntries(csvFilePath,10).LastOrDefault();
			}
			IEnumerable<FboxLogEntry> entriesToAdd = GetOnlyNewEntries(currentFritzBoxEntries, lastOldEntry);
			AppendAllEntries(csvFilePath, entriesToAdd);
		}

		public static IEnumerable<FboxLogEntry> GetOnlyNewEntries(IEnumerable<FboxLogEntry> currentFritzBoxEntries, FboxLogEntry? lastOldEntry)
		{
			int numberOfOldEntries = 0;
			if (lastOldEntry != null)
			{
				numberOfOldEntries = FboxLogEntry.CalculateNumberOfOldEntries(currentFritzBoxEntries, lastOldEntry);
			}
			IEnumerable<FboxLogEntry> entriesToAdd = currentFritzBoxEntries.Skip(numberOfOldEntries);
			return entriesToAdd;
		}

		public static void AppendAllEntries(string csvfilePath, IEnumerable<FboxLogEntry> ascendingNewEntries)
		{
			CsvConfiguration configuration = BuildCsvConfiguration();
			using (var writer = new StreamWriter(csvfilePath, true, Encoding.UTF8))
			using (var csv = new CsvWriter(writer, configuration))
			{
				foreach (FboxLogEntry fboxLogEntry in ascendingNewEntries)
				{
					LogEntriesCsvHelper.WriteEntry(csv, fboxLogEntry);
					csv.NextRecord();
				}
			}
		}

		public static void WriteEntry(CsvWriter csv, FboxLogEntry fboxLogEntry)
		{
			csv.WriteField(fboxLogEntry.Timestamp.ToString(FboxLogEntry.cLogFileTimestampFormat));
			csv.WriteField((int)fboxLogEntry.Category);
			csv.WriteField(fboxLogEntry.EventTypeId);
			csv.WriteField(fboxLogEntry.LogMessage);
		}

		public static FboxLogEntry ReadEntry(CsvReader csv)
		{
			string rawTimestamp = csv.GetField<string>(0);
			int logCategory = csv.GetField<int>(1);
			int eventTypeId = csv.GetField<int>(2);
			string logMessage = csv.GetField<string>(3);

			FboxLogEntry fboxLogEntry = new FboxLogEntry()
			{
				Timestamp = DateTime.ParseExact(rawTimestamp, FboxLogEntry.cLogFileTimestampFormat, null),
				Category = (LogCategory)logCategory,
				EventTypeId = eventTypeId,
				LogMessage = logMessage
			};
			return fboxLogEntry;
		}

	}
}
