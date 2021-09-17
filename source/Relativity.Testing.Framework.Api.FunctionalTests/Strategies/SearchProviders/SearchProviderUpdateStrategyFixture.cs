using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IUpdateWorkspaceEntityStrategy<SearchProvider>))]
	internal class SearchProviderUpdateStrategyFixture : ApiServiceTestFixture<IUpdateWorkspaceEntityStrategy<SearchProvider>>
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
			SearchProvider existingSearchProvider = null;

			ArrangeWorkingWorkspace(x => x
				.Create(out existingSearchProvider));

			var toUpdate = existingSearchProvider.Copy();
			toUpdate.Name = Randomizer.GetString();
			toUpdate.Order = 10;

			var result = Sut.Update(DefaultWorkspace.ArtifactID, toUpdate);

			result.Should().BeEquivalentTo(toUpdate);
		}
	}
}
