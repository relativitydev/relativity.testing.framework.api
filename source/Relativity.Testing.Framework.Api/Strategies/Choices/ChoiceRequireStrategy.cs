using System;
using System.Linq;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ChoiceRequireStrategy : IRequireWorkspaceEntityStrategy<Choice>
	{
		private readonly ICreateWorkspaceEntityStrategy<Choice> _createWorkspaceEntityStrategy;
		private readonly IChoiceGetAllByObjectFieldStrategy _getAllByObjectFieldStrategy;
		private readonly IUpdateWorkspaceEntityStrategy<Choice> _updateWorkspaceEntityStrategy;

		public ChoiceRequireStrategy(
			ICreateWorkspaceEntityStrategy<Choice> createWorkspaceEntityStrategy,
			IChoiceGetAllByObjectFieldStrategy getAllByObjectFieldStrategy,
			IUpdateWorkspaceEntityStrategy<Choice> updateWorkspaceEntityStrategy)
		{
			_createWorkspaceEntityStrategy = createWorkspaceEntityStrategy;
			_getAllByObjectFieldStrategy = getAllByObjectFieldStrategy;
			_updateWorkspaceEntityStrategy = updateWorkspaceEntityStrategy;
		}

		public Choice Require(int workspaceId, Choice entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			if (entity.ArtifactID != 0)
			{
				return _updateWorkspaceEntityStrategy.Update(workspaceId, entity);
			}

			if (entity.Name != null && entity.ObjectType?.Name != null && entity.Field?.Name != null)
			{
				var existedEntity = _getAllByObjectFieldStrategy.GetAll(workspaceId, entity.ObjectType.Name, entity.Field.Name).
					FirstOrDefault(x => x.Name == entity.Name);

				if (existedEntity == null)
				{
					return _createWorkspaceEntityStrategy.Create(workspaceId, entity);
				}
				else
				{
					entity.ArtifactID = existedEntity.ArtifactID;
					return _updateWorkspaceEntityStrategy.Update(workspaceId, entity);
				}
			}

			return _createWorkspaceEntityStrategy.Create(workspaceId, entity);
		}
	}
}
