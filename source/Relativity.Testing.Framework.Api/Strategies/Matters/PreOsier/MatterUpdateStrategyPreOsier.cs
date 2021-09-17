using System;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class MatterUpdateStrategyPreOsier : IMatterUpdateStrategy
	{
		private readonly IRestService _restService;
		private readonly IMatterStatusGetChoiceIdByNameStrategy _matterStatusGetChoiceIdByNameStrategy;
		private readonly IMatterGetByNameAndClientIdStrategy _matterGetByNameAndClientIdStrategy;
		private readonly IMatterGetByIdStrategy _matterGetByIdStrategy;

		public MatterUpdateStrategyPreOsier(
			IRestService restService,
			IMatterStatusGetChoiceIdByNameStrategy matterStatusGetChoiceIdByNameStrategy,
			IMatterGetByNameAndClientIdStrategy matterGetByNameAndClientIdStrategy,
			IMatterGetByIdStrategy matterGetByIdStrategy)
		{
			_restService = restService;
			_matterStatusGetChoiceIdByNameStrategy = matterStatusGetChoiceIdByNameStrategy;
			_matterGetByNameAndClientIdStrategy = matterGetByNameAndClientIdStrategy;
			_matterGetByIdStrategy = matterGetByIdStrategy;
		}

		public Matter Update(Matter entity, bool restrictedUpdate = false)
		{
			ValidateEntity(entity);

			if (entity.ArtifactID == 0)
			{
				if (!string.IsNullOrWhiteSpace(entity.Name) && entity.Client != null && entity.Client.ArtifactID > 0)
				{
					TryFillMatterArtifactIdByNameAndClientId(entity);
				}
				else
				{
					throw new ArgumentException("This entity should have an artifact ID or name and client id as an identifier.", nameof(entity));
				}
			}

			int statusID = GetStatusId(entity.Status);
			var updateRequest = new MatterUpdateRequest(entity, statusID, restrictedUpdate);

			_restService.Put($"relativity.matters/workspace/-1/matters/{entity.ArtifactID}", updateRequest);

			return _matterGetByIdStrategy.Get(entity.ArtifactID);
		}

		private static void ValidateEntity(Matter entity)
		{
			if (entity is null)
			{
				throw new ArgumentNullException(nameof(entity));
			}
		}

		private void TryFillMatterArtifactIdByNameAndClientId(Matter entity)
		{
			Matter entityByName = _matterGetByNameAndClientIdStrategy
				.Get(entity.Name, entity.Client.ArtifactID);

			if (entityByName == null)
			{
				throw new ArgumentException($"Can't find matter entity by Name and ClientID.", nameof(entity));
			}
			else
			{
				entity.ArtifactID = entityByName.ArtifactID;
			}
		}

		private int GetStatusId(string status)
		{
			return _matterStatusGetChoiceIdByNameStrategy.GetId(status);
		}
	}
}
