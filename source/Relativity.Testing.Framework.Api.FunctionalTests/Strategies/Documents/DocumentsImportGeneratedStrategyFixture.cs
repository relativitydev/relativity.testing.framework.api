using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IDocumentsImportGeneratedStrategy))]
	internal class DocumentsImportGeneratedStrategyFixture : ApiServiceTestFixture<IDocumentsImportGeneratedStrategy>
	{
		private Workspace _workspace;

		public DocumentsImportGeneratedStrategyFixture()
		{
		}

		public DocumentsImportGeneratedStrategyFixture(string relativityInstanceAlias)
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
			Sut.Import(_workspace.ArtifactID);

			var result = Facade.Resolve<IGetAllWorkspaceEntitiesStrategy<Document>>()
				.GetAll(_workspace.ArtifactID);

			result.Length.Should().Be(10);
		}
	}
}
