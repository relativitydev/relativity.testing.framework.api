using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Arrangement;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IDocumentSingleProducedImageImportStrategy))]
	internal class DocumentSingleProducedImageImportStrategyFixture : ApiServiceTestFixture<IDocumentSingleProducedImageImportStrategy>
	{
		private Workspace _workspace;
		private Production _production;

		public DocumentSingleProducedImageImportStrategyFixture()
		{
		}

		public DocumentSingleProducedImageImportStrategyFixture(string relativityInstanceAlias)
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
			const string fileName = "single_image.jpg";

			Sut.Import(_workspace.ArtifactID, _production.ArtifactID, $@"{AppDomain.CurrentDomain.BaseDirectory}\files\{fileName}");

			var result = Facade.Resolve<IGetAllWorkspaceEntitiesStrategy<Document>>()
				.GetAll(_workspace.ArtifactID);

			result.Length.Should().Be(1);
			var document = result.First();
			document.ControlNumber.Should().Be(fileName);
		}
	}
}
