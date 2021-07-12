using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[VersionRange(">=12.1")]
	[TestOf(typeof(IScriptRunTableJobStrategy))]
	[Ignore("SUTs frequently give 500 errors while running this test. https://github.com/relativitydev/relativity.testing.framework.api/issues/50")]
	internal class ScriptRunTableJobFixture : ApiServiceTestFixture<IScriptRunTableJobStrategy>
	{
		[Test]
		public void Script_RunTableJob()
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

			var result = Sut.Run(DefaultWorkspace.ArtifactID, script.ArtifactID);

			foreach (System.Data.DataRow row in result.Rows)
			{
				var artifactId = Convert.ToInt32(row["ArtifactID"]);
				artifactId.Should().BeGreaterThan(0);
			}
		}
	}
}
