using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Arrangement;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IDocumentsFromCsvProductionImportStrategy))]
	internal class DocumentsFromCsvProductionImportStrategyFixture : ApiServiceTestFixture<IDocumentsFromCsvProductionImportStrategy>
	{
		private Workspace _workspace;
		private Production _production;

		public DocumentsFromCsvProductionImportStrategyFixture()
		{
		}

		public DocumentsFromCsvProductionImportStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			Arrange(x => x.Create(out _workspace)
				.ArrangeWorkspace(w => w.Create(out _production)));
		}

		[Test]
		public void Import()
		{
			Sut.Import(_workspace.ArtifactID, _production.ArtifactID, $@"{AppDomain.CurrentDomain.BaseDirectory}\files\images.csv");

			var result = Facade.Resolve<IGetAllWorkspaceEntitiesStrategy<Document>>()
				.GetAll(_workspace.ArtifactID);

			result.Length.Should().Be(3);

			foreach (var docunent in result)
			{
				docunent.HasImages.Should().BeNull();
				docunent.HasNative.Should().BeFalse();
				docunent.ExtractedText.Should().BeNullOrEmpty();
			}
		}
	}
}
