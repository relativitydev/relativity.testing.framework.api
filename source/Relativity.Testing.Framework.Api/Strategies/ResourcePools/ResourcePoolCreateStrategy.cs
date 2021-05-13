using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ResourcePoolCreateStrategy : ICreateStrategy<ResourcePool>
	{
		private readonly IRestService _restService;
		private readonly IGetByIdStrategy<ResourcePool> _getByIdStrategy;

		public ResourcePoolCreateStrategy(
			IRestService restService,
			IGetByIdStrategy<ResourcePool> getByIdStrategy)
		{
			_restService = restService;
			_getByIdStrategy = getByIdStrategy;
		}

		public ResourcePool Create(ResourcePool entity)
		{
			var dto = new
			{
				resourcePoolRequest = new
				{
					Client = new Securable<Artifact>(new Artifact { ArtifactID = entity.Client.ArtifactID }),
					entity.Name,
					entity.IsVisible,
					entity.Keywords,
					entity.Notes
				}
			};

			var artifact = _restService.Post<ResourcePool>("relativity.resourcepools/workspace/-1/resource-pools/", dto);

			return _getByIdStrategy.Get(artifact.ArtifactID);
		}
	}
}
