using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetWorkspaceEntityByIdStrategy<Script>))]
	internal class ScriptGetByIdStrategyFixture : ApiServiceTestFixture<IGetWorkspaceEntityByIdStrategy<Script>>
	{
		public ScriptGetByIdStrategyFixture()
		{
		}

		public ScriptGetByIdStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[Test]
		public void Get_Missing()
		{
			var result = Sut.Get(DefaultWorkspace.ArtifactID, int.MaxValue);

			result.Should().BeNull();
		}

		[Test]
		public void Get_Existing()
		{
			Script existingScript = null;
			const string action = "<action returns=\"table\"><![CDATA[ SELECT TOP(10) * FROM[eddsdbo].[Artifact]]]></action>";

			ArrangeWorkingWorkspace(x => x
				.Create(new Script
				{
					Name = "My script Name",
					Description = "About my script",
					Category = "My category",
					ScriptBody = $"<script>{action}</script>"
				}).Pick(out existingScript));

			var result = Sut.Get(DefaultWorkspace.ArtifactID, existingScript.ArtifactID);

			result.Should().NotBeNull();

			result.ArtifactID.Should().BePositive();
			result.Name.Should().BeEquivalentTo(existingScript.Name);
			result.Description.Should().BeEquivalentTo(existingScript.Description);
			result.Category.Should().BeEquivalentTo(existingScript.Category);
			result.ScriptBody.Should().Contain(action);
		}
	}
}
