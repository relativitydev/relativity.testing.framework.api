using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IUpdateProductionsDataSourceStrategy))]
	internal class ProductionDataSourceUpdateStrategyFixture : ApiServiceTestFixture<IUpdateProductionsDataSourceStrategy>
	{
		private Production _production;
		private KeywordSearch _keywordSearch;

		public ProductionDataSourceUpdateStrategyFixture()
		{
		}

		public ProductionDataSourceUpdateStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			_production = Facade.Resolve<ICreateWorkspaceEntityStrategy<Production>>()
				.Create(DefaultWorkspace.ArtifactID, new Production());

			_keywordSearch = Facade.Resolve<ICreateWorkspaceEntityStrategy<KeywordSearch>>()
				.Create(DefaultWorkspace.ArtifactID, new KeywordSearch());
		}

		[Test]
		public void Update_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Update(DefaultWorkspace.ArtifactID, _production.ArtifactID, null));
		}

		[Test]
		public void Update()
		{
			ProductionDataSource existingProductionDataSource = null;

			Arrange(() =>
			{
				existingProductionDataSource = Facade.Resolve<ICreateWorkspaceEntityStrategy<ProductionDataSource>>()
					.Create(DefaultWorkspace.ArtifactID, new ProductionDataSource
					{
						Name = Randomizer.GetString(),
						ProductionId = _production.ArtifactID,
						ProductionType = ProductionType.NativesOnly,
						SavedSearch = new NamedArtifact { ArtifactID = _keywordSearch.ArtifactID }
					});
			});

			var toUpdate = existingProductionDataSource.Copy();
			toUpdate.Name = Randomizer.GetString();

			Sut.Update(DefaultWorkspace.ArtifactID, _production.ArtifactID, toUpdate);

			var result = Facade.Resolve<IGetWorkspaceEntityByIdStrategy<ProductionDataSource>>().Get(DefaultWorkspace.ArtifactID, existingProductionDataSource.ArtifactID);
			result.Should().BeEquivalentTo(toUpdate, x => x.Excluding(y => y.ArtifactTypeId)
				.Excluding(y => y.Placeholder).Excluding(y => y.SavedSearch.Name).Excluding(y => y.MarkupSet)
				.Excluding(y => y.ProductionId));
		}
	}
}
