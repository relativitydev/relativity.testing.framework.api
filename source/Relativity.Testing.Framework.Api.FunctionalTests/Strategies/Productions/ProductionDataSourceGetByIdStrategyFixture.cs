using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetWorkspaceEntityByIdStrategy<ProductionDataSource>))]
	internal class ProductionDataSourceGetByIdStrategyFixture : ApiServiceTestFixture<IGetWorkspaceEntityByIdStrategy<ProductionDataSource>>
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
		public void Get_Missing()
		{
			var result = Sut.Get(DefaultWorkspace.ArtifactID, int.MaxValue);

			result.Should().BeNull();
		}

		[Test]
		public void Get_Existing()
		{
			ProductionDataSource existingProductionDataSource = null;

			var entity = new ProductionDataSource
			{
				Name = Randomizer.GetString(),
				ProductionId = _production.ArtifactID,
				ProductionType = ProductionType.NativesOnly,
				SavedSearch = new NamedArtifact { ArtifactID = _keywordSearch.ArtifactID },
				UseImagePlaceholder = UseImagePlaceholderOption.NeverUseImagePlaceholder
			};

			Arrange(() =>
			{
				existingProductionDataSource = Facade.Resolve<ICreateWorkspaceEntityStrategy<ProductionDataSource>>()
					.Create(DefaultWorkspace.ArtifactID, entity);
			});

			var result = Sut.Get(DefaultWorkspace.ArtifactID, existingProductionDataSource.ArtifactID);

			result.Should().BeEquivalentTo(existingProductionDataSource, x => x.
				Excluding(y => y.ArtifactID).
				Excluding(y => y.ProductionId).
				Excluding(y => y.ArtifactTypeId).
				Excluding(y => y.Placeholder).
				Excluding(y => y.SavedSearch.Name).
				Excluding(y => y.MarkupSet));
		}
	}
}
