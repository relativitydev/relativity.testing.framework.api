using System;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class MatterUpdateStrategy : IUpdateStrategy<Matter>
	{
		private readonly IRestService _restService;
		private readonly IMatterStatusGetChoiceIdByNameStrategy _matterStatusGetChoiceIdByNameStrategy;
		private readonly IMatterGetByNameAndClientIdStrategy _matterGetByNameAndClientIdStrategy;

		public MatterUpdateStrategy(
			IRestService restService,
			IMatterStatusGetChoiceIdByNameStrategy matterStatusGetChoiceIdByNameStrategy,
			IMatterGetByNameAndClientIdStrategy matterGetByNameAndClientIdStrategy)
		{
			_restService = restService;
			_matterStatusGetChoiceIdByNameStrategy = matterStatusGetChoiceIdByNameStrategy;
			_matterGetByNameAndClientIdStrategy = matterGetByNameAndClientIdStrategy;
		}

		public void Update(Matter entity)
		{
			if (entity is null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			if (entity.ArtifactID == 0)
			{
				if (entity.Name != null && entity.Client != null)
				{
					var entityByName = _matterGetByNameAndClientIdStrategy.Get(entity.Name, entity.Client.ArtifactID);

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
					throw new ArgumentException("This entity should have an artifact ID or name and client id as an identifier.", nameof(entity));
				}
			}

			var dto = new
			{
				MatterRequest = new
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
					entity.Number,
					Status = new
					{
						Secured = false,
						Value = new
						{
							ArtifactId = GetStatusId(entity.Status)
						}
					},
					entity.Keywords,
					entity.Notes
				}
			};

			_restService.Put($"relativity.matters/workspace/-1/matters/{entity.ArtifactID}", dto);
		}

		private int GetStatusId(string status)
		{
			return _matterStatusGetChoiceIdByNameStrategy.GetId(status);
		}
	}
}
