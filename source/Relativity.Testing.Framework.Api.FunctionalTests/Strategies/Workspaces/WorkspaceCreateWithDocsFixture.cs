using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IWorkspaceService))]
	internal class WorkspaceCreateWithDocsFixture : ApiServiceTestFixture<IWorkspaceService>
	{
		public WorkspaceCreateWithDocsFixture()
		{
		}

		public WorkspaceCreateWithDocsFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[Test]
		public void CreateWithDocs_NegativeValue()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() =>
				Sut.CreateWithDocs(new Workspace(), -10));
		}

		[Test]
		public void CreateWithDocs()
		{
			var createdWorkspace = Sut.CreateWithDocs(new Workspace());

			var documents = Facade.Resolve<IGetAllWorkspaceEntitiesStrategy<Document>>()
				.GetAll(createdWorkspace.ArtifactID);

			documents.Length.Should().Be(10);
		}
	}
}
