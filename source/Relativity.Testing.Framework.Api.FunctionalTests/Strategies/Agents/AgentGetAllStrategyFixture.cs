using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(AgentGetAllStrategy))]
	internal class AgentGetAllStrategyFixture : ApiServiceTestFixture<IGetAllStrategy<Agent>>
	{
		[Test]
		public void GetAll()
		{
			var result = Sut.GetAll();
			result.Should().NotBeEmpty();
			result.Any(x => x.RunInterval != Agent.RunIntervalDefault).Should().BeTrue();
		}
	}
}
