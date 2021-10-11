using System;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Validators;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class FolderUpdateStrategy : IFolderUpdateStrategy
	{
		private readonly IRestService _restService;

		private readonly IFolderGetByIdStrategy _getByIdStrategy;

		private readonly IWorkspaceIdValidator _workspaceArtifactIdValidator;

		private readonly IArtifactIdValidator _artifactIdValidator;

		public FolderUpdateStrategy(
			IRestService restService,
			IFolderGetByIdStrategy getByIdStrategy,
			IWorkspaceIdValidator workspaceArtifactIdValidator,
			IArtifactIdValidator artifactIdValidator)
		{
			_restService = restService;
			_getByIdStrategy = getByIdStrategy;
			_workspaceArtifactIdValidator = workspaceArtifactIdValidator;
			_artifactIdValidator = artifactIdValidator;
		}

		public Folder Update(int workspaceArtifactID, Folder folder)
		{
			_workspaceArtifactIdValidator.Validate(workspaceArtifactID);
			if (folder == null)
			{
				throw new ArgumentNullException(nameof(folder));
			}

			_artifactIdValidator.Validate(folder.ArtifactID, "Folder");
			var dto = new FolderRequest(workspaceArtifactID, folder);

			_restService.Post("Relativity.Services.Folder.IFolderModule/Folder%20Manager/UpdateSingleAsync", dto);

			return _getByIdStrategy.Get(workspaceArtifactID, folder.ArtifactID, folder.ParentFolder?.ArtifactID);
		}
	}
}
