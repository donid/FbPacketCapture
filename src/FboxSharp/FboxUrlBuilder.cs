using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace FboxSharp
{

	public class FboxUrlBuilder
	{
		private readonly FboxConnectionSettings _loginData;

		public FboxUrlBuilder(FboxConnectionSettings loginData)
		{
			_loginData = loginData;
		}

		public Uri GetBaseUrl()
		{
			return new Uri($"{_loginData.Scheme}://{_loginData.Host}");
		}


		public Uri GetLoginLuaRelativeUrl()
		{
			return new Uri($"{_loginData.Path}", UriKind.Relative);
		}

		// when you specify a wrong response: a SessionInfo with an empty/invalid SID will be returned
		public Uri GetCreateSessionRelativeUrl(string challenge)
		{
			string response = CalculateLoginResponse(challenge, _loginData.Password);
			Uri loginUrl = GetLoginLuaRelativeUrl();
			return new Uri($"{loginUrl}?username={_loginData.UserName}&response={response}", UriKind.Relative);
		}

		public Uri GetQuerySessionRelativeUrl(string sessionId)
		{
			Uri loginUrl = GetLoginLuaRelativeUrl();
			return new Uri($"{loginUrl}?sid={sessionId}", UriKind.Relative);
		}

		public Uri GetTerminateSessionRelativeUrl(string sessionId)
		{
			Uri loginUrl = GetLoginLuaRelativeUrl();
			return new Uri($"{loginUrl}?logout=1&sid={sessionId}", UriKind.Relative);
		}

		public static Uri GetLogEntriesRelativeUrl(string sessionId)
		{
			return new Uri($"query.lua?mq_log=logger:status/log&sid={sessionId}", UriKind.Relative);
		}

		public static Uri GetCreatePcapStreamRelativeUrl(string sessionId, string interfaceName)
		{
			//string url = $"http://fritz.box/cgi-bin/capture_notimeout?ifaceorminor={interfaceName}&snaplen=1600&capture=Start&sid={sessionId}";
			return new Uri($"cgi-bin/capture_notimeout?ifaceorminor={interfaceName}&snaplen=1600&capture=Start&sid={sessionId}", UriKind.Relative);
		}

		public static string CalculateLoginResponse(string challenge, string password)
		{
			string hash = CalculateMD5Hash($"{challenge}-{password}");
			return $"{challenge}-{hash}";
		}

		public static string CalculateMD5Hash(string text)
		{
			MD5 md5Hasher = MD5.Create();
			// UTF-8 > UTF-16LE
			byte[] data = md5Hasher.ComputeHash(Encoding.Unicode.GetBytes(text));
			return string.Join(string.Empty, data.Select(item => item.ToString("x2")));
		}


	}
}
