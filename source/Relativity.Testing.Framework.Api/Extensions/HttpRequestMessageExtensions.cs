using System.Net.Http;
using Relativity.Testing.Framework.Api.Helpers;
using Relativity.Testing.Framework.Api.Models;

namespace Relativity.Testing.Framework.Api.Extensions
{
	internal static class HttpRequestMessageExtensions
	{
		public static void OverrideUserCredentialsIfNeeded(this HttpRequestMessage request, UserCredentials userCredentials)
		{
			if (userCredentials != null)
			{
				var newBasicAuthorizationParameter = BasicAuthorizationHelper.GenerateBasicAuthorizationParameter(userCredentials.Username, userCredentials.Password);
				request.Headers.Add("Authorization", newBasicAuthorizationParameter);
			}
		}
	}
}
