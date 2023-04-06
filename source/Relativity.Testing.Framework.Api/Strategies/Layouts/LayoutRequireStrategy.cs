using System;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.0")]
	internal class LayoutRequireStrategy : IRequireWorkspaceEntityStrategy<Layout>
	{
		private readonly ICreateWorkspaceEntityStrategy<Layout> _createWorkspaceEntityStrategy;
		private readonly IGetWorkspaceEntityByIdStrategy<Layout> _getWorkspaceEntityByIdStrategy;
		private readonly IGetWorkspaceEntityByNameStrategy<Layout> _getWorkspaceEntityByNameStrategy;
		private readonly IUpdateWorkspaceEntityStrategy<Layout> _updateWorkspaceEntityStrategy;

		public LayoutRequireStrategy(
			ICreateWorkspaceEntityStrategy<Layout> createWorkspaceEntityStrategy,
			IGetWorkspaceEntityByIdStrategy<Layout> getWorkspaceEntityByIdStrategy,
			IGetWorkspaceEntityByNameStrategy<Layout> getWorkspaceEntityByNameStrategy,
			IUpdateWorkspaceEntityStrategy<Layout> updateWorkspaceEntityStrategy)
		{
			_createWorkspaceEntityStrategy = createWorkspaceEntityStrategy;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
			_getWorkspaceEntityByNameStrategy = getWorkspaceEntityByNameStrategy;
			_updateWorkspaceEntityStrategy = updateWorkspaceEntityStrategy;
		}

		public Layout Require(int workspaceId, Layout entity)
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
