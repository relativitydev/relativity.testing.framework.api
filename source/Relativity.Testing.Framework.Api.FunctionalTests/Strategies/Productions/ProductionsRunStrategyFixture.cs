using System;
using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[Ignore("Needs access to known static folder for import - https://jira.kcura.com/browse/RTF-1210")]
	[TestOf(typeof(IProductionsRunStrategy))]
	internal class ProductionsRunStrategyFixture : ApiServiceTestFixture<IProductionsRunStrategy>
	{
		private Workspace _workspace;
		private Production _production;
		private KeywordSearch _keywordSearch;

		public ProductionsRunStrategyFixture()
		{
		}

		public ProductionsRunStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			Arrange(x => x.Create(out _workspace));

			Facade.Resolve<IDocumentsFromCsvNativeImportStrategy>()
				.Import(_workspace.ArtifactID, $@"{AppDomain.CurrentDomain.BaseDirectory}\files\native_documents.csv");

			_production = Facade.Resolve<ICreateWorkspaceEntityStrategy<Production>>()
				.Create(_workspace.ArtifactID, new Production());

			_keywordSearch = Facade.Resolve<ICreateWorkspaceEntityStrategy<KeywordSearch>>()
				.Create(_workspace.ArtifactID, new KeywordSearch());

			var dataSource = new ProductionDataSource
			{
				Name = Randomizer.GetString(),
				ProductionId = _production.ArtifactID,
				ProductionType = ProductionType.NativesOnly,
				SavedSearch = new NamedArtifact { ArtifactID = _keywordSearch.ArtifactID }
			};

			Facade.Resolve<ICreateWorkspaceEntityStrategy<ProductionDataSource>>()
				.Create(_workspace.ArtifactID, dataSource);

			Facade.Resolve<IProductionsStageStrategy>()
				.Stage(_workspace.ArtifactID, _production.ArtifactID);
		}

		[Test]
		public void RunProduction()
		{
			Sut.Run(_workspace.ArtifactID, _production.ArtifactID, 60);

			Facade.Resolve<IGetProductionStatusStrategy>().GetStatus(_workspace.ArtifactID, _production.ArtifactID).Should().Be(ProductionStatus.Produced);
		}

		[Test]
		public void Run_Missing()
		{
			Assert.Throws<HttpRequestException>(() =>
				Sut.Run(_workspace.ArtifactID, int.MaxValue));
		}
	}
}
