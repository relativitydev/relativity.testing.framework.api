using System;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ResourcePoolUpdateStrategy : IUpdateStrategy<ResourcePool>
	{
		private readonly IRestService _restService;
		private readonly IGetByNameStrategy<ResourcePool> _getByNameStrategy;
		private readonly IGetByNameStrategy<Client> _getByClientNameStrategy;

		public ResourcePoolUpdateStrategy(
			IRestService restService,
			IGetByNameStrategy<ResourcePool> getByNameStrategy,
			IGetByNameStrategy<Client> getByClientNameStrategy)
		{
			_restService = restService;
			_getByNameStrategy = getByNameStrategy;
			_getByClientNameStrategy = getByClientNameStrategy;
		}

		public void Update(ResourcePool entity)
		{
			ResolveResourcePoolEntity(entity);

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

			_restService.Put($"relativity.resourcepools/workspace/-1/resource-pools/{entity.ArtifactID}", dto);
		}

		private void ResolveResourcePoolEntity(ResourcePool entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			if (entity.ArtifactID == 0)
			{
				if (entity.Name != null)
				{
					var entityByName = _getByNameStrategy.Get(entity.Name);

					if (entityByName == null)
					{
						throw new ArgumentException("Can't find the entity.", nameof(entity));
					}
					else
					{
						entity.ArtifactID = entityByName.ArtifactID;
					}
				}
				else
				{
					throw new ArgumentException("This entity should have an artifact ID or name as an identifier.", nameof(entity));
				}
			}

			if (entity.Client == null)
			{
				throw new ArgumentException("This client should have an artifact ID or name as an identifier.", nameof(entity));
			}
			else
			{
				if (entity.Client.Name != null)
				{
					entity.Client = _getByClientNameStrategy.Get(entity.Client.Name);
				}
				else
				{
					throw new ArgumentException("This client should have a name as an identifier.", nameof(entity));
				}
			}
		}
	}
}
