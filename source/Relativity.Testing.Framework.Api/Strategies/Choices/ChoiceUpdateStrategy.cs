using System;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ChoiceUpdateStrategy : IUpdateWorkspaceEntityStrategy<Choice>
	{
		private readonly IRestService _restService;

		public ChoiceUpdateStrategy(IRestService restService)
		{
			_restService = restService;
		}

		public void Update(int workspaceId, Choice entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			var dto = new
			{
				ChoiceRequest = new
				{
					entity.Name,
					entity.Order,
					entity.Parent,
					Color = workspaceId == -1 ? null : (int?)entity.Color
				}
			};

			_restService.Put($"Relativity.Choices/workspace/{workspaceId}/choice/{entity.ArtifactID}", dto);
		}
	}
}
