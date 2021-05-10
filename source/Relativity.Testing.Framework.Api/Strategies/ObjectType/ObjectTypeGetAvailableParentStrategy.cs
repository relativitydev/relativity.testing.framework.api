using System.Collections.Generic;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ObjectTypeGetAvailableParentStrategy : IGetAvailableParentObjectTypesStrategy
	{
		private readonly IRestService _restService;

		public ObjectTypeGetAvailableParentStrategy(IRestService restService)
		{
			_restService = restService;
		}

		public List<ObjectType> GetAvailableParentObjectTypes(int workspaceId)
		{
			return _restService.Get<List<ObjectType>>($"Relativity.objectTypes/workspace/{workspaceId}/objectTypes/availableparentobjecttypes");
		}
	}
}
