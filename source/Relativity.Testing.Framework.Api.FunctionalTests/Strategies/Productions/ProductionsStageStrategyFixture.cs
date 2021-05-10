using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IProductionsStageStrategy))]
	internal class ProductionsStageStrategyFixture : ApiServiceTestFixture<IProductionsStageStrategy>
	{
		private Production _production;
		private KeywordSearch _keywordSearch;

		public ProductionsStageStrategyFixture()
		{
		}

		public ProductionsStageStrategyFixture(string relativityInstanceAlias)
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

			var dataSource = new ProductionDataSource
			{
				Name = Randomizer.GetString(),
				ProductionId = _production.ArtifactID,
				ProductionType = ProductionType.NativesOnly,
				SavedSearch = new NamedArtifact { ArtifactID = _keywordSearch.ArtifactID }
			};

			Facade.Resolve<ICreateWorkspaceEntityStrategy<ProductionDataSource>>().Create(DefaultWorkspace.ArtifactID, dataSource);
		}

		[Test]
		public void Stage()
		{
			Sut.Stage(DefaultWorkspace.ArtifactID, _production.ArtifactID);

			Facade.Resolve<IGetProductionStatusStrategy>().GetStatus(DefaultWorkspace.ArtifactID, _production.ArtifactID).Should().Be(ProductionStatus.Staged);
		}

		[Test]
		public void Stage_Missing()
		{
			Assert.Throws<HttpRequestException>(() =>
				Sut.Stage(DefaultWorkspace.ArtifactID, int.MaxValue));
		}
	}
}
