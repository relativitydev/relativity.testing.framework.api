using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[Ignore("Needs access to known static folder for import - https://jira.kcura.com/browse/RTF-1210")]
	[TestOf(typeof(IDocumentsFromCsvImageImportStrategy))]
	internal class DocumentsFromCsvImageImportStrategyFixture : ApiServiceTestFixture<IDocumentsFromCsvImageImportStrategy>
	{
		private Workspace _workspace;

		public DocumentsFromCsvImageImportStrategyFixture()
		{
		}

		public DocumentsFromCsvImageImportStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			Arrange(x => x.Create(out _workspace));
		}

		[Test]
		public void Import()
		{
			Sut.Import(_workspace.ArtifactID, $@"{AppDomain.CurrentDomain.BaseDirectory}\files\images.csv");

			var result = Facade.Resolve<IGetAllWorkspaceEntitiesStrategy<Document>>()
				.GetAll(_workspace.ArtifactID);

			result.Length.Should().Be(3);

			foreach (var docunent in result)
			{
				docunent.HasImages.Name.Should().Be("Yes");
			}
		}
	}
}
