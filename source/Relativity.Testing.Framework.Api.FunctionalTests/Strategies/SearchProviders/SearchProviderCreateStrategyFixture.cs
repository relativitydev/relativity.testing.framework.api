using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ICreateWorkspaceEntityStrategy<SearchProvider>))]
	internal class SearchProviderCreateStrategyFixture : ApiServiceTestFixture<ICreateWorkspaceEntityStrategy<SearchProvider>>
	{
		public SearchProviderCreateStrategyFixture()
		{
		}

		public SearchProviderCreateStrategyFixture(string relativityInstanceAlias)
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
			var result = Sut.Create(DefaultWorkspace.ArtifactID, new SearchProvider());

			result.ArtifactID.Should().BePositive();
			result.Name.Should().NotBeNullOrEmpty();
		}

		[Test]
		public void Create_WithFilledEntity()
		{
			var entity = new SearchProvider
			{
				Name = Randomizer.GetString(),
				Order = 200
			};

			var result = Sut.Create(DefaultWorkspace.ArtifactID, entity);

			result.ArtifactID.Should().BePositive();
			result.Should().BeEquivalentTo(entity, o => o.Excluding(x => x.ArtifactID));
		}
	}
}
