using System;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Validators;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class FolderCreateStrategy : IFolderCreateStrategy
	{
		private readonly IRestService _restService;

		private readonly IFolderGetByIdStrategy _getByIdStrategy;

		private readonly IWorkspaceIdValidator _workspaceIdValidator;

		public FolderCreateStrategy(
			IRestService restService,
			IFolderGetByIdStrategy getByIdStrategy,
			IWorkspaceIdValidator workspaceIdValidator)
		{
			_restService = restService;
			_getByIdStrategy = getByIdStrategy;
			_workspaceIdValidator = workspaceIdValidator;
		}

		public Folder Create(int workspaceArtifactID, Folder folder)
		{
			_workspaceIdValidator.Validate(workspaceArtifactID);

			if (folder == null)
			{
				throw new ArgumentNullException(nameof(folder));
			}

			var dto = new FolderRequest(workspaceArtifactID, folder);

			int folderArtifactID = _restService.Post<int>("Relativity.Services.Folder.IFolderModule/Folder%20Manager/CreateSingleAsync", dto);

			return _getByIdStrategy.Get(workspaceArtifactID, folderArtifactID, folder.ParentFolder?.ArtifactID);
		}
	}
}
