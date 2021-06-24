using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ICreateWorkspaceEntityStrategy<ProductionDataSource>))]
	internal class ProductionsCreateDataSourceStrategyFixture : ApiServiceTestFixture<ICreateWorkspaceEntityStrategy<ProductionDataSource>>
	{
		private Production _production;
		private KeywordSearch _keywordSearch;

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			_production = Facade.Resolve<ICreateWorkspaceEntityStrategy<Production>>()
				.Create(DefaultWorkspace.ArtifactID, new Production());

			_keywordSearch = Facade.Resolve<ICreateWorkspaceEntityStrategy<KeywordSearch>>()
				.Create(DefaultWorkspace.ArtifactID, new KeywordSearch());
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
			var entity = new ProductionDataSource
			{
				Name = Randomizer.GetString(),
				ProductionId = _production.ArtifactID,
				ProductionType = ProductionType.NativesOnly,
				UseImagePlaceholder = UseImagePlaceholderOption.NeverUseImagePlaceholder,
				SavedSearch = new NamedArtifact { ArtifactID = _keywordSearch.ArtifactID }
			};

			var result = Sut.Create(DefaultWorkspace.ArtifactID, entity);

			result.ArtifactID.Should().BePositive();
			result.Should().BeEquivalentTo(entity, o => o.
				Excluding(x => x.ArtifactID).
				Excluding(x => x.ArtifactTypeId).
				Excluding(x => x.SavedSearch.Name).
				Excluding(x => x.Placeholder).
				Excluding(x => x.MarkupSet));
		}
	}
}
