using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class EntityExistByIdStrategy : IExistsWorkspaceEntityByIdStrategy<Entity>
	{
		private readonly IObjectService _objectService;

		public EntityExistByIdStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public bool Exists(int workspaceId, int entityId)
		{
			return _objectService.Query<Entity>().
				For(workspaceId).
				FetchOnlyArtifactID().
				Where(x => x.ArtifactID, entityId).
				Any();
		}
	}
}
