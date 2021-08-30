using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IUpdateWorkspaceEntityStrategy<Tab>))]
	[NonParallelizable] // We're seeing a lot of tab tests fail, so I'm hoping this will help alleviate it. RTF-854
	internal class TabUpdateStrategyFixture : ApiServiceTestFixture<IUpdateWorkspaceEntityStrategy<Tab>>
	{
		[Test]
		public void Update_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Update(DefaultWorkspace.ArtifactID, null));
		}

		[Test]
		public void Update()
		{
			Tab toUpdate = null;

			ArrangeWorkingWorkspace(x => x
				.Create(out ObjectType objectType)
				.Create(new Tab { ObjectType = objectType }).Pick(out toUpdate));

			toUpdate.Name = Randomizer.GetString("AT_{0}");

			Sut.Update(DefaultWorkspace.ArtifactID, toUpdate);

			var result = Facade.Resolve<IGetWorkspaceEntityByIdStrategy<Tab>>().Get(DefaultWorkspace.ArtifactID, toUpdate.ArtifactID);

			result.Should().BeEquivalentTo(toUpdate);
		}

		[Test]
		public void Update_TabWithLinkTypeSetupAsExternalLink()
		{
			Tab toUpdate = null;

			ArrangeWorkingWorkspace(x => x
				.Create(new Tab
				{
					Name = Randomizer.GetString("AT_"),
					LinkType = TabLinkType.Link,
					Link = "http://some.link.us/"
				})
				.Pick(out toUpdate));

			toUpdate.Name = Randomizer.GetString("AT_{0}");
			toUpdate.Link = "http://some.external.link.us";

			Sut.Update(DefaultWorkspace.ArtifactID, toUpdate);

			var result = Facade.Resolve<IGetWorkspaceEntityByIdStrategy<Tab>>().Get(DefaultWorkspace.ArtifactID, toUpdate.ArtifactID);

			result.Should().BeEquivalentTo(toUpdate);
		}
	}
}
