using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[NonParallelizable] // These tests cause deadlocks in the database when run in parallel.
	[TestOf(typeof(ICreateWorkspaceEntityStrategy<MarkupSet>))]
	internal class MarkupSetCreateFixture : ApiServiceTestFixture<ICreateWorkspaceEntityStrategy<MarkupSet>>
	{
		public MarkupSetCreateFixture()
		{
		}

		public MarkupSetCreateFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

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
				Order = 1,
				RedactionText = Randomizer.GetString()
			};

			var result = Sut.Create(DefaultWorkspace.ArtifactID, entity);

			result.ArtifactID.Should().BePositive();
			result.Name.Should().NotBeNullOrEmpty();
			result.RedactionText.Should().NotBeNullOrEmpty();
		}
	}
}
