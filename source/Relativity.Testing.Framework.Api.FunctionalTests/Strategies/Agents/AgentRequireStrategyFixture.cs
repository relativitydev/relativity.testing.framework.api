using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IRequireStrategy<Agent>))]
	internal class AgentRequireStrategyFixture : ApiServiceTestFixture<IRequireStrategy<Agent>>
	{
		private AgentType _agentType;
		private AgentServer _agentServer;
		private ICreateStrategy<Agent> _createStrategy;
		private IGetByIdStrategy<Agent> _getByIdStrategy;

		public AgentRequireStrategyFixture()
		{
		}

		public AgentRequireStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

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
		public void Require_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Require(null));
		}

		[Test]
		public void Require_Existing()
		{
			Agent existingAgent = null;

			Arrange(() =>
			{
				var entity = new Agent
				{
					AgentType = _agentType,
					AgentServer = _agentServer
				};

				existingAgent = _createStrategy.Create(entity);
			});

			var toUpdate = existingAgent.Copy();
			toUpdate.AgentType = _agentType;
			toUpdate.AgentServer = _agentServer;
			toUpdate.LoggingLevel = AgentLoggingLevelType.All;

			var result = Sut.Require(toUpdate);

			result.Should().BeEquivalentTo(toUpdate, o => o.Excluding(x => x.ArtifactID)
				.Excluding(x => x.AgentType.ArtifactID)
				.Excluding(x => x.Name));
		}

		[Test]
		public void Require_Missing()
		{
			var entity = new Agent
			{
				AgentType = _agentType,
				AgentServer = _agentServer
			};

			var result = Sut.Require(entity);

			result.ArtifactID.Should().BePositive();
			result.Name.Should().StartWith(_agentType.Name);
			result.Should().BeEquivalentTo(entity, o => o.Excluding(x => x.ArtifactID)
				.Excluding(x => x.AgentType.ArtifactID)
				.Excluding(x => x.Name)
				.Excluding(x => x.RunInterval));
		}
	}
}
