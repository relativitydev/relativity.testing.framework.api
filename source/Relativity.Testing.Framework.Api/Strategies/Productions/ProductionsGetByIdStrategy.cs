using System.Net.Http;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ProductionsGetByIdStrategy : IGetWorkspaceEntityByIdStrategy<Production>
	{
		private readonly IRestService _restService;
		private readonly IExistsWorkspaceEntityByIdStrategy<Production> _existsWorkspaceEntityByIdStrategy;

		public ProductionsGetByIdStrategy(
			IRestService restService,
			IExistsWorkspaceEntityByIdStrategy<Production> existsWorkspaceEntityByIdStrategy)
		{
			_restService = restService;
			_existsWorkspaceEntityByIdStrategy = existsWorkspaceEntityByIdStrategy;
		}

		public Production Get(int workspaceId, int entityId)
		{
			if (!_existsWorkspaceEntityByIdStrategy.Exists(workspaceId, entityId))
			{
				return null;
			}

			var dto = new
			{
				workspaceArtifactID = workspaceId,
				productionArtifactID = entityId,
				DataSourceReadMode = 2
			};

			try
			{
				return _restService.Post<Production>("Relativity.Productions.Services.IProductionModule/Production%20Manager/ReadSingleAsync", dto);
			}

			// If the production is deleted after we check to see if it exists, but before we get to the POST, we're going to get
			// an exception when we try to return it. But if we check it's existence once more, that should give us the right value.
			catch (HttpRequestException ex)
			{
				if (!ex.Message.Contains("Production specified is invalid."))
				{
					throw;
				}

				return !_existsWorkspaceEntityByIdStrategy.Exists(workspaceId, entityId) ?
					null :
					_restService.Post<Production>("Relativity.Productions.Services.IProductionModule/Production%20Manager/ReadSingleAsync", dto);
			}
		}
	}
}
