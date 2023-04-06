using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Validators;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class FolderDeleteUnusedStrategy : IFolderDeleteUnusedStrategy
	{
		private readonly IRestService _restService;

		private readonly IWorkspaceIdValidator _workspaceArtifactIdValidator;

		public FolderDeleteUnusedStrategy(
			IRestService restService,
			IWorkspaceIdValidator workspaceArtifactIdValidator)
		{
			_restService = restService;
			_workspaceArtifactIdValidator = workspaceArtifactIdValidator;
		}

		public QueryResult<Artifact> Delete(int workspaceArtifactID)
		{
			_workspaceArtifactIdValidator.Validate(workspaceArtifactID);

			var dto = new
			{
				workspaceArtifactID
			};

			return _restService.Post<QueryResult<Artifact>>("Relativity.Services.Folder.IFolderModule/Folder%20Manager/DeleteUnusedFoldersAsync", dto);
		}
	}
}
