using FluentAssertions;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(AgentUpdateStrategy))]
	public class AgentUpdateStrategyFixture
	{
		private Mock<IRestService> _mockRestService;
		private Mock<IGetAllStrategy<Agent>> _getAllStrategy;
		private Mock<IGetByIdStrategy<Agent>> _getAgentByIdStrategy;
		private AgentUpdateStrategy _agentUpdateStrategy;

		[OneTimeSetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();
			_getAllStrategy = new Mock<IGetAllStrategy<Agent>>();
			_getAgentByIdStrategy = new Mock<IGetByIdStrategy<Agent>>();
			_agentUpdateStrategy = new AgentUpdateStrategy(_mockRestService.Object, _getAllStrategy.Object, _getAgentByIdStrategy.Object);
		}

		[Test]
		public void GetCurrentRunIntervalIfNotSet_ReturnsRunInveralFromAgentRequest_IfNotSet()
		{
			_getAgentByIdStrategy.Setup(e => e.Get(It.IsAny<int>())).Returns(new Agent
			{
				RunInterval = 1
			});

			Agent agent = new Agent();

			int runInterval = _agentUpdateStrategy.GetCurrentRunIntervalIfNotSet(agent);

			runInterval.Should().Be(1);
		}

		[Test]
		public void GetCurrentRunIntervalIfNotSet_ReturnsRunInveralFromAgentModel_IfSet()
		{
			_getAgentByIdStrategy.Setup(e => e.Get(It.IsAny<int>())).Returns(new Agent
			{
				RunInterval = 1
			});

			Agent agent = new Agent
			{
				RunInterval = 2
			};

			int runInterval = _agentUpdateStrategy.GetCurrentRunIntervalIfNotSet(agent);

			runInterval.Should().Be(2);
		}
	}
}
