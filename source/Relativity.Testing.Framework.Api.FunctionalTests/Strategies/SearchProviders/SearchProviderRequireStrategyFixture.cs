using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IRequireWorkspaceEntityStrategy<SearchProvider>))]
	internal class SearchProviderRequireStrategyFixture : ApiServiceTestFixture<IRequireWorkspaceEntityStrategy<SearchProvider>>
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
			SearchProvider existingSearchProvider = null;

			ArrangeWorkingWorkspace(x => x
				.Create(out existingSearchProvider));

			var toUpdate = existingSearchProvider.Copy();
			toUpdate.Name = Randomizer.GetString();
			toUpdate.Order = 10;

			var result = Sut.Require(DefaultWorkspace.ArtifactID, toUpdate);

			result.Should().BeEquivalentTo(toUpdate);
		}

		[Test]
		public void Require_Missing()
		{
			var entity = new SearchProvider
			{
				Name = Randomizer.GetString(),
				Order = 200
			};

			var result = Sut.Require(DefaultWorkspace.ArtifactID, entity);

			result.ArtifactID.Should().BePositive();
			result.Should().BeEquivalentTo(entity, o => o.Excluding(x => x.ArtifactID));
		}
	}
}
