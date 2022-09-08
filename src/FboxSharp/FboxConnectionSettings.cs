using System;
using System.Linq;

namespace FboxSharp
{

	public class FboxConnectionSettings
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="userName">Can  be empty, if FritzBox does  not use 'User-based' login</param>
		/// <param name="password"></param>
		public FboxConnectionSettings(string userName, string password)
		{
			UserName = userName;
			Password = password;
		}

		public string Scheme { get; set; } = "http";
		public string Host { get; set; } = "fritz.box";
		public string UserName { get; set; }
		public string Password { get; set; }
		public string Path { get; set; } = "login_sid.lua";
	}

}
