﻿using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ICreateWorkspaceEntityStrategy<Layout>))]
	[VersionRange(">=12.1")]
	[Ignore("https://github.com/relativitydev/relativity.testing.framework.api/issues/13")]
	internal class LayoutCreateStrategyFixture : ApiServiceTestFixture<ICreateWorkspaceEntityStrategy<Layout>>
	{
		[Test]
		public void Create_WithFilledEntity()
		{
			ObjectType objectType = null;

			ArrangeWorkingWorkspace(x => x.Create(out objectType));

			var entity = new Layout
			{
				Name = Randomizer.GetString("AT_"),
				ObjectType = objectType
			};

			Layout result = Sut.Create(DefaultWorkspace.ArtifactID, entity);

			result.ArtifactID.Should().BePositive();
			result.Should().BeEquivalentTo(entity, o => o.
				Excluding(x => x.ArtifactID).
				Excluding(x => x.Owner));
		}
	}
}
