using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetByIdStrategy<Agent>))]
	internal class AgentGetByIdStrategyFixture : ApiServiceTestFixture<IGetByIdStrategy<Agent>>
	{
		public AgentGetByIdStrategyFixture()
		{
		}

		public AgentGetByIdStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[Test]
		public void Get_Missing()
		{
			var result = Sut.Get(int.MaxValue);

			result.Should().BeNull();
		}

		[Test]
		public void Get_Existing()
		{
			Agent existingAgent = null;

			Arrange(() =>
			{
				existingAgent = Facade.Resolve<IGetAllStrategy<Agent>>()
					.GetAll()[0];
			});

			var result = Sut.Get(existingAgent.ArtifactID);

			result.Should().BeEquivalentTo(existingAgent, x => x.Excluding(e => e.AgentType));
			result.AgentType.ArtifactID.Should().BePositive();
			result.AgentType.Name.Should().NotBeNullOrEmpty();
		}
	}
}
