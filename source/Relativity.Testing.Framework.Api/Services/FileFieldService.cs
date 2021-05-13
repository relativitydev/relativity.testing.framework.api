using System;
using Relativity.Testing.Framework.Api.Attributes;
using Relativity.Testing.Framework.Api.Extensions;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	[DoNotRetry]
	internal class FileFieldService : IFileFieldService
	{
		private readonly IFileFieldUploadStrategy _fileFieldUploadStrategy;

		private readonly IFileFieldDownloadStrategy _fileFieldDownloadStrategy;

		public FileFieldService(
			IFileFieldUploadStrategy fileFieldUploadStrategy,
			IFileFieldDownloadStrategy fileFieldDownloadStrategy)
		{
			_fileFieldUploadStrategy = fileFieldUploadStrategy;
			_fileFieldDownloadStrategy = fileFieldDownloadStrategy;
		}

		public FileFieldDTO UploadFile(int workspaceId, FileFieldDTO fileFieldDto)
		{
			ValidateInputForFileUpload(workspaceId, fileFieldDto);

			var result = _fileFieldUploadStrategy.UploadFile(workspaceId, fileFieldDto);
			return result;
		}

		private static void ValidateInputForFileUpload(int workspaceId, FileFieldDTO fileFieldDto)
		{
			ValidateWorkspaceID(workspaceId);
			fileFieldDto.ValidateFileFieldDTO();
			fileFieldDto.ValidateFileName();
			fileFieldDto.ValidateFieldAndObjectRef();
		}

		public FileFieldDTO DownloadFile(int workspaceId, FileFieldDTO fileFieldDto)
		{
			ValidateInputForFileDownload(workspaceId, fileFieldDto);

			var result = _fileFieldDownloadStrategy.DownloadFile(workspaceId, fileFieldDto);
			return result;
		}

		private static void ValidateInputForFileDownload(int workspaceId, FileFieldDTO fileFieldDto)
		{
			ValidateWorkspaceID(workspaceId);
			fileFieldDto.ValidateFileFieldDTO();
			fileFieldDto.ValidateFieldAndObjectRef();
			fileFieldDto.ValidatFileStream();
		}

		private static void ValidateWorkspaceID(int workspaceId)
		{
			if (workspaceId == 0 || workspaceId < -1)
			{
				throw new ArgumentException("WorkspaceId must be -1 or a valid workspace artifact id.");
			}
		}
	}
}
