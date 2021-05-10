using System.Linq;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class LibraryApplicationIsInstalledInWorkspaceStrategy : ILibraryApplicationIsInstalledInWorkspaceStrategy
	{
		private readonly IRestService _restService;

		public LibraryApplicationIsInstalledInWorkspaceStrategy(IRestService restService)
		{
			_restService = restService;
		}

		public bool IsInstalledInWorkspace(int workspaceId, int applicationId)
		{
			object dto = new
			{
				queryOptions = new
				{
					Condition = $"'Case Artifact ID' == {workspaceId}"
				}
			};

			var response = _restService.Post<ArrayResponse<LibraryApplicationInstallStatusResponse>>(
				$"Relativity.LibraryApplications/workspace/-1/libraryapplications/{applicationId}/install/search?start1&length={int.MaxValue}", dto);

			var installStatus = response.Results.OrderByDescending(x => x.ApplicationInstallID)
				.FirstOrDefault(x => x.WorkspaceIdentifier.ArtifactID == workspaceId)?.InstallStatus.Code;

			return (installStatus != null) && installStatus == RelativityApplicationInstallStatusCode.Completed;
		}
	}
}
