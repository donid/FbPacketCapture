using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Xml.Linq;

namespace FboxSharp
{
	public class FboxSessionInfo
	{

		public static readonly string EmptySessionId = "0000000000000000"; // empty / invalid


		XDocument _xDocument;


		public FboxSessionInfo(string xml)
		{
			try
			{
				_xDocument = XDocument.Parse(xml);
			}
			catch (Exception ex)
			{
				throw new FboxSharpException("GetSessionInfoAsync cannot retrieve '{requestUri}'", ex);
			}
		}

		public bool IsSessionIdEmpty()
		{
			return SessionId == EmptySessionId;
		}

		public string SessionId { get { return GetValue(_xDocument, "SID"); } }

		public string Challenge { get { return GetValue(_xDocument, "Challenge"); } }

		/// <summary>
		/// Time in seconds for which log-ins are denied (after a failed log-in)
		/// Brute-Force-Protection mechanism
		/// </summary>
		public int BlockTime
		{
			get
			{
				string bt = GetValue(_xDocument, "BlockTime");
				return Int32.Parse(bt);
			}
		}

		/// <summary>
		/// Permissions of the current user.
		/// 1: read-only
		/// 2: read and write
		/// </summary>
		public Tuple<string, int>[] Rights
		{
			get
			{
				XElement? xElement = GetElement(_xDocument, "Rights");
				if (xElement == null)
				{
					return new Tuple<string, int>[0];
				}

				IEnumerable<string> names = xElement.Elements("Name").Select(e => e.Value);
				IEnumerable<string> accessVals = xElement.Elements("Access").Select(e => e.Value);
				var res = names.Zip(accessVals, (n, a) => Tuple.Create(n, Int32.Parse(a)));
				return res.ToArray();
			}
		}

		public string[] Users
		{
			get
			{
				XElement? xElement = GetElement(_xDocument, "Users");
				if (xElement == null)
				{
					return new string[0];
				}
				return xElement.Elements("User").Select(e => e.Value).ToArray();
			}
		}

		static XElement? GetElement(XDocument doc, string name)
		{
			XElement? info = doc.FirstNode as XElement;
			if (info == null)
			{
				return null;
			}
			return info.Element(name);
		}

		static string GetValue(XDocument doc, string name)
		{
			XElement? xElement = GetElement(doc, name);
			if (xElement == null)
			{
				return string.Empty;
			}
			return xElement.Value;
		}

	}
}
