using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IDeleteWorkspaceEntityByIdStrategy<ProductionDataSource>))]
	internal class ProductionsDeleteDataSourceStrategyFixture : ApiServiceTestFixture<IDeleteWorkspaceEntityByIdStrategy<ProductionDataSource>>
	{
		private Production _production;
		private KeywordSearch _keywordSearch;

		public ProductionsDeleteDataSourceStrategyFixture()
		{
		}

		public ProductionsDeleteDataSourceStrategyFixture(string relativityInstanceAlias)
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
		public void Delete_Missing()
		{
			Assert.Throws<HttpRequestException>(() =>
				Sut.Delete(DefaultWorkspace.ArtifactID, int.MaxValue));
		}

		[Test]
		public void Delete_Existing()
		{
			ProductionDataSource toDelete = null;

			Arrange(() =>
			{
				toDelete = Facade.Resolve<ICreateWorkspaceEntityStrategy<ProductionDataSource>>()
					.Create(DefaultWorkspace.ArtifactID, new ProductionDataSource
					{
						Name = Randomizer.GetString(),
						ProductionId = _production.ArtifactID,
						ProductionType = ProductionType.NativesOnly,
						SavedSearch = new NamedArtifact { ArtifactID = _keywordSearch.ArtifactID }
					});
			});

			Sut.Delete(DefaultWorkspace.ArtifactID, toDelete.ArtifactID);

			Facade.Resolve<IGetWorkspaceEntityByIdStrategy<ProductionDataSource>>()
				.Get(DefaultWorkspace.ArtifactID, toDelete.ArtifactID)
				.Should().BeNull();
		}
	}
}
