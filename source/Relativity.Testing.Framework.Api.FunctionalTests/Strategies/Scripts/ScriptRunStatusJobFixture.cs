using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[VersionRange(">=12.1")]
	[TestOf(typeof(IScriptRunStatusJobStrategy))]
	[Ignore("SUTs frequently give 500 errors while running this test. https://github.com/relativitydev/relativity.testing.framework.api/issues/50")]
	internal class ScriptRunStatusJobFixture : ApiServiceTestFixture<IScriptRunStatusJobStrategy>
	{
		[Test]
		public void Script_RunStatusJob()
		{
			Script script = null;
			Client clientToUpdate = null;

			Arrange(x => x.Create(new Client()).Pick(out clientToUpdate));

			ArrangeWorkingWorkspace(x => x
				.Create(new Script
				{
					Name = "My status script Name",
					Description = "About my script",
					Category = "My category",
					ScriptBody = $"<script><action returns='status'><![CDATA[UPDATE [EDDS].[eddsdbo].[Client] SET Name = 'New Name' where ArtifactID = '{clientToUpdate.ArtifactID}']]></action></script>"
				}).Pick(out script));

			var result = Sut.Run(DefaultWorkspace.ArtifactID, script.ArtifactID);

			var clientResult = Facade.Resolve<IClientService>().Get(clientToUpdate.ArtifactID);

			clientResult.Name.Should().Be("New Name");
			result.Should().Be(1);
		}
	}
}
