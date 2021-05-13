using System;
using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ViewGetByNameStrategy : IGetWorkspaceEntityByNameStrategy<View>
	{
		private readonly IObjectService _objectService;
		private readonly IGetWorkspaceEntityByIdStrategy<View> _getWorkspaceEntityByIdStrategy;

		public ViewGetByNameStrategy(
			IObjectService objectService,
			IGetWorkspaceEntityByIdStrategy<View> getWorkspaceEntityByIdStrategy)
		{
			_objectService = objectService;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
		}

		public View Get(int workspaceId, string entityName)
		{
			if (entityName == null)
			{
				throw new ArgumentNullException(nameof(entityName));
			}

			var artifact = _objectService.Query<View>().
				For(workspaceId).
				Where(x => x.Name, entityName).
				FirstOrDefault();

			if (artifact == null)
			{
				return null;
			}

			return _getWorkspaceEntityByIdStrategy.Get(workspaceId, artifact.ArtifactID);
		}
	}
}
