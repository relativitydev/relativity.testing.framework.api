using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ICreateWorkspaceEntityStrategy<MarkupSet>))]
	internal class MarkupSetCreateFixture : ApiServiceTestFixture<ICreateWorkspaceEntityStrategy<MarkupSet>>
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
			var entity = new MarkupSet
			{
				Name = Randomizer.GetString(),
				Order = Randomizer.GetInt(int.MaxValue),
				RedactionText = Randomizer.GetString()
			};

			var result = Sut.Create(DefaultWorkspace.ArtifactID, entity);

			result.ArtifactID.Should().BePositive();
			result.Name.Should().NotBeNullOrEmpty();
			result.RedactionText.Should().NotBeNullOrEmpty();
		}
	}
}
