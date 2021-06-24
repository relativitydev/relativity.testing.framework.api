using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IDocumentSingleNativeImportStrategy))]
	internal class DocumentSingleNativeImportStrategyFixture : ApiServiceTestFixture<IDocumentSingleNativeImportStrategy>
	{
		private Workspace _workspace;

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			Arrange(x => x.Create(out _workspace));
		}

		[Test]
		public void Import()
		{
			const string fileName = "single_native.docx";
			Sut.Import(_workspace.ArtifactID, $@"{AppDomain.CurrentDomain.BaseDirectory}\files\{fileName}");

			var result = Facade.Resolve<IGetAllWorkspaceEntitiesStrategy<Document>>()
				.GetAll(_workspace.ArtifactID);

			result.Length.Should().Be(1);
			var document = result.First();
			document.ControlNumber.Should().Be(fileName);
			document.HasNative.Should().BeTrue();
		}
	}
}
