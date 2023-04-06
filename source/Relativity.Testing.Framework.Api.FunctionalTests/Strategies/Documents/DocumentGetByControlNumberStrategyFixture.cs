using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetWorkspaceEntityByNameStrategy<Document>))]
	internal class DocumentGetByControlNumberStrategyFixture : ApiServiceTestFixture<IGetWorkspaceEntityByNameStrategy<Document>>
	{
		[Test]
		public void Get_Missing()
		{
			var result = Sut.Get(DefaultWorkspace.ArtifactID, Guid.NewGuid().ToString());

			result.Should().BeNull();
		}

		[Test]
		public void Get_Existing()
		{
			const string fileName = "single_native.docx";
			var filePath = $@"{AppDomain.CurrentDomain.BaseDirectory}\files\{fileName}";

			Arrange(() =>
			{
				Facade.Resolve<IDocumentSingleNativeImportStrategy>().
					Import(DefaultWorkspace.ArtifactID, filePath);
			});

			var result = Sut.Get(DefaultWorkspace.ArtifactID, fileName);

			result.ArtifactID.Should().BePositive();
			result.ControlNumber.Should().Be(fileName);
			result.HasNative.Should().BeTrue();
		}
	}
}
