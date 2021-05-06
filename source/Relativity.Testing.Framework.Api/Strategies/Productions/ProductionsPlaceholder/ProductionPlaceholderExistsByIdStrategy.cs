using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ProductionPlaceholderExistsByIdStrategy : IExistsWorkspaceEntityByIdStrategy<ProductionPlaceholder>
	{
		private readonly IObjectService _objectService;

		public ProductionPlaceholderExistsByIdStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public bool Exists(int workspaceId, int entityId)
		{
			return _objectService.Query<ProductionPlaceholder>()
				.For(workspaceId)
				.FetchOnlyArtifactID()
				.Where(x => x.ArtifactID, entityId)
				.Any();
		}
	}
}
