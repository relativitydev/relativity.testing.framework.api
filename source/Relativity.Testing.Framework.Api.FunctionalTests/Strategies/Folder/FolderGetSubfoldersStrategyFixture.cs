using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IFolderGetSubfoldersStrategy))]

	internal class FolderGetSubfoldersStrategyFixture : ApiServiceTestFixture<IFolderGetSubfoldersStrategy>
	{
		private IFolderGetSubfoldersStrategy _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = Facade.Resolve<IFolderGetSubfoldersStrategy>();
		}

		[Test]
		public void Get_Missing_ThrowsException()
		{
			int id = int.MaxValue;

			HttpRequestException exception = Assert.Throws<HttpRequestException>(() =>
				_sut.Get(DefaultWorkspace.ArtifactID, id));

			exception.Message.Should().Contain($"ArtifactID {id} does not exist.");
		}

		[Test]
		public void Get_Existing_ReturnsStatusWithExistsSetToTrue()
		{
			Folder workspaceRootFolderSubfolder = null;
			Arrange(() =>
			{
				var toCreate = new Folder
				{
					Name = Randomizer.GetString("AT_")
				};
				workspaceRootFolderSubfolder = Facade.Resolve<IFolderCreateStrategy>().Create(DefaultWorkspace.ArtifactID, toCreate);
			});

			List<Folder> subfolders = _sut.Get(DefaultWorkspace.ArtifactID, workspaceRootFolderSubfolder.ParentFolder.ArtifactID);

			subfolders.Should().NotBeEmpty();
			subfolders.Should().Contain(folder => folder.ArtifactID == workspaceRootFolderSubfolder.ArtifactID);
			subfolders.FirstOrDefault(folder => folder.ArtifactID == workspaceRootFolderSubfolder.ArtifactID).Should().BeEquivalentTo(workspaceRootFolderSubfolder);
		}
	}
}
