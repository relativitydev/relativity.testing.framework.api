using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(AgentGetAllByTypeNameStrategy))]
	internal class AgentGetAllByTypeNameStrategyFixture : ApiServiceTestFixture<IAgentGetAllByAgentTypeNameStrategy>
	{
		[Test]
		public void GetAllByTypeName_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.GetAllByTypeName(null));
		}

		[Test]
		public void GetAllByTypeName_Missing()
		{
			var result = Sut.GetAllByTypeName(Guid.NewGuid().ToString());
			result.Should().BeEmpty();
		}

		[Test]
		public void GetAllByTypeName_Existing()
		{
			string existingAgentTypeName = null;

			Arrange(() =>
			{
				existingAgentTypeName = Facade.Resolve<IGetAllStrategy<Agent>>()
					.GetAll()[0].AgentType.Name;
			});

			var result = Sut.GetAllByTypeName(existingAgentTypeName);

			result.Should().NotBeEmpty();
			foreach (Agent agent in result)
			{
				agent.ArtifactID.Should().BePositive();
				agent.Name.Should().NotBeNullOrWhiteSpace();
				agent.AgentType.Name.Should().NotBeNullOrWhiteSpace();
				agent.AgentServer.ArtifactID.Should().BePositive();
			}
		}
	}
}
