using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IClientGetEligibleStatusesStrategy))]
	public class ClientGetEligibleStatusesStrategyFixture : ApiTestFixture
	{
		private IClientGetEligibleStatusesStrategy _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = Facade.Resolve<IClientGetEligibleStatusesStrategy>();
		}

		[Test]
		public void Get_ReturnsListOfStatuses()
		{
			IEnumerable<NamedArtifact> result = _sut.Get();

			result.Should().NotBeEmpty();
			result.First().Name.Should().NotBeNullOrWhiteSpace();
			result.First().ArtifactID.Should().BePositive();
		}
	}
}
