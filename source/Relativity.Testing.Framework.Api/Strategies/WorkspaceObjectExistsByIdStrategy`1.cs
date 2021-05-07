using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class WorkspaceObjectExistsByIdStrategy<T> : IExistsWorkspaceEntityByIdStrategy<T>
		where T : Artifact
	{
		private readonly IObjectService _objectService;

		public WorkspaceObjectExistsByIdStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public bool Exists(int workspaceId, int entityId)
		{
			return _objectService.Query<T>().
				FetchOnlyArtifactID().
				For(workspaceId).
				Where(x => x.ArtifactID, entityId).
				Any();
		}
	}
}
