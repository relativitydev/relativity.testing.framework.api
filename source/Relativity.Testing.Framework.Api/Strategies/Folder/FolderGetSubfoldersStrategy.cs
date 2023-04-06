using System.Collections.Generic;
using System.Linq;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Validators;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class FolderGetSubfoldersStrategy : IFolderGetSubfoldersStrategy
	{
		private readonly IRestService _restService;

		private readonly IWorkspaceIdValidator _workspaceArtifactIdValidator;

		private readonly IArtifactIdValidator _artifactIdValidator;

		public FolderGetSubfoldersStrategy(
			IRestService restService,
			IWorkspaceIdValidator workspaceArtifactIdValidator,
			IArtifactIdValidator artifactIdValidator)
		{
			_restService = restService;
			_workspaceArtifactIdValidator = workspaceArtifactIdValidator;
			_artifactIdValidator = artifactIdValidator;
		}

		public List<Folder> Get(int workspaceArtifactID, int parentFolderArtifactID)
		{
			_workspaceArtifactIdValidator.Validate(workspaceArtifactID);
			_artifactIdValidator.Validate(parentFolderArtifactID, "Parent Folder");

			var dto = new
			{
				parentID = parentFolderArtifactID,
				workspaceArtifactID
			};

			List<FolderDto> result = _restService.Post<List<FolderDto>>("Relativity.Services.Folder.IFolderModule/Folder%20Manager/GetChildrenAsync", dto);

			return result.Select(folderDto => folderDto.DoMappingToFolder()).ToList();
		}
	}
}
