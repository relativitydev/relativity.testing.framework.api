using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(AgentTypeGetAllStrategy))]
	internal class AgentTypeGetAllStrategyFixture : ApiServiceTestFixture<IGetAllStrategy<AgentType>>
	{
		public AgentTypeGetAllStrategyFixture()
		{
		}

		public AgentTypeGetAllStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[Test]
		public void GetAll()
		{
			var result = Sut.GetAll();
			result.Should().NotBeEmpty();
		}
	}
}
