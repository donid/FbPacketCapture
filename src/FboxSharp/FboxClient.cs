using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FboxSharp
{

    public class FboxClient : IDisposable
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public FboxClient(string fritzHostName = "fritz.box")
        {
            _httpClient.BaseAddress = new Uri("http://" + fritzHostName);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
        }

        public async Task<Stream> GetPcapStreamAsync(string sessionId, string interfaceName, CancellationTokenSource cts)
        {
            //string url = $"http://fritz.box/cgi-bin/capture_notimeout?ifaceorminor={interfaceName}&snaplen=1600&capture=Start&sid={sessionId}";
            Uri uri = new Uri($"/cgi-bin/capture_notimeout?ifaceorminor={interfaceName}&snaplen=1600&capture=Start&sid={sessionId}", UriKind.Relative);
            Stream input = await _httpClient.GetStreamAsync(uri/*, cts.Token*/); // todo: overload with CancellationTokenSource not available in .Net Standard 2.0
            return input;
        }

        // throws FboxSharpException on error
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
                // we get here when username does not exist in FritzBox ( info2.IsSessionIdEmpty()==true )
                return string.Empty;
            }

            return info2.SessionId;
        }


        private async Task<SessionInfo> GetSessionInfoAsync(string parameters = "")
        {
            HttpResponseMessage response;
            try
            {
                response = await _httpClient.GetAsync("login_sid.lua" + parameters);
            }
            catch (HttpRequestException ex)
            {
                throw new FboxSharpException("GetSessionInfoAsync cannot retrieve login_sid.lua", ex);
            }
            if (!response.IsSuccessStatusCode)
            {
                throw new FboxSharpException("GetSessionInfoAsync errorcode: " + response.StatusCode);
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


        #region IDisposable
        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_httpClient != null)
                    {
                        _httpClient.Dispose();
                    }
                }
            }
            _disposed = true;
        }

        ~FboxClient()
        {
            Dispose(false);
        }
        #endregion
    }
}
