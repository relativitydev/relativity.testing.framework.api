using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class MarkupSetService : IMarkupSetService
	{
		private readonly ICreateWorkspaceEntityStrategy<MarkupSet> _createWorkspaceEntityStrategy;
		private readonly IGetWorkspaceEntityByIdStrategy<MarkupSet> _getWorkspaceEntityByIdStrategy;
		private readonly IGetWorkspaceEntityByNameStrategy<MarkupSet> _getWorkspaceEntityByNameStrategy;
		private readonly IRequireWorkspaceEntityStrategy<MarkupSet> _requireWorkspaceEntityStrategy;
		private readonly IExistsWorkspaceEntityByIdStrategy<MarkupSet> _existsWorkspaceEntityByIdStrategy;
		private readonly IUpdateWorkspaceEntityStrategy<MarkupSet> _updateWorkspaceEntityStrategy;
		private readonly IDeleteWorkspaceEntityByIdStrategy<MarkupSet> _deleteWorkspaceEntityByIdStrategy;

		public MarkupSetService(
			ICreateWorkspaceEntityStrategy<MarkupSet> createWorkspaceEntityStrategy,
			IGetWorkspaceEntityByIdStrategy<MarkupSet> getWorkspaceEntityByIdStrategy,
			IGetWorkspaceEntityByNameStrategy<MarkupSet> getWorkspaceEntityByNameStrategy,
			IRequireWorkspaceEntityStrategy<MarkupSet> requireWorkspaceEntityStrategy,
			IExistsWorkspaceEntityByIdStrategy<MarkupSet> existsWorkspaceEntityByIdStrategy,
			IUpdateWorkspaceEntityStrategy<MarkupSet> updateWorkspaceEntityStrategy,
			IDeleteWorkspaceEntityByIdStrategy<MarkupSet> deleteWorkspaceEntityByIdStrategy)
		{
			_createWorkspaceEntityStrategy = createWorkspaceEntityStrategy;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
			_getWorkspaceEntityByNameStrategy = getWorkspaceEntityByNameStrategy;
			_requireWorkspaceEntityStrategy = requireWorkspaceEntityStrategy;
			_existsWorkspaceEntityByIdStrategy = existsWorkspaceEntityByIdStrategy;
			_updateWorkspaceEntityStrategy = updateWorkspaceEntityStrategy;
			_deleteWorkspaceEntityByIdStrategy = deleteWorkspaceEntityByIdStrategy;
		}

		public MarkupSet Create(int workspaceId, MarkupSet entity)
			=> _createWorkspaceEntityStrategy.Create(workspaceId, entity);

		public MarkupSet Get(int workspaceId, int entityId)
			=> _getWorkspaceEntityByIdStrategy.Get(workspaceId, entityId);

		public MarkupSet Get(int workspaceId, string entityName)
			=> _getWorkspaceEntityByNameStrategy.Get(workspaceId, entityName);

		public MarkupSet Require(int workspaceId, MarkupSet entity)
			=> _requireWorkspaceEntityStrategy.Require(workspaceId, entity);

		public bool Exists(int workspaceId, int entityId)
			=> _existsWorkspaceEntityByIdStrategy.Exists(workspaceId, entityId);

		public MarkupSet Update(int workspaceId, MarkupSet entity)
			=> _updateWorkspaceEntityStrategy.Update(workspaceId, entity);

		public void Delete(int workspaceId, int entityId)
			=> _deleteWorkspaceEntityByIdStrategy.Delete(workspaceId, entityId);
	}
}
