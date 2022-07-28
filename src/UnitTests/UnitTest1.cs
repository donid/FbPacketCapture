using System.Net;

using AdysTech.CredentialManager;

namespace UnitTests
{
	[TestClass]
	public class UnitTest1
	{
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
	}

}
