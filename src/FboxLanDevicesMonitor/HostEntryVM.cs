using PS.FritzBox.API.LANDevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace FboxLanDevicesMonitor
{
	public class HostEntryVM
	{
		private HostEntry _hostEntry;
		public HostEntryVM(HostEntry hostEntry)
		{
			_hostEntry = hostEntry;
		}

		public bool Active { get { return _hostEntry.Active; } }

		public string HostName { get { return _hostEntry.HostName; } }

		public string MACAddress { get { return _hostEntry.MACAddress; } }

		public IPAddress IPAddress { get { return _hostEntry.IPAddress; } }

		public string AddressSource { get { return _hostEntry.AddressSource; } }

		public uint LeaseTimeRemaining { get { return _hostEntry.LeaseTimeRemaining; } }

		public string InterfaceType { get { return _hostEntry.InterfaceType; } }

		public static string[]? ListDiffComparer(HostEntryVM arg1, HostEntryVM arg2)
		{
			List<string> propNames = new List<string>();
			if (arg1.Active != arg2.Active)
			{
				propNames.Add("Active");
			}
			if (!propNames.Any())
			{
				return null;
			}
			return propNames.ToArray();
		}

	}
}
