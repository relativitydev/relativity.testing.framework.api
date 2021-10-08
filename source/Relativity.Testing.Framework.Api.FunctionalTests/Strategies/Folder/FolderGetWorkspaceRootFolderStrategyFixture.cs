using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IFolderGetWorkspaceRootFolderStrategy))]

	internal class FolderGetWorkspaceRootFolderStrategyFixture : ApiServiceTestFixture<IFolderGetWorkspaceRootFolderStrategy>
	{
		private IFolderGetWorkspaceRootFolderStrategy _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = Facade.Resolve<IFolderGetWorkspaceRootFolderStrategy>();
		}

		[Test]
		public void Get_ReturnsRootWorkspaceFolder()
		{
			Folder rootFolder = _sut.Get(DefaultWorkspace.ArtifactID);

			rootFolder.Should().NotBeNull();
			rootFolder.Name.Should().NotBeNullOrWhiteSpace();
			rootFolder.ArtifactID.Should().BeGreaterThan(0);
			rootFolder.Permissions.Should().NotBeNull();
		}
	}
}
