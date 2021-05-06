using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ObjectTypeDeleteByIdStrategy : DeleteWorkspaceEntityByIdStrategy<ObjectType>
	{
		private readonly IRestService _restService;

		public ObjectTypeDeleteByIdStrategy(IRestService restService)
		{
			_restService = restService;
		}

		protected override void DoDelete(int workspaceId, int entityId)
		{
			_restService.Delete($"relativity.objectTypes/workspace/{workspaceId}/objectTypes/{entityId}");
		}
	}
}
