using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;

namespace FboxSharp
{

	[DataContract(Namespace = "", Name = "SessionInfo")]
	public class SessionInfo
	{

		private static readonly string EmptySessionId = "0000000000000000";

		public bool IsSessionIdEmpty()
		{
			return SessionId == EmptySessionId;
		}

		[DataMember(Name = "SID", Order = 0, IsRequired = true)]
		public string SessionId { get; set; } = string.Empty;

		[DataMember(Name = "Challenge", Order = 1, IsRequired = true)]
		public string Challenge { get; set; } = string.Empty;

		[DataMember(Name = "BlockTime", Order = 2)]
		public string BlockTime { get; set; } = string.Empty;

		// we get an exception, if this is active - didn't understand why...
		//[DataMember(Name = "Rights", Order = 3, IsRequired = false)]
		//public string Rights { get; set; }

		public static SessionInfo Deserialize(Stream stream)
		{
			DataContractSerializer serializer = new DataContractSerializer(typeof(SessionInfo));
			SessionInfo? info = serializer.ReadObject(stream) as SessionInfo;
			if (info == null)
			{
				throw new FboxSharpException("SessionInfo.Deserialize returned 'null'");
			}
			return info;
		}

	}
}
