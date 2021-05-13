using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ProductionsDataSourceExistsByIdStrategy : IExistsWorkspaceEntityByIdStrategy<ProductionDataSource>
	{
		private readonly IObjectService _objectService;

		public ProductionsDataSourceExistsByIdStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public bool Exists(int workspaceId, int entityId)
		{
			return _objectService.Query<ProductionDataSource>()
				.For(workspaceId)
				.FetchOnlyArtifactID()
				.Where(x => x.ArtifactID, entityId)
				.Any();
		}
	}
}
