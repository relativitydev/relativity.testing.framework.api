using System;
using System.Linq;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ChoiceRequireStrategy : IRequireWorkspaceEntityStrategy<Choice>
	{
		private readonly ICreateWorkspaceEntityStrategy<Choice> _createWorkspaceEntityStrategy;
		private readonly IGetWorkspaceEntityByIdStrategy<Choice> _getWorkspaceEntityByIdStrategy;
		private readonly IChoiceGetAllByObjectFieldStrategy _getAllByObjectFieldStrategy;
		private readonly IUpdateWorkspaceEntityStrategy<Choice> _updateWorkspaceEntityStrategy;

		public ChoiceRequireStrategy(
			ICreateWorkspaceEntityStrategy<Choice> createWorkspaceEntityStrategy,
			IGetWorkspaceEntityByIdStrategy<Choice> getWorkspaceEntityByIdStrategy,
			IChoiceGetAllByObjectFieldStrategy getAllByObjectFieldStrategy,
			IUpdateWorkspaceEntityStrategy<Choice> updateWorkspaceEntityStrategy)
		{
			_createWorkspaceEntityStrategy = createWorkspaceEntityStrategy;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
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
				_updateWorkspaceEntityStrategy.Update(workspaceId, entity);
				return _getWorkspaceEntityByIdStrategy.Get(workspaceId, entity.ArtifactID);
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
					_updateWorkspaceEntityStrategy.Update(workspaceId, entity);
					return _getWorkspaceEntityByIdStrategy.Get(workspaceId, entity.ArtifactID);
				}
			}

			return _createWorkspaceEntityStrategy.Create(workspaceId, entity);
		}
	}
}
