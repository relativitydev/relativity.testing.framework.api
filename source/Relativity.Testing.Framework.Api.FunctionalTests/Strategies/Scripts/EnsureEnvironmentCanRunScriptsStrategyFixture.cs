using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[NonParallelizable]
	[VersionRange(">=12.1")]
	[TestOf(typeof(IEnsureEnvironmentCanRunScriptsStrategy))]
	internal class EnsureEnvironmentCanRunScriptsStrategyFixture : ApiServiceTestFixture<IEnsureEnvironmentCanRunScriptsStrategy>
	{
		public EnsureEnvironmentCanRunScriptsStrategyFixture()
		{
		}

		public EnsureEnvironmentCanRunScriptsStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			IDeleteByIdStrategy<Agent> agentDeleteStrategy = Facade.Resolve<IDeleteByIdStrategy<Agent>>();

			Agent[] agents = Facade.Resolve<IAgentGetAllByAgentTypeNameStrategy>().GetAllByTypeName("Script Run Manager");
			foreach (Agent agent in agents)
			{
				agentDeleteStrategy.Delete(agent.ArtifactID);
			}
		}

		[Test]
		public void EnsureEnvironmentCanRunScriptsStrategy_CreatesAndEnablesAScriptRunAgent()
		{
			Sut.EnsureEnvironmentCanRunScripts();

			Agent[] agents = Facade.Resolve<IAgentGetAllByAgentTypeNameStrategy>().GetAllByTypeName("Script Run Manager");
			agents.Length.Should().BeGreaterThan(0);
			agents.First().Enabled.Should().BeTrue();
		}
	}
}
