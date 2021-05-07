using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IUpdateWorkspaceEntityStrategy<Layout>))]
	[VersionRange(">=12.0")]
	internal class LayoutUpdateStrategyFixture : ApiServiceTestFixture<IUpdateWorkspaceEntityStrategy<Layout>>
	{
		public LayoutUpdateStrategyFixture()
		{
		}

		public LayoutUpdateStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
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
			Layout toUpdate = null;

			ArrangeWorkingWorkspace(x => x
				.Create(out ObjectType objectType)
				.Create(new Layout { ObjectType = objectType }).Pick(out toUpdate));

			toUpdate.Name = Randomizer.GetString("AT_{0}");

			Sut.Update(DefaultWorkspace.ArtifactID, toUpdate);

			var result = Facade.Resolve<IGetWorkspaceEntityByIdStrategy<Layout>>().Get(DefaultWorkspace.ArtifactID, toUpdate.ArtifactID);

			result.Should().BeEquivalentTo(toUpdate);
		}
	}
}
