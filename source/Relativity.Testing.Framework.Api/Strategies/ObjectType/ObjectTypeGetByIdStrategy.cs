using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ObjectTypeGetByIdStrategy : IGetWorkspaceEntityByIdStrategy<ObjectType>
	{
		private readonly IRestService _restService;

		public ObjectTypeGetByIdStrategy(IRestService restService)
		{
			_restService = restService;
		}

		public ObjectType Get(int workspaceId, int entityId)
		{
			return _restService.Get<ObjectType>($"relativity.objectTypes/workspace/{workspaceId}/objectTypes/{entityId}");
		}
	}
}
