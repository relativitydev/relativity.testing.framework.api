using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ObjectTypeGetByNameStrategy : IGetWorkspaceEntityByNameStrategy<ObjectType>
	{
		private readonly IObjectService _objectService;

		public ObjectTypeGetByNameStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public ObjectType Get(int workspaceId, string entityName)
		{
			return _objectService.Query<ObjectType>().
				For(workspaceId).
				Where(x => x.Name, entityName).
				FirstOrDefault();
		}
	}
}
