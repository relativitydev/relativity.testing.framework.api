using System;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Validators;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class FolderQueryStrategy : IFolderQueryStrategy
	{
		private readonly IRestService _restService;

		private readonly IWorkspaceIdValidator _workspaceIdValidator;

		public FolderQueryStrategy(IRestService restService, IWorkspaceIdValidator workspaceIdValidator)
		{
			_restService = restService;
			_workspaceIdValidator = workspaceIdValidator;
		}

		public QueryResult<NamedArtifact> Query(int workspaceArtifactID, Query query, int length = 0)
		{
			if (query == null)
			{
				throw new ArgumentNullException(nameof(query));
			}

			_workspaceIdValidator.Validate(workspaceArtifactID);

			var folderQueryRequest = new FolderQueryRequest(workspaceArtifactID, query, length);

			return _restService.Post<QueryResult<NamedArtifact>>("Relativity.Services.Folder.IFolderModule/Folder%20Manager/QueryAsync", folderQueryRequest);
		}
	}
}
