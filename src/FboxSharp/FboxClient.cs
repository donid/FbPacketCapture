using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	internal static class IsExternalInit { }
}


// some code taken from AVM document 'Session-ID_deutsch_13Nov18.pdf' and heavily refactored

namespace FboxSharp
{
	public class FboxClient : IDisposable
	{
		private readonly WebClient _webClient;

		private readonly HttpClient _httpClient = new HttpClient();
		private readonly FboxUrlBuilder _urlBuilder;

		// session timeout: 20 minutes - gets prolonged when connection is actively used (requests)
		private string _sessionId = string.Empty;


		public FboxClient(FboxConnectionSettings settings)
		{
			_urlBuilder = new FboxUrlBuilder(settings);
			_httpClient.BaseAddress = _urlBuilder.GetBaseUrl();
			_httpClient.DefaultRequestHeaders.Accept.Clear();
			_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));


#pragma warning disable SYSLIB0014 // Type or member is obsolete
			// HttpClient has only async methods !!!
			_webClient = new WebClient();
#pragma warning restore SYSLIB0014 // Type or member is obsolete
			_webClient.BaseAddress = _urlBuilder.GetBaseUrl().ToString();

		}


#region Synchronous methods

		public string DownloadString(Uri url)
		{
#pragma warning disable SYSLIB0014 // Type or member is obsolete
			string result = _webClient.DownloadString(url);
#pragma warning restore SYSLIB0014 // Type or member is obsolete

			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <remarks>Throws FboxSharpException on error</remarks>
		/// <returns>The sessionId of the current or newly created session</returns>
		public string GetSessionId()
		{
			if (_sessionId == string.Empty)
			{
				_sessionId = CreateNewSession();
			}
			else
			{
				FboxSessionInfo info = GetSessionInfoForSid(_sessionId);
				// when the sessionId is no longer valid (e.g. timeout) -> an empty Id will be returned
				if (info.IsSessionIdEmpty())
				{
					_sessionId = CreateNewSession();
				}
			}
			return _sessionId;
		}

		// throws FboxSharpException on error
		private  FboxSessionInfo GetSessionInfo(Uri requestUri)
		{
			string response;
			try
			{
				response = DownloadString(requestUri);
			}
			catch (WebException ex)
			{
				throw new FboxSharpException("GetSessionInfo cannot retrieve '{requestUri}'", ex);
			}

			FboxSessionInfo info = new FboxSessionInfo(response);
			return info;
		}
		public FboxSessionInfo GetSessionInfoForSid(string sessionId)
		{
			Uri requestUri = _urlBuilder.GetQuerySessionRelativeUrl(sessionId);
			FboxSessionInfo result = GetSessionInfo(requestUri);
			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <remarks>Throws FboxSharpException on error</remarks>
		/// <returns>The sessionId of the new session</returns>
		public string CreateNewSession()
		{
			Uri loginUrl = _urlBuilder.GetLoginLuaRelativeUrl();
			FboxSessionInfo info1 = GetSessionInfo(loginUrl);
			if (info1.BlockTime > 0)
			{
				throw new FboxSharpException($"FritzBox log-ins are blocked for {info1.BlockTime} seconds.");
			}
			if (!info1.IsSessionIdEmpty())
			{
				// we should never get here, if the FritzBox uses authentication
				// because we didn't use/send credentials
				return info1.SessionId;
			}

			Uri createSessionUrl = _urlBuilder.GetCreateSessionRelativeUrl(info1.Challenge);
			FboxSessionInfo info2 = GetSessionInfo(createSessionUrl);
			if (info2.IsSessionIdEmpty())
			{
				// we get here when username does not exist in FritzBox ( info2.IsSessionIdEmpty()==true )
				throw new FboxSharpException("CreateNewSession returned an empty sessionId - probably wrong User / Password.");
			}

			Debug.WriteLine("Created new FritzBox-Session: sid=" + info2.SessionId);

			return info2.SessionId;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sessionId"></param>
		/// <returns>Returns a SessionInfo with an empty/invalid SID</returns>
		public FboxSessionInfo TerminateSession(string sessionId)
		{
			// http://fritz.box/login_sid.lua?logout=1&sid=<sid> 
			Uri url = _urlBuilder.GetTerminateSessionRelativeUrl(sessionId);
			string page = DownloadString(url);
			FboxSessionInfo result = new FboxSessionInfo(page);
			return result;
		}
#endregion

		static void CheckResponse(string response)
		{
			// FritzBox responds with html login-page, when login fails
			if (response.StartsWith("<!DOCTYPE html>"))
			{
				throw new FboxSharpException("Unexpectedly received HTMl response - probably wrong User / Password! (or SessionId has expired)");
			}
		}

		// TR064-API doesn't return all entries!
		public async Task<string> GetAllLogEntriesJsonAsync(string sessionId)
		{
			Uri logEntriesUrl = FboxUrlBuilder.GetLogEntriesRelativeUrl(sessionId);
			string result = await _httpClient.GetStringAsync(logEntriesUrl);
			CheckResponse(result);
			return result;
		}

		public async Task<IReadOnlyList<FboxLogEntry>> GetAllLogEntriesAsync()
		{
			string sid = await GetSessionIdAsync();
			string log = await GetAllLogEntriesJsonAsync(sid);
			var entries = FboxLogEntry.ParseAllLogEntriesJson(log);
			return entries;
		}

		public async Task<Stream> GetPcapStreamAsync(string sessionId, string interfaceName, CancellationTokenSource cts)
		{
			Uri uri = FboxUrlBuilder.GetCreatePcapStreamRelativeUrl(sessionId, interfaceName);
			Stream input = await _httpClient.GetStreamAsync(uri/*, cts.Token*/); // todo: overload with CancellationTokenSource not available in .Net Standard 2.0
			return input;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <remarks>Throws FboxSharpException on error</remarks>
		/// <returns>The sessionId of the current or newly created session</returns>
		public async Task<string> GetSessionIdAsync()
		{
			if (_sessionId == string.Empty)
			{
				_sessionId = await CreateNewSessionAsync();
			}
			else
			{
				FboxSessionInfo info = await GetSessionInfoForSidAsync(_sessionId);
				// when the sessionId is no longer valid (e.g. timeout) -> an empty Id will be returned
				if (info.IsSessionIdEmpty())
				{
					_sessionId = await CreateNewSessionAsync();
				}
			}
			return _sessionId;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <remarks>Throws FboxSharpException on error</remarks>
		/// <returns>The sessionId of the new session</returns>
		public async Task<string> CreateNewSessionAsync()
		{
			Uri loginUrl = _urlBuilder.GetLoginLuaRelativeUrl();
			FboxSessionInfo info1 = await GetSessionInfoAsync(loginUrl);
			if (info1.BlockTime > 0)
			{
				throw new FboxSharpException($"FritzBox log-ins are blocked for {info1.BlockTime} seconds.");
			}
			if (!info1.IsSessionIdEmpty())
			{
				// we should never get here, if the FritzBox uses authentication
				// because we didn't use/send credentials
				return info1.SessionId;
			}

			Uri createSessionUrl = _urlBuilder.GetCreateSessionRelativeUrl(info1.Challenge);
			FboxSessionInfo info2 = await GetSessionInfoAsync(createSessionUrl);
			if (info2.IsSessionIdEmpty())
			{
				// we get here when username does not exist in FritzBox ( info2.IsSessionIdEmpty()==true )
				throw new FboxSharpException("CreateNewSession returned an empty sessionId - probably wrong User / Password.");
			}

			Debug.WriteLine("Created new FritzBox-Session: sid=" + info2.SessionId);

			return info2.SessionId;
		}

		public async Task<FboxSessionInfo> GetSessionInfoForSidAsync(string sessionId)
		{
			Uri requestUri = _urlBuilder.GetQuerySessionRelativeUrl(sessionId);
			FboxSessionInfo result = await GetSessionInfoAsync(requestUri);
			return result;
		}

		// throws FboxSharpException on error
		private async Task<FboxSessionInfo> GetSessionInfoAsync(Uri requestUri)
		{
			string response;
			try
			{
				response = await _httpClient.GetStringAsync(requestUri);
			}
			catch (HttpRequestException ex)
			{
				throw new FboxSharpException("GetSessionInfoAsync cannot retrieve '{requestUri}'", ex);
			}

			FboxSessionInfo info = new FboxSessionInfo(response);
			return info;
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
					if (_webClient != null)
					{
						_webClient.Dispose();
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
