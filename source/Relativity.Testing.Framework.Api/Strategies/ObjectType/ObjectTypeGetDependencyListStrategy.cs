using System.Collections.Generic;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ObjectTypeGetDependencyListStrategy : IGetDependencyListForWorkspaceEntityStrategy<ObjectType>
	{
		private readonly IRestService _restService;

		public ObjectTypeGetDependencyListStrategy(IRestService restService)
		{
			_restService = restService;
		}

		public List<Dependency> GetDependencies(int workspaceId, int entityId)
		{
			return _restService.Get<List<Dependency>>($"Relativity.objectTypes/workspace/{workspaceId}/objectTypes/{entityId}/dependencyList");
		}
	}
}
