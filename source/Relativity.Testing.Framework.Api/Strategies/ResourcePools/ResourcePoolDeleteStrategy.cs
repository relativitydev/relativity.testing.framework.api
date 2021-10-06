using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ResourcePoolDeleteStrategy : DeleteByIdStrategy<ResourcePool>
	{
		private readonly IRestService _restService;
		private readonly IExistsByIdStrategy<ResourcePool> _existsByIdStrategy;

		public ResourcePoolDeleteStrategy(
			IRestService restService,
			IExistsByIdStrategy<ResourcePool> existsByIdStrategy)
		{
			_restService = restService;
			_existsByIdStrategy = existsByIdStrategy;
		}

		protected override void DoDelete(int id)
		{
			if (!_existsByIdStrategy.Exists(id))
			{
				throw new ObjectNotFoundException();
			}

			_restService.Delete($"relativity.resourcepools/workspace/-1/resource-pools/{id}");
		}
	}
}
