using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(AgentDeleteStrategy))]
	internal class AgentDeleteStrategyFixture : ApiServiceTestFixture<IDeleteByIdStrategy<Agent>>
	{
		[Test]
		public void Delete_Missing()
		{
			Assert.Throws<HttpRequestException>(() =>
				Sut.Delete(int.MaxValue));
		}

		[Test]
		public void Delete_Existing()
		{
			Agent toDelete = null;

			Arrange(() =>
			{
				var agentType = Facade.Resolve<IGetByNameStrategy<AgentType>>()
					.Get("Auto Batch Manager");

				var agentServer = Facade.Resolve<IAgentServerGetAvailableStrategy>().GetAvailable(agentType.Name)[0];

				var agent = new Agent
				{
					AgentType = agentType,
					AgentServer = agentServer
				};

				toDelete = Facade.Resolve<ICreateStrategy<Agent>>().Create(agent);
			});

			Sut.Delete(toDelete.ArtifactID);

			Facade.Resolve<IGetByIdStrategy<Agent>>().Get(toDelete.ArtifactID).
				Should().BeNull();
		}
	}
}
