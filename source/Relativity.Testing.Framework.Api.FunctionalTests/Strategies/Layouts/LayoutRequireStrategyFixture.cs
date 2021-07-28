using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IRequireWorkspaceEntityStrategy<Layout>))]
	[VersionRange(">=12.0")]
	internal class LayoutRequireStrategyFixture : ApiServiceTestFixture<IRequireWorkspaceEntityStrategy<Layout>>
	{
		[Test]
		public void Require_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Require(DefaultWorkspace.ArtifactID, null));
		}

		[Test]
		public void Require_Existing()
		{
			Layout toUpdate = null;

			ArrangeWorkingWorkspace(x => x
				.Create(out ObjectType objectType)
				.Create(new Layout { ObjectType = objectType }).Pick(out toUpdate));

			toUpdate.Name = Randomizer.GetString("AT_{0}");

			var result = Sut.Require(DefaultWorkspace.ArtifactID, toUpdate);

			result.Should().BeEquivalentTo(toUpdate);
		}

		[Test]
		public void Require_Missing()
		{
			ObjectType objectType = null;

			ArrangeWorkingWorkspace(x => x
				.Create(new ObjectType()).Pick(out objectType));

			var layout = new Layout
			{
				Name = Randomizer.GetString("AT_Name_"),
				ObjectType = objectType
			};

			var result = Sut.Require(DefaultWorkspace.ArtifactID, layout);

			result.ArtifactID.Should().BePositive();
			result.Should().BeEquivalentTo(layout, o => o.
			Excluding(x => x.ArtifactID).
			Excluding(x => x.Owner));
		}
	}
}
