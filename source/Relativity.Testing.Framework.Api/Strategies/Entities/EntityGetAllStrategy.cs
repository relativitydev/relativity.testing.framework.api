using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class EntityGetAllStrategy : IGetAllWorkspaceEntitiesStrategy<Entity>
	{
		private readonly IObjectService _objectService;

		public EntityGetAllStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public Entity[] GetAll(int workspaceId)
		{
			return _objectService.Query<Entity>().
				For(workspaceId).
				ToArray();
		}
	}
}
