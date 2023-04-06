using System.Net.Http;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Extensions;
using Relativity.Testing.Framework.Api.Models;

namespace Relativity.Testing.Framework.Api.Tests.Extensions
{
	[TestOf(typeof(HttpRequestMessageExtensions))]
	public class HttpRequestMessageExtensionsFixture
	{
		[Test]
		public void OverrideUserCredentialsIfNeeded_WithValidUserCredentials_ShouldAddAuthorizationHeader()
		{
			using (var request = new HttpRequestMessage())
			{
				request.OverrideUserCredentialsIfNeeded(new UserCredentials { Username = "Username", Password = "Password" });

				Assert.IsTrue(request.Headers.Contains("Authorization"));
			}
		}

		[Test]
		public void OverrideUserCredentialsIfNeeded_WithNullUserCredentials_ShouldNotAddAuthorizationHeader()
		{
			using (var request = new HttpRequestMessage())
			{
				request.OverrideUserCredentialsIfNeeded(null);

				Assert.IsFalse(request.Headers.Contains("Authorization"));
			}
		}

		[Test]
		public void OverrideUserCredentialsIfNeeded_WithEmptyUserCredentials_ShouldAddAuthorizationHeader()
		{
			using (var request = new HttpRequestMessage())
			{
				request.OverrideUserCredentialsIfNeeded(new UserCredentials { Username = null, Password = string.Empty });

				Assert.IsTrue(request.Headers.Contains("Authorization"));
			}
		}
	}
}
