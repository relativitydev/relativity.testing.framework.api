using System;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ChoiceCreateStrategy : CreateWorkspaceEntityStrategy<Choice>
	{
		private readonly IRestService _restService;
		private readonly IGetWorkspaceEntityByIdStrategy<Choice> _getWorkspaceEntityByIdStrategy;
		private readonly IGetWorkspaceEntityByNameStrategy<Field> _getFieldByNameStrategy;

		public ChoiceCreateStrategy(
			IRestService restService,
			IGetWorkspaceEntityByIdStrategy<Choice> getWorkspaceEntityByIdStrategy,
			IGetWorkspaceEntityByNameStrategy<Field> getFieldByNameStrategy)
		{
			_restService = restService;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
			_getFieldByNameStrategy = getFieldByNameStrategy;
		}

		protected override Choice DoCreate(int workspaceId, Choice entity)
		{
			Validate(workspaceId, entity);
			entity.FillRequiredProperties();

			ChoiceRequest choiceRequest = ChoiceExtensions.ToChoiceRequest(entity);

			var dto = new
			{
				choiceRequest = choiceRequest
			};

			int artifactId = _restService.Post<int>($"Relativity.Choices/workspace/{workspaceId}/choice", dto);

			return _getWorkspaceEntityByIdStrategy.Get(workspaceId, artifactId);
		}

		private void Validate(int workspaceId, Choice entity)
		{
			if (entity.Field == null)
			{
				throw new ArgumentException($"{nameof(Choice)} model should have {nameof(Choice.Field)} set.", nameof(entity));
			}
			else if (entity.Field.ArtifactID == 0 && entity.Field.Name != null)
			{
				entity.Field = _getFieldByNameStrategy.Get(workspaceId, entity.Field.Name);

				if (entity.Field == null)
				{
					throw ObjectNotFoundException.CreateForNotFoundByName<Field>(entity.Field.Name);
				}
			}
		}
	}
}
