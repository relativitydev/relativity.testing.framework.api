using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(AgentServerGetAvailableStrategy))]
	internal class AgentServerGetAvailableStrategyFixture : ApiServiceTestFixture<IAgentServerGetAvailableStrategy>
	{
		[Test]
		public void GetAvailable_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.GetAvailable(null));
		}

		[Test]
		public void GetAvailable_Missing()
		{
			Assert.Throws<ObjectNotFoundException>(() =>
				Sut.GetAvailable(Guid.NewGuid().ToString()));
		}

		[Test]
		public void GetAvailable_Existing()
		{
			AgentType existingAgentType = null;

			Arrange(() =>
			{
				existingAgentType = Facade.Resolve<IGetAllStrategy<AgentType>>()
					.GetAll()[0];
			});

			var result = Sut.GetAvailable(existingAgentType.Name);

			result.Should().NotBeEmpty();

			foreach (AgentServer agentServer in result)
			{
				agentServer.ArtifactID.Should().BePositive();
				agentServer.Name.Should().NotBeNullOrWhiteSpace();
				agentServer.NumberOfAgents.Should().BePositive();
				agentServer.ProcessorCores.Should().BePositive();
			}
		}
	}
}
