using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IUpdateStrategy<Agent>))]
	internal class AgentUpdateStrategyFixture : ApiServiceTestFixture<IUpdateStrategy<Agent>>
	{
		private AgentType _agentType;
		private AgentServer _agentServer;
		private ICreateStrategy<Agent> _createStrategy;
		private IGetByIdStrategy<Agent> _getByIdStrategy;

		protected override void OnSetUpTest()
		{
			base.OnSetUpTest();
			_agentType = Facade.Resolve<IGetByNameStrategy<AgentType>>()
				.Get("Transform Set Manager");

			_agentServer = Facade.Resolve<IAgentServerGetAvailableStrategy>().GetAvailable(_agentType.Name)[0];

			_createStrategy = Facade.Resolve<ICreateStrategy<Agent>>();
			_getByIdStrategy = Facade.Resolve<IGetByIdStrategy<Agent>>();
		}

		[Test]
		public void Update_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Update(null));
		}

		[Test]
		public void Update()
		{
			var entity = new Agent
			{
				AgentType = _agentType,
				AgentServer = _agentServer
			};

			var toUpdate = _createStrategy.Create(entity.Copy());

			toUpdate.Enabled = false;
			toUpdate.LoggingLevel = AgentLoggingLevelType.All;
			toUpdate.AgentServer = _agentServer;
			toUpdate.AgentType = _agentType;
			toUpdate.RunInterval = 21;

			Sut.Update(toUpdate);

			var result = _getByIdStrategy.Get(toUpdate.ArtifactID);

			result.Should().BeEquivalentTo(toUpdate, o => o.Excluding(x => x.ArtifactID)
				.Excluding(x => x.AgentType.ArtifactID)
				.Excluding(x => x.RunInterval)
				.Excluding(x => x.Name));
		}
	}
}
