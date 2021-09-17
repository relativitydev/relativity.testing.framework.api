using System;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class MarkupSetUpdateStrategy : IUpdateWorkspaceEntityStrategy<MarkupSet>
	{
		private readonly IObjectService _objectService;
		private readonly IGetWorkspaceEntityByIdStrategy<MarkupSet> _getWorkspaceEntityByIdStrategy;

		public MarkupSetUpdateStrategy(IObjectService objectService, IGetWorkspaceEntityByIdStrategy<MarkupSet> getWorkspaceEntityByIdStrategy)
		{
			_objectService = objectService;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
		}

		public MarkupSet Update(int workspaceId, MarkupSet entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			_objectService.Update(workspaceId, entity);

			return _getWorkspaceEntityByIdStrategy.Get(workspaceId, entity.ArtifactID);
		}
	}
}
