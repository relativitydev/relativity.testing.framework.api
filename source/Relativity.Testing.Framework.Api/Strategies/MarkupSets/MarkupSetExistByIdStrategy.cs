using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class MarkupSetExistByIdStrategy : IExistsWorkspaceEntityByIdStrategy<MarkupSet>
	{
		private readonly IObjectService _objectService;

		public MarkupSetExistByIdStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public bool Exists(int workspaceId, int entityId)
		{
			return _objectService.Query<MarkupSet>().
				For(workspaceId).
				FetchOnlyArtifactID().
				Where(x => x.ArtifactID, entityId).
				Any();
		}
	}
}
