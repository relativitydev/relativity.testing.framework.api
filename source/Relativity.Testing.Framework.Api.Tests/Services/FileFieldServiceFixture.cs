using System;
using System.IO;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Models.Fields;

namespace Relativity.Testing.Framework.Api.Tests.Services
{
	[TestFixture]
	[TestOf(typeof(IFileFieldService))]
	public class FileFieldServiceFixture
	{
		private const string _TEST_FILE_NAME = "Test File Name";
		private Mock<IFileFieldDownloadStrategy> _mockDownloadStrategy;
		private Mock<IFileFieldUploadStrategy> _mockUploadStrategy;
		private IFileFieldService _sut;

		[OneTimeSetUp]
		public void SetUp()
		{
			_mockDownloadStrategy = new Mock<IFileFieldDownloadStrategy>();
			_mockUploadStrategy = new Mock<IFileFieldUploadStrategy>();
			_sut = new FileFieldService(_mockUploadStrategy.Object, _mockDownloadStrategy.Object);
		}

		[Test]
		public void UploadFile_ThrowsWhenObjectRefIsNull()
		{
			var fileFieldDTO = new FileFieldDTO
			{
				Field = new FileField
				{
					ArtifactID = 1
				},
				FileName = _TEST_FILE_NAME
			};

			CheckThatUploadFileThrowsArgumentExceptionWithMessage(-1, fileFieldDTO, "File Field DTO must contain ObjectRef with Artifact ID");
		}

		[Test]
		public void UploadFile_ThrowsWhenObjectRefArtifactIDIsZero()
		{
			var fileFieldDTO = new FileFieldDTO
			{
				Field = new FileField
				{
					ArtifactID = 1
				},
				ObjectRef = new Artifact(0),
				FileName = _TEST_FILE_NAME
			};

			CheckThatUploadFileThrowsArgumentExceptionWithMessage(-1, fileFieldDTO, "File Field DTO must contain ObjectRef with Artifact ID");
		}

		[Test]
		public void UploadFile_ThrowsWhenFileNameIsEmpty()
		{
			var fileFieldDTO = new FileFieldDTO
			{
				Field = new FileField
				{
					ArtifactID = 1
				},
				ObjectRef = new Artifact(1),
				FileName = string.Empty
			};

			CheckThatUploadFileThrowsArgumentExceptionWithMessage(-1, fileFieldDTO, "File Field DTO must contain not null and not empty FileName.");
		}

		[Test]
		public void UploadFile_ThrowsWhenFileNameIsNull()
		{
			var fileFieldDTO = new FileFieldDTO
			{
				Field = new FileField
				{
					ArtifactID = 1
				},
				ObjectRef = new Artifact(1),
			};

			CheckThatUploadFileThrowsArgumentExceptionWithMessage(-1, fileFieldDTO, "File Field DTO must contain not null and not empty FileName.");
		}

		[Test]
		public void UploadFile_ThrowsWhenFileFieldDoesNotHaveArtifactIdAndName()
		{
			var fileFieldDTO = new FileFieldDTO
			{
				Field = new FileField
				{
					ArtifactID = 0
				},
				ObjectRef = new Artifact(1),
				FileName = _TEST_FILE_NAME
			};

			CheckThatUploadFileThrowsArgumentExceptionWithMessage(-1, fileFieldDTO, "File Field DTO must contain File with Artifact ID or Name.");
		}

		[Test]
		public void UploadFile_ThrowsWhenWorkspaceIdIsZero()
		{
			var fileFieldDTO = new FileFieldDTO
			{
				Field = new FileField
				{
					ArtifactID = 1
				},
				ObjectRef = new Artifact(1),
				FileName = _TEST_FILE_NAME
			};
			CheckThatUploadFileThrowsArgumentExceptionWithMessage(0, fileFieldDTO, "WorkspaceId must be -1 or a valid workspace artifact id.");
		}

