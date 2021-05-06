using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ViewGetByAllStrategy : IGetAllWorkspaceEntitiesStrategy<View>
	{
		private readonly IObjectService _objectService;
		private readonly IGetWorkspaceEntityByIdStrategy<View> _getWorkspaceEntityByIdStrategy;

		public ViewGetByAllStrategy(
			IObjectService objectService,
			IGetWorkspaceEntityByIdStrategy<View> getWorkspaceEntityByIdStrategy)
		{
			_objectService = objectService;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
		}

		public View[] GetAll(int workspaceId)
		{
			var artifacts = _objectService.Query<View>().
				For(workspaceId).
				ToArray();

			return artifacts;
		}
	}
}
