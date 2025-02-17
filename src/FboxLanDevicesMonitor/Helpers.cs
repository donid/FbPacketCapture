using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DnblCore;

using FboxSharp;

using Microsoft.Extensions.Logging;

using PS.FritzBox.API;
using PS.FritzBox.API.LANDevice;
using PS.FritzBox.API.SOAP;

namespace FboxLanDevicesMonitor
{
	internal static class Helpers
	{
		public static ILogger? Logger { get; set; }


		public static int CompareIPAddress(IPAddress address1, IPAddress address2)
		{
			IStructuralComparable c = ((IStructuralComparable)address1.GetAddressBytes());
			int result = c.CompareTo(address2.GetAddressBytes(), Comparer<byte>.Default);
			return result;
		}

		public static ConnectionSettings ConvertToPsapiConnectionSettings(FboxConnectionSettings settings)
		{
			var result = new ConnectionSettings();
			result.BaseUrl = $"http://{settings.Host}:49000";
			result.UserName = settings.UserName;
			result.Password = settings.Password;
			return result;
		}

		public static async Task<List<HostEntry>> GetAllHosts(ConnectionSettings settings)
		{
			HostsClient hostsClient = new HostsClient(settings);
			ushort numHosts = await hostsClient.GetHostNumberOfEntriesAsync();
			List<HostEntry> list = new List<HostEntry>();
			for (ushort index = 0; index < numHosts; index++)
			{
				try
				{
					HostEntry he = await hostsClient.GetGenericHostEntryAsync(index);
					list.Add(he);
				}
				catch (SoapFaultException ex)
				{
					// this exception sometimes occurs for the last index (only one time I have seen it for the last TWO items)
					// haven't found a pattern yet to predict when this happens, most of the times the call just works
					string message = nameof(GetAllHosts) + ": GetGenericHostEntryAsync failed for index {@index} (numHosts {@numHosts})";
					Logger?.LogWarning(ex, message, index, numHosts);
				}
			}

			return list;
		}


		public static string GetTooltipText(DiffResults<HostEntryVM, string[]> diffResult)
		{
			StringBuilder toolTip = new();
			if (diffResult.Added.Any())
			{
				toolTip.AppendLine("<b>Added:</b>");
				foreach (HostEntryVM addedEntry in diffResult.Added)
				{
					toolTip.AppendLine($"{addedEntry.HostName} - {addedEntry.IPAddress} - {addedEntry.MACAddress}");
				}
			}
			if (diffResult.Removed.Any())
			{
				toolTip.AppendLine("<b>Removed:</b>");
				foreach (HostEntryVM removedEntry in diffResult.Removed)
				{
					toolTip.AppendLine($"{removedEntry.HostName} - {removedEntry.IPAddress} - {removedEntry.MACAddress}");
				}
			}
			if (diffResult.Changed.Any())
			{
				toolTip.AppendLine("<b>Changed:</b>");
				foreach (DiffResultChangedItem<HostEntryVM, string[]> changedEntry in diffResult.Changed)
				{
					toolTip.AppendLine($"{changedEntry.Item1.HostName}: {GetChangedPropertiesText(changedEntry)}");
				}
			}

			return toolTip.ToString();
		}

		private static string GetChangedPropertiesText(DiffResultChangedItem<HostEntryVM, string[]> changedEntry)
		{
			StringBuilder stringBuilder = new StringBuilder();
			//string.Join(", ", changedEntry.DiffInfo);
			foreach (string propertyName in changedEntry.DiffInfo)
			{
				string oldValue = GetPropertyValue(changedEntry.Item1, propertyName).ToString();
				string newValue = GetPropertyValue(changedEntry.Item2, propertyName).ToString();
				stringBuilder.Append($"{propertyName} '{oldValue}' -> '{newValue}' | ");
			}
			return stringBuilder.ToString();
		}

		public static object GetPropertyValue(object source, string propertyName)
		{
			PropertyInfo property = source.GetType().GetProperty(propertyName);
			return property.GetValue(source, null);
		}



	}
}
