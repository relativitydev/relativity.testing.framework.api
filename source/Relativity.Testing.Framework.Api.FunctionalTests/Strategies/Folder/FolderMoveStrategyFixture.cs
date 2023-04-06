using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IFolderMoveStrategy))]
	internal class FolderMoveStrategyFixture : ApiServiceTestFixture<IFolderMoveStrategy>
	{
		private IFolderMoveStrategy _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = Facade.Resolve<IFolderMoveStrategy>();
		}

		[Test]
		public void Move_WithInvalidFolderArtifactIDs_ThrowsException()
		{
			int id = int.MaxValue;

			HttpRequestException exception = Assert.Throws<HttpRequestException>(() =>
			   _sut.Move(DefaultWorkspace.ArtifactID, id, id - 1));

			exception.Message.Should().Contain($"Folder Artifact ID {id} is invalid.");
		}

		[Test]
		public void Move_WithValidFolderArtifactIDs_ReturnsFolderMoveResponse()
		{
			Folder folderToMove = null;
			Folder newParentFolder = null;
			Arrange(() =>
			{
				IFolderCreateStrategy createStrategy = Facade.Resolve<IFolderCreateStrategy>();
				folderToMove = new Folder
				{
					Name = Randomizer.GetString("AT_")
				};
				folderToMove = createStrategy.Create(DefaultWorkspace.ArtifactID, folderToMove);

				newParentFolder = new Folder
				{
					Name = Randomizer.GetString("AT_")
				};
				newParentFolder = createStrategy.Create(DefaultWorkspace.ArtifactID, newParentFolder);
			});

			FolderMoveResponse result = _sut.Move(DefaultWorkspace.ArtifactID, folderToMove.ArtifactID, newParentFolder.ArtifactID);

			result.Should().NotBeNull();
			result.TotalOperations.Should().BeGreaterThan(0);
			result.ProcessState.Should().NotBeNullOrWhiteSpace();
		}
	}
}
