using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Validators;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class FolderGetWorkspaceRootFolderStrategy : IFolderGetWorkspaceRootFolderStrategy
	{
		private readonly IRestService _restService;

		private readonly IWorkspaceIdValidator _workspaceArtifactIdValidator;

		public FolderGetWorkspaceRootFolderStrategy(
			IRestService restService,
			IWorkspaceIdValidator workspaceArtifactIdValidator)
		{
			_restService = restService;
			_workspaceArtifactIdValidator = workspaceArtifactIdValidator;
		}

		public Folder Get(int workspaceArtifactID)
		{
			_workspaceArtifactIdValidator.Validate(workspaceArtifactID);

			var dto = new
			{
				workspaceArtifactID
			};

			FolderDto result = _restService.Post<FolderDto>("Relativity.Services.Folder.IFolderModule/Folder%20Manager/GetWorkspaceRootAsync", dto);

			return result.DoMappingToFolder();
		}
	}
}
