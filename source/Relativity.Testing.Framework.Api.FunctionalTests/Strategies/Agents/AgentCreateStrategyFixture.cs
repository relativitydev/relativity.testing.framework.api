using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(AgentCreateStrategy))]
	internal class AgentCreateStrategyFixture : ApiServiceTestFixture<ICreateStrategy<Agent>>
	{
		private AgentType _agentType;
		private AgentServer _agentServer;

		protected override void OnSetUpTest()
		{
			base.OnSetUpTest();
			_agentType = Facade.Resolve<IGetByNameStrategy<AgentType>>()
				.Get("Auto Batch Manager");

			_agentServer = Facade.Resolve<IAgentServerGetAvailableStrategy>().GetAvailable(_agentType.Name)[0];
		}

		[Test]
		public void Create_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Create(null));
		}

		[Test]
		public void Create_WithoutType()
		{
			var entity = new Agent
			{
				AgentServer = _agentServer
			};

			var exeption = Assert.Throws<ArgumentException>(
				() => Sut.Create(entity));

			exeption.Message.Should().StartWith($"{nameof(Agent)} model should have {nameof(Agent.AgentType)} set.");
		}

		[Test]
		public void Create_WithoutServer()
		{
			var entity = new Agent
			{
				AgentType = _agentType
			};

			var exeption = Assert.Throws<ArgumentException>(
				() => Sut.Create(entity));

			exeption.Message.Should().StartWith($"{nameof(Agent)} model should have {nameof(Agent.AgentServer)} set.");
		}

		[Test]
		public void Create_WithFilledEntity()
		{
			var entity = new Agent
			{
				AgentType = _agentType,
				AgentServer = _agentServer,
				RunInterval = 25
			};

			var result = Sut.Create(entity.Copy());

			result.ArtifactID.Should().BePositive();
			result.Name.Should().StartWith(_agentType.Name);
			result.Should().BeEquivalentTo(entity, o => o.Excluding(x => x.ArtifactID)
				.Excluding(x => x.AgentType.ArtifactID)
				.Excluding(x => x.Name));
		}
	}
}
