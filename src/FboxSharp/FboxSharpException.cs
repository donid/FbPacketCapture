using System;
using System.Linq;

namespace FboxSharp
{
	[Serializable]
	public class FboxSharpException : Exception
	{
		public FboxSharpException() { }
		public FboxSharpException(string message) : base(message) { }
		public FboxSharpException(string message, Exception inner) : base(message, inner) { }
		protected FboxSharpException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
