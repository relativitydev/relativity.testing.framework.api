using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ObjectTypeGetByIdStrategy : IGetWorkspaceEntityByIdStrategy<ObjectType>
	{
		private readonly IObjectService _objectService;

		public ObjectTypeGetByIdStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public ObjectType Get(int workspaceId, int entityId)
		{
			return _objectService.Query<ObjectType>().
				For(workspaceId).
				Where(x => x.ArtifactID, entityId).
				FirstOrDefault();
		}
	}
}
