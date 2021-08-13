using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ICreateWorkspaceEntityStrategy<Script>))]
	internal class ScriptCreateStrategyFixture : ApiServiceTestFixture<ICreateWorkspaceEntityStrategy<Script>>
	{
		[Test]
		public void Create_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Create(DefaultWorkspace.ArtifactID, null));
		}

		[Test]
		public void Create_WithFilledEntity()
		{
			const string action = "<action returns=\"table\"><![CDATA[ SELECT TOP(10) * FROM[eddsdbo].[Artifact]]]></action>";

			var entity = new Script
			{
				Name = "My script Name",
				Description = "About my script",
				Category = "My category",
				ScriptBody = $"<script>{action}</script>"
			};

			var result = Sut.Create(DefaultWorkspace.ArtifactID, entity);

			result.Should().NotBeNull();

			result.ArtifactID.Should().BePositive();
			result.Name.Should().BeEquivalentTo(entity.Name);
			result.Description.Should().BeEquivalentTo(entity.Description);
			result.Category.Should().BeEquivalentTo(entity.Category);
			result.ScriptBody.Should().Contain(action);
		}

		[Test]
		public void Create_WithFilledEntity_AdminCase()
		{
			const string action = "<action returns=\"table\"><![CDATA[ SELECT TOP(10) * FROM[eddsdbo].[Artifact]]]></action>";

			var entity = new Script
			{
				Name = "My script Name",
				Description = "About my script",
				Category = "My category",
				ScriptBody = $"<script>{action}</script>"
			};

			var result = Sut.Create(-1, entity);

			result.Should().NotBeNull();

			result.ArtifactID.Should().BePositive();
			result.Name.Should().BeEquivalentTo(entity.Name);
			result.Description.Should().BeEquivalentTo(entity.Description);
			result.Category.Should().BeEquivalentTo(entity.Category);
			result.ScriptBody.Should().Contain(action);
		}
	}
}
