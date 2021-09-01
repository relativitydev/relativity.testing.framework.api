using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IRequireWorkspaceEntityStrategy<Tab>))]
	[NonParallelizable] // We're seeing a lot of tab tests fail, so I'm hoping this will help alleviate it.
	internal class TabRequireStrategyFixture : ApiServiceTestFixture<IRequireWorkspaceEntityStrategy<Tab>>
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
			Tab toUpdate = null;

			ArrangeWorkingWorkspace(x => x
				.Create(out ObjectType objectType)
				.Create(new Tab { ObjectType = objectType }).Pick(out toUpdate));

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

			var tab = new Tab
			{
				Name = Randomizer.GetString("AT_Name_"),
				ObjectType = objectType
			};

			var result = Sut.Require(DefaultWorkspace.ArtifactID, tab);

			result.ArtifactID.Should().BePositive();
			result.Should().BeEquivalentTo(tab, o => o.Excluding(x => x.ArtifactID));
		}
	}
}
