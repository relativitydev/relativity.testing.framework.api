using Relativity.Testing.Framework.Api.DTO;
using Relativity.Testing.Framework.Api.Extensions;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class WorkspaceGetByIdStrategyV1 : IGetByIdStrategy<Workspace>
	{
		private readonly IRestService _restService;
		private readonly IExistsByIdStrategy<Workspace> _existsByIdStrategy;

		public WorkspaceGetByIdStrategyV1(IRestService restService, IExistsByIdStrategy<Workspace> existsByIdStrategy)
		{
			_restService = restService;
			_existsByIdStrategy = existsByIdStrategy;
		}

		public Workspace Get(int id)
		{
			if (!_existsByIdStrategy.Exists(id))
			{
				return null;
			}

			WorkspaceDTO response = _restService.Get<WorkspaceDTO>($"relativity-environment/V1/workspace/{id}");

			return response.MapToWorkspace();
		}
	}
}
