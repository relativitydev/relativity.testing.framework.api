using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Validators;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class FolderGetAccessStatusStrategy : IFolderGetAccessStatusStrategy
	{
		private readonly IRestService _restService;

		private readonly IWorkspaceIdValidator _workspaceArtifactIdValidator;

		private readonly IArtifactIdValidator _artifactIdValidator;

		public FolderGetAccessStatusStrategy(
			IRestService restService,
			IWorkspaceIdValidator workspaceArtifactIdValidator,
			IArtifactIdValidator artifactIdValidator)
		{
			_restService = restService;
			_workspaceArtifactIdValidator = workspaceArtifactIdValidator;
			_artifactIdValidator = artifactIdValidator;
		}

		public FolderAccessStatus Get(int workspaceArtifactID, int folderArtifactID)
		{
			_workspaceArtifactIdValidator.Validate(workspaceArtifactID);
			_artifactIdValidator.Validate(folderArtifactID, "Folder");

			var dto = new
			{
				artifactID = folderArtifactID,
				workspaceArtifactID
			};

			FolderAccessStatusDto result = _restService.Post<FolderAccessStatusDto>("Relativity.Services.Folder.IFolderModule/Folder%20Manager/GetAccessStatusAsync", dto);

			return result.DoMappingToFolderAccessStatus();
		}
	}
}
