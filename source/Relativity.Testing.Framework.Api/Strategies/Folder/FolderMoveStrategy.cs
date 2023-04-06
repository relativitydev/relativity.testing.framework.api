using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Validators;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class FolderMoveStrategy : IFolderMoveStrategy
	{
		private readonly IRestService _restService;
		private readonly IWorkspaceIdValidator _workspaceArtifactIdValidator;
		private readonly IArtifactIdValidator _artifactIdValidator;

		public FolderMoveStrategy(
			IRestService restService,
			IWorkspaceIdValidator workspaceArtifactIdValidator,
			IArtifactIdValidator artifactIdValidator)
		{
			_restService = restService;
			_workspaceArtifactIdValidator = workspaceArtifactIdValidator;
			_artifactIdValidator = artifactIdValidator;
		}

		public FolderMoveResponse Move(int workspaceArtifactID, int folderArtifactID, int destinationFolderArtifactID)
		{
			_workspaceArtifactIdValidator.Validate(workspaceArtifactID);
			_artifactIdValidator.Validate(folderArtifactID, "Folder");
			_artifactIdValidator.Validate(destinationFolderArtifactID, "Destination Folder");

			var dto = new
			{
				artifactID = folderArtifactID,
				destinationFolderID = destinationFolderArtifactID,
				workspaceArtifactID
			};

			FolderMoveResponseDto result = _restService.Post<FolderMoveResponseDto>("Relativity.Services.Folder.IFolderModule/Folder%20Manager/MoveFolderAsync", dto);

			return result.DoMappingToFolderMoveResponse();
		}
	}
}
