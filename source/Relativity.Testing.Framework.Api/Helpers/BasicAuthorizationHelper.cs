using System;
using System.Text;

namespace Relativity.Testing.Framework.Api.Helpers
{
	internal static class BasicAuthorizationHelper
	{
		public static string GenerateBasicAuthorizationParameter(string username, string password)
		{
			var unencodedUsernameAndPassword = $"{username}:{password}";

			var unencodedBytes = Encoding.ASCII.GetBytes(unencodedUsernameAndPassword);
			var base64UsernameAndPassword = Convert.ToBase64String(unencodedBytes);

			return $"Basic {base64UsernameAndPassword}";
		}
	}
}
