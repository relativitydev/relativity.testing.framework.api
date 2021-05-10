using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ICreateWorkspaceEntityStrategy<Entity>))]
	internal class EntityCreateStrategyFixture : ApiServiceTestFixture<ICreateWorkspaceEntityStrategy<Entity>>
	{
		public EntityCreateStrategyFixture()
		{
		}

		public EntityCreateStrategyFixture(string relativityInstanceAlias)
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
		public void Create_WithEmptyEntity()
		{
			var result = Sut.Create(DefaultWorkspace.ArtifactID, new Entity());
			result.ArtifactID.Should().BePositive();
		}

		[Test]
		public void Create_WithFilledEntity()
		{
			Entity entity = new Entity
			{
				FullName = Randomizer.GetString(),
				Type = new NamedArtifact { Name = "Other" },
				DocumentNumberingPrefix = Randomizer.GetString(),
				Classification = new List<NamedArtifact> { new NamedArtifact { Name = "Custodian – Processing" } }
			};

			var result = Sut.Create(DefaultWorkspace.ArtifactID, entity);
			result.ArtifactID.Should().BePositive();
			result.FullName.Should().NotBeNullOrEmpty();
			result.DocumentNumberingPrefix.Should().NotBeNullOrEmpty();
		}
	}
}
