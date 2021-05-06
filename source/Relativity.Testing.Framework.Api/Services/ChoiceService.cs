using System.Collections.Generic;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class ChoiceService : IChoiceService
	{
		private readonly ICreateWorkspaceEntityStrategy<Choice> _createWorkspaceEntityStrategy;
		private readonly IRequireWorkspaceEntityStrategy<Choice> _requireWorkspaceEntityStrategy;
		private readonly IDeleteWorkspaceEntityByIdStrategy<Choice> _deleteWorkspaceEntityByIdStrategy;
		private readonly IGetWorkspaceEntityByIdStrategy<Choice> _getWorkspaceEntityByIdStrategy;
		private readonly IChoiceGetAllByObjectFieldStrategy _getAllByObjectFieldStrategy;
		private readonly IUpdateWorkspaceEntityStrategy<Choice> _updateWorkspaceEntityStrategy;

		public ChoiceService(
			ICreateWorkspaceEntityStrategy<Choice> createWorkspaceEntityStrategy,
			IRequireWorkspaceEntityStrategy<Choice> requireWorkspaceEntityStrategy,
			IDeleteWorkspaceEntityByIdStrategy<Choice> deleteWorkspaceEntityByIdStrategy,
			IGetWorkspaceEntityByIdStrategy<Choice> getWorkspaceEntityByIdStrategy,
			IChoiceGetAllByObjectFieldStrategy getAllByObjectFieldStrategy,
			IUpdateWorkspaceEntityStrategy<Choice> updateWorkspaceEntityStrategy)
		{
			_createWorkspaceEntityStrategy = createWorkspaceEntityStrategy;
			_requireWorkspaceEntityStrategy = requireWorkspaceEntityStrategy;
			_deleteWorkspaceEntityByIdStrategy = deleteWorkspaceEntityByIdStrategy;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
			_getAllByObjectFieldStrategy = getAllByObjectFieldStrategy;
			_updateWorkspaceEntityStrategy = updateWorkspaceEntityStrategy;
		}

		public Choice Create(int workspaceId, Choice entity)
			=> _createWorkspaceEntityStrategy.Create(workspaceId, entity);

		public Choice Require(int workspaceId, Choice entity)
			=> _requireWorkspaceEntityStrategy.Require(workspaceId, entity);

		public void Delete(int workspaceId, int entityId)
			=> _deleteWorkspaceEntityByIdStrategy.Delete(workspaceId, entityId);

		public Choice Get(int workspaceId, int entityId)
			=> _getWorkspaceEntityByIdStrategy.Get(workspaceId, entityId);

		public IEnumerable<Choice> GetAll(int workspaceId, string objectTypeName, string fieldName)
			=> _getAllByObjectFieldStrategy.GetAll(workspaceId, objectTypeName, fieldName);

		public void Update(int workspaceId, Choice entity)
			=> _updateWorkspaceEntityStrategy.Update(workspaceId, entity);
	}
}
