using System;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class MarkupSetRequireStrategy : IRequireWorkspaceEntityStrategy<MarkupSet>
	{
		private readonly ICreateWorkspaceEntityStrategy<MarkupSet> _createWorkspaceEntityStrategy;
		private readonly IGetWorkspaceEntityByIdStrategy<MarkupSet> _getWorkspaceEntityByIdStrategy;
		private readonly IGetWorkspaceEntityByNameStrategy<MarkupSet> _getWorkspaceEntityByNameStrategy;
		private readonly IUpdateWorkspaceEntityStrategy<MarkupSet> _updateWorkspaceEntityStrategy;

		public MarkupSetRequireStrategy(
			ICreateWorkspaceEntityStrategy<MarkupSet> createWorkspaceEntityStrategy,
			IGetWorkspaceEntityByIdStrategy<MarkupSet> getWorkspaceEntityByIdStrategy,
			IGetWorkspaceEntityByNameStrategy<MarkupSet> getWorkspaceEntityByNameStrategy,
			IUpdateWorkspaceEntityStrategy<MarkupSet> updateWorkspaceEntityStrategy)
		{
			_createWorkspaceEntityStrategy = createWorkspaceEntityStrategy;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
			_getWorkspaceEntityByNameStrategy = getWorkspaceEntityByNameStrategy;
			_updateWorkspaceEntityStrategy = updateWorkspaceEntityStrategy;
		}

		public MarkupSet Require(int workspaceId, MarkupSet entity)
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

			if (entity.Name != null)
			{
				var existedEntity = _getWorkspaceEntityByNameStrategy.Get(workspaceId, entity.Name);
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
