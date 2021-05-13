using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ResourcePoolExistByIdStrategy : IExistsByIdStrategy<ResourcePool>
	{
		private readonly IObjectService _objectService;

		public ResourcePoolExistByIdStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public bool Exists(int id)
		{
			return _objectService.Query<ResourcePool>().
				FetchOnlyArtifactID().
				Where(x => x.ArtifactID, id).
				Any();
		}
	}
}
