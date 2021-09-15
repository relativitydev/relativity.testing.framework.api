using System;
using System.Collections.Generic;
using System.Linq;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class GroupUpdateStrategy : IUpdateStrategy<Group>
	{
		private readonly IRestService _restService;
		private readonly IGetAllByNamesStrategy<Group> _getAllByNamesStrategy;
		private readonly IGetByIdStrategy<Group> _getByIdStrategy;

		public GroupUpdateStrategy(
			IRestService restService,
			IGetAllByNamesStrategy<Group> getAllByNamesStrategy,
			IGetByIdStrategy<Group> getByIdStrategy)
		{
			_restService = restService;
			_getAllByNamesStrategy = getAllByNamesStrategy;
			_getByIdStrategy = getByIdStrategy;
		}

		public Group Update(Group entity)
		{
			if (entity is null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			if (entity.ArtifactID == 0)
			{
				if (entity.Name != null)
				{
					var entityByName = _getAllByNamesStrategy.GetAll(new List<string> { entity.Name }).FirstOrDefault();

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

			var dto = new
			{
				groupRequest = new
				{
					Client = new
					{
						Secured = false,
						Value = new
						{
							entity.Client.ArtifactID
						}
					},
					entity.Name,
					entity.Keywords,
					entity.Notes
				}
			};

			_restService.Put($"relativity.groups/workspace/-1/groups/{entity.ArtifactID}", dto);

			return _getByIdStrategy.Get(entity.ArtifactID);
		}
	}
}
