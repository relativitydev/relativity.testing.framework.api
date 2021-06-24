using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[VersionRange(">=12.1")]
	[TestOf(typeof(IScriptPreviewStrategy))]
	internal class ScriptPreviewFixture : ApiServiceTestFixture<IScriptPreviewStrategy>
	{
		[Test]
		public void Script_Preview()
		{
			Script script = null;

			ArrangeWorkingWorkspace(x => x
				.Create(new Script
				{
					Name = "My script Name",
					Description = "About my script",
					Category = "My category",
					ScriptBody = $"<script><action returns=\"table\"><![CDATA[ SELECT TOP(10) * FROM[eddsdbo].[Artifact]]]></action></script>"
				}).Pick(out script));

			var result = Sut.PreviewScript(DefaultWorkspace.ArtifactID, script.ArtifactID);

			result.Should().NotBeNullOrEmpty();
		}
	}
}
