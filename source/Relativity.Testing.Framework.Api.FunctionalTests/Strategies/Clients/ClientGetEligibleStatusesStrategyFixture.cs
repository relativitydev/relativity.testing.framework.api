using System.Collections.Generic;
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
		public void Get_DoesNotThrowException()
		{
			Assert.DoesNotThrow(() => _sut.Get());
		}

		[Test]
		public void Get_ReturnsListOfStatuses()
		{
			IList<ArtifactIdNamePair> result = _sut.Get();

			result.Should().NotBeEmpty();
			result[0].Name.Should().NotBeNullOrWhiteSpace();
			result[0].ArtifactID.Should().BePositive();
		}
	}
}
