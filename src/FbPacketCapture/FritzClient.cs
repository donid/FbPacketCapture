using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FbPacketCapture
{

	public class FbClient
	{
		private readonly HttpClient _httpClient = new HttpClient();// todo: implement dispose for FbClient

		public FbClient(string fritzHostName = "fritz.box")
		{
			_httpClient.BaseAddress = new Uri("http://" + fritzHostName);
			_httpClient.DefaultRequestHeaders.Accept.Clear();
			_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
		}

		public async Task<Stream> GetPcapStreamAsync(string sessionId, string interfaceName, CancellationTokenSource cts)
		{
			//string url = $"http://fritz.box/cgi-bin/capture_notimeout?ifaceorminor={interfaceName}&snaplen=1600&capture=Start&sid={sessionId}";
			Uri uri = new Uri($"/cgi-bin/capture_notimeout?ifaceorminor={interfaceName}&snaplen=1600&capture=Start&sid={sessionId}", UriKind.Relative);
			Stream input = await _httpClient.GetStreamAsync(uri, cts.Token);
			return input;
		}

		// throws ApplicationException on error
		// GetSessionIdAsync code copied and refactored from 
		// https://github.com/Slion/SharpLibFritzBox/blob/master/Project/SessionInfo.cs
		// linux bash script for fritzbox login
		// https://raw.githubusercontent.com/ntop/ntopng/dev/tools/fritzdump.sh
		public async Task<string> GetSessionIdAsync(string username, string password)
		{
			SessionInfo info1 = await GetSessionInfoAsync();
			if (info1 == null)
			{
				return string.Empty;
			}
			if (!info1.IsSessionIdEmpty())
			{
				return info1.SessionId;
			}

			string loginResponse = CalculateLoginResponse(info1.Challenge, password);
			SessionInfo info2 = await GetSessionInfoAsync($"?username={username}&response={loginResponse}");
			if (info2 == null || info2.IsSessionIdEmpty())
			{
				return string.Empty;
			}

			return info2.SessionId;
		}


		private async Task<SessionInfo> GetSessionInfoAsync(string parameters = "")
		{
			HttpResponseMessage response = await _httpClient.GetAsync("login_sid.lua" + parameters);
			if (!response.IsSuccessStatusCode)
			{
				throw new ApplicationException("GetSessionInfoAsync errocode: " + response.StatusCode);
			}

			Stream stream = await response.Content.ReadAsStreamAsync();
			SessionInfo info = SessionInfo.Deserialize(stream);
			return info;
		}



		private static string CalculateLoginResponse(string challenge, string password)
		{
			string hash = CalculateMD5Hash($"{challenge}-{password}");
			return $"{challenge}-{hash}";
		}

		private static string CalculateMD5Hash(string text)
		{
			MD5 md5Hasher = MD5.Create();
			byte[] data = md5Hasher.ComputeHash(Encoding.Unicode.GetBytes(text));
			return string.Join(string.Empty, data.Select(item => item.ToString("x2")));
		}


	}
}
