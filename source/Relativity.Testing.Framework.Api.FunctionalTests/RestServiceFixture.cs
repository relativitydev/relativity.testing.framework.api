using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;

namespace Relativity.Testing.Framework.Api.FunctionalTests
{
	[TestOf(typeof(RestService))]
	internal class RestServiceFixture : ApiServiceTestFixture<IRestService>
	{
		public RestServiceFixture()
		{
		}

		public RestServiceFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[Test]
		public void Post()
		{
			string result = Sut.Post<string>("Relativity.Services.InstanceDetails.IInstanceDetailsModule/InstanceDetailsService/GetRelativityVersionAsync");

			result.Should().NotBeNullOrWhiteSpace();
		}
	}
}
