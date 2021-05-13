using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IUpdateWorkspaceEntityStrategy<Script>))]
	internal class ScriptUpdateStrategyFixture : ApiServiceTestFixture<IUpdateWorkspaceEntityStrategy<Script>>
	{
		private IGetWorkspaceEntityByIdStrategy<Script> _getWorkspaceEntityByIdStrategy;

		public ScriptUpdateStrategyFixture()
		{
		}

		public ScriptUpdateStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();
			_getWorkspaceEntityByIdStrategy = Facade.Resolve<IGetWorkspaceEntityByIdStrategy<Script>>();
		}

		[Test]
		public void Update_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Update(DefaultWorkspace.ArtifactID, null));
		}

		[Test]
		public void Update()
		{
			const string action = "<action returns=\"table\"><![CDATA[ SELECT TOP(10) * FROM[eddsdbo].[Artifact]]]></action>";

			Script existingScript = null;

			ArrangeWorkingWorkspace(x => x
				.Create(new Script
				{
					Name = "My script Name",
					Description = "About my script",
					Category = "My category",
					ScriptBody = $"<script>{action}</script>"
				}).Pick(out existingScript));

			var toUpdate = existingScript.Copy();
			toUpdate.Name = Randomizer.GetString();
			toUpdate.Description = Randomizer.GetString();
			toUpdate.Category = Randomizer.GetString();

			Sut.Update(DefaultWorkspace.ArtifactID, toUpdate);

			var result = _getWorkspaceEntityByIdStrategy.Get(DefaultWorkspace.ArtifactID, toUpdate.ArtifactID);
			result.ArtifactID.Should().Be(toUpdate.ArtifactID);
			result.Name.Should().BeEquivalentTo(toUpdate.Name);
			result.Description.Should().BeEquivalentTo(toUpdate.Description);
			result.Category.Should().BeEquivalentTo(toUpdate.Category);
			result.ScriptBody.Should().Contain(action);
		}
	}
}
