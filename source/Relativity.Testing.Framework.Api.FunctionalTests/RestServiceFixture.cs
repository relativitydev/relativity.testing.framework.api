using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;

namespace Relativity.Testing.Framework.Api.FunctionalTests
{
	[TestOf(typeof(RestService))]
	internal class RestServiceFixture : ApiServiceTestFixture<IRestService>
	{
		[Test]
		public void Post()
		{
			string result = Sut.Post<string>("Relativity.Services.InstanceDetails.IInstanceDetailsModule/InstanceDetailsService/GetRelativityVersionAsync");

			result.Should().NotBeNullOrWhiteSpace();
		}
	}
}
