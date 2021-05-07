using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class MarkupSetGetByIdStrategy : IGetWorkspaceEntityByIdStrategy<MarkupSet>
	{
		private readonly IObjectService _objectService;
		private readonly IExistsWorkspaceEntityByIdStrategy<MarkupSet> _existsWorkspaceEntityByIdStrategy;

		public MarkupSetGetByIdStrategy(
			IObjectService objectService,
			IExistsWorkspaceEntityByIdStrategy<MarkupSet> existsWorkspaceEntityByIdStrategy)
		{
			_objectService = objectService;
			_existsWorkspaceEntityByIdStrategy = existsWorkspaceEntityByIdStrategy;
		}

		public MarkupSet Get(int workspaceId, int entityId)
		{
			if (!_existsWorkspaceEntityByIdStrategy.Exists(workspaceId, entityId))
			{
				return null;
			}

			return _objectService.Query<MarkupSet>().
				For(workspaceId).
				Where(x => x.ArtifactID, entityId).
				FirstOrDefault();
		}
	}
}
