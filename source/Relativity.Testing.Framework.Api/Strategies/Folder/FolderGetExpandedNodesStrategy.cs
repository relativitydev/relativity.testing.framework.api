using System.Collections.Generic;
using System.Linq;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Validators;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class FolderGetExpandedNodesStrategy : IFolderGetExpandedNodesStrategy
	{
		private readonly IRestService _restService;

		private readonly IWorkspaceIdValidator _workspaceArtifactIdValidator;

		public FolderGetExpandedNodesStrategy(
			IRestService restService,
			IWorkspaceIdValidator workspaceArtifactIdValidator)
		{
			_restService = restService;
			_workspaceArtifactIdValidator = workspaceArtifactIdValidator;
		}

		public List<Folder> Get(int workspaceArtifactID, List<int> expandedNodesArtifactIDs, int selectedFolderArtifactID = 0)
		{
			_workspaceArtifactIdValidator.Validate(workspaceArtifactID);

			var dto = new
			{
				expandedNodes = expandedNodesArtifactIDs,
				selectedFolderId = selectedFolderArtifactID,
				workspaceArtifactId = workspaceArtifactID
			};

			List<FolderDto> result = _restService.Post<List<FolderDto>>("Relativity.Services.Folder.IFolderModule/Folder%20Manager/GetFolderTreeAsync", dto);

			return result.Select(folderDto => folderDto.DoMappingToFolder()).ToList();
		}
	}
}
