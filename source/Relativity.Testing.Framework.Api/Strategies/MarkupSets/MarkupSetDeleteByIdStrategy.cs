using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class MarkupSetDeleteByIdStrategy : DeleteWorkspaceEntityByIdStrategy<MarkupSet>
	{
		private readonly IObjectService _objectService;
		private readonly IExistsWorkspaceEntityByIdStrategy<MarkupSet> _existsWorkspaceEntityByIdStrategy;

		public MarkupSetDeleteByIdStrategy(
			IObjectService objectService,
			IExistsWorkspaceEntityByIdStrategy<MarkupSet> existsWorkspaceEntityByIdStrategy)
		{
			_objectService = objectService;
			_existsWorkspaceEntityByIdStrategy = existsWorkspaceEntityByIdStrategy;
		}

		protected override void DoDelete(int workspaceId, int entityId)
		{
			if (!_existsWorkspaceEntityByIdStrategy.Exists(workspaceId, entityId))
				throw new ObjectNotFoundException();

			_objectService.Delete(workspaceId, entityId);
		}
	}
}
