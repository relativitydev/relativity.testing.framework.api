using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IFolderGetExpandedNodesStrategy))]

	internal class FolderGetExpandedNodesStrategyFixture : ApiServiceTestFixture<IFolderGetExpandedNodesStrategy>
	{
		private IFolderGetExpandedNodesStrategy _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = Facade.Resolve<IFolderGetExpandedNodesStrategy>();
		}

		[Test]
		public void Get_Existing_ReturnsTree()
		{
			Folder childFolderOfTheWorkspaceRootFolder = null;
			childFolderOfTheWorkspaceRootFolder = ArrangeChildFolderOfWorkspaceRootFolder(childFolderOfTheWorkspaceRootFolder);

			List<int> expandedNodes = new List<int> { childFolderOfTheWorkspaceRootFolder.ParentFolder.ArtifactID };

			List<Folder> tree = _sut.Get(DefaultWorkspace.ArtifactID, expandedNodes);

			TestIfTreeContainsFolder(childFolderOfTheWorkspaceRootFolder, tree);
		}

		[Test]
		public void Get_ExistingWithSelected_SelectsFolder()
		{
			Folder childFolderOfTheWorkspaceRootFolder = null;
			childFolderOfTheWorkspaceRootFolder = ArrangeChildFolderOfWorkspaceRootFolder(childFolderOfTheWorkspaceRootFolder);

			List<int> expandedNodes = new List<int> { childFolderOfTheWorkspaceRootFolder.ParentFolder.ArtifactID, childFolderOfTheWorkspaceRootFolder.ArtifactID };

			List<Folder> tree = _sut.Get(DefaultWorkspace.ArtifactID, expandedNodes, childFolderOfTheWorkspaceRootFolder.ArtifactID);

			TestIfTreeContainsFolder(childFolderOfTheWorkspaceRootFolder, tree);
			tree.First().Children.FirstOrDefault(f => f.ArtifactID == childFolderOfTheWorkspaceRootFolder.ArtifactID)?.Selected.Should().BeTrue();
		}

		private Folder ArrangeChildFolderOfWorkspaceRootFolder(Folder childFolderOfTheWorkspaceRootFolder)
		{
			Arrange(() =>
			{
				var toCreate = new Folder
				{
					Name = Randomizer.GetString("AT_")
				};
				childFolderOfTheWorkspaceRootFolder = Facade.Resolve<IFolderCreateStrategy>().Create(DefaultWorkspace.ArtifactID, toCreate);
			});
			return childFolderOfTheWorkspaceRootFolder;
		}

		private static void TestIfTreeContainsFolder(Folder folder, List<Folder> tree)
		{
			tree.Should().NotBeEmpty();
			tree.FirstOrDefault().Should().NotBeNull();
			tree.First().Children.Should().NotBeNullOrEmpty();
			tree.First().Children.Should().Contain(f => f.ArtifactID == folder.ArtifactID);
		}
	}
}
