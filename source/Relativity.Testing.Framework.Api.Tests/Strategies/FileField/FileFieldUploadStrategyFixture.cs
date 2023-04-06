using System;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Models.Fields;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(GroupCreateStrategyV1))]
	internal class FileFieldUploadStrategyFixture
	{
		private Mock<IRestService> _mockRestService;
		private IFileFieldUploadStrategy _sut;

		[SetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();
			_sut = new FileFieldUploadStrategy(_mockRestService.Object);
		}

		[Test]
		public void UploadFile_PassesWorkspaceIDToUpdateRequest()
		{
			int workspaceID = 2;

			FileFieldDTO responseFileField = new FileFieldDTO
			{
				UploadedFileGuid = Guid.NewGuid(),
				FileName = "AFile"
			};

			_mockRestService.Setup(restService => restService.Post<FileFieldDTO>(It.IsAny<string>(), It.IsAny<object>(), 2, null)).Returns(responseFileField);

			FileFieldDTO fileFieldDTO = new FileFieldDTO
			{
				Field = new FileField
				{
					ArtifactID = 1
				},
				FileName = "AFile",
				ObjectRef = new Artifact(1),
				FileStream = System.IO.Stream.Null
			};

			_sut.UploadFile(workspaceID, fileFieldDTO);

			_mockRestService.Verify(restService => restService.Post<NamedArtifact>($"Relativity.Objects/workspace/{workspaceID}/object/update", It.IsAny<object>(), 2, null), Times.Once);
		}
	}
}
