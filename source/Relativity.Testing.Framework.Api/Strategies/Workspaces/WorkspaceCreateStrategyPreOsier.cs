using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class WorkspaceCreateStrategyPreOsier : WorkspaceCreateAbstractStrategy
	{
		private readonly int _timeout = 5;
		private readonly IRestService _restService;

		public WorkspaceCreateStrategyPreOsier(
			IRestService restService,
			IGetByIdStrategy<Workspace> getWorkspaceByIdStrategy,
			IWorkspaceFillRequiredPropertiesStrategy workspaceFillRequiredPropertiesStrategy)
				: base(getWorkspaceByIdStrategy, workspaceFillRequiredPropertiesStrategy)
		{
			_restService = restService;
		}

		protected override int CreateWorkspace(object workspaceToCreate)
		{
			var result = _restService.Post<JObject>("Relativity.Workspaces/workspace/", workspaceToCreate, _timeout);

			return int.Parse(result[nameof(Artifact.ArtifactID)].ToString());
		}
	}
}