		[Test]
		public void DownloadFile_ThrowsWhenObjectRefIsNull()
		{
			var fileFieldDTO = new FileFieldDTO
			{
				Field = new FileField
				{
					ArtifactID = 1
				},
				FileName = _TEST_FILE_NAME,
				FileStream = new MemoryStream()
			};

			using (fileFieldDTO.FileStream)
			{
				CheckThatDownloadFileThrowsArgumentExceptionWithMessage(-1, fileFieldDTO, "File Field DTO must contain ObjectRef with Artifact ID");
			}
		}

		[Test]
		public void DownloadFile_ThrowsWhenObjectRefArtifactIDIsZero()
		{
			var fileFieldDTO = new FileFieldDTO
			{
				Field = new FileField
				{
					ArtifactID = 1
				},
				ObjectRef = new Artifact(0),
				FileStream = new MemoryStream()
			};

			using (fileFieldDTO.FileStream)
			{
				CheckThatDownloadFileThrowsArgumentExceptionWithMessage(-1, fileFieldDTO, "File Field DTO must contain ObjectRef with Artifact ID");
			}
		}

		[Test]
		public void DownloadFile_ThrowsWhenFileStreamIsClosed()
		{
			var fileFieldDTO = new FileFieldDTO
			{
				Field = new FileField
				{
					ArtifactID = 1
				},
				ObjectRef = new Artifact(1),
				FileStream = new MemoryStream()
			};

			using (fileFieldDTO.FileStream)
			{
				fileFieldDTO.FileStream.Close();
				CheckThatDownloadFileThrowsArgumentExceptionWithMessage(-1, fileFieldDTO, "File Field DTO must contain FileStream that supports writing.");
			}
		}

		[Test]
		public void DownloadFile_ThrowsWhenFileStreamIsNull()
		{
			var fileFieldDTO = new FileFieldDTO
			{
				Field = new FileField
				{
					ArtifactID = 1
				},
				ObjectRef = new Artifact(1),
			};

			CheckThatDownloadFileThrowsArgumentExceptionWithMessage(-1, fileFieldDTO, "File Field DTO must contain FileStream that supports writing.");
		}

		[Test]
		public void DownloadFile_ThrowsWhenFileFieldDoesNotHaveArtifactIdAndName()
		{
			var fileFieldDTO = new FileFieldDTO
			{
				Field = new FileField
				{
					ArtifactID = 0
				},
				ObjectRef = new Artifact(1),
				FileStream = new MemoryStream()
			};

			using (fileFieldDTO.FileStream)
			{
				CheckThatDownloadFileThrowsArgumentExceptionWithMessage(-1, fileFieldDTO, "File Field DTO must contain File with Artifact ID or Name.");
			}
		}

		[Test]
		public void DownloadFile_ThrowsWhenWorkspaceIdIsZero()
		{
			var fileFieldDTO = new FileFieldDTO
			{
				Field = new FileField
				{
					ArtifactID = 1
				},
				ObjectRef = new Artifact(1),
				FileStream = new MemoryStream()
			};

			using (fileFieldDTO.FileStream)
			{
				CheckThatDownloadFileThrowsArgumentExceptionWithMessage(0, fileFieldDTO, "WorkspaceId must be -1 or a valid workspace artifact id.");
			}
		}

		private void CheckThatUploadFileThrowsArgumentExceptionWithMessage(int workspaceId, FileFieldDTO filefieldDto, string message)
		{
			var exception = Assert.Throws<ArgumentException>(() => _sut.UploadFile(workspaceId, filefieldDto));
			exception.Message.Should().Contain(message);
		}

		private void CheckThatDownloadFileThrowsArgumentExceptionWithMessage(int workspaceId, FileFieldDTO filefieldDto, string message)
		{
			var exception = Assert.Throws<ArgumentException>(() => _sut.DownloadFile(workspaceId, filefieldDto));
			exception.Message.Should().Contain(message);
		}
	}
}
