using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Services
{
	public class OcrProfileService : IOcrProfileService
	{
		private readonly ICreateWorkspaceEntityStrategy<OcrProfile> _createWorkspaceEntityStrategy;
		private readonly IDeleteWorkspaceEntityByIdStrategy<OcrProfile> _deleteWorkspaceEntityByIdStrategy;
		private readonly IGetWorkspaceEntityByIdStrategy<OcrProfile> _getWorkspaceEntityByIdStrategy;

		public OcrProfileService(
			ICreateWorkspaceEntityStrategy<OcrProfile> createWorkspaceEntityStrategy,
			IDeleteWorkspaceEntityByIdStrategy<OcrProfile> deleteWorkspaceEntityByIdStrategy,
			IGetWorkspaceEntityByIdStrategy<OcrProfile> getWorkspaceEntityByIdStrategy)
		{
			_createWorkspaceEntityStrategy = createWorkspaceEntityStrategy;
			_deleteWorkspaceEntityByIdStrategy = deleteWorkspaceEntityByIdStrategy;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
		}

		public OcrProfile Create(int workspaceId, OcrProfile entity)
			=> _createWorkspaceEntityStrategy.Create(workspaceId, entity);

		public void Delete(int workspaceId, int entityId)
			=> _deleteWorkspaceEntityByIdStrategy.Delete(workspaceId, entityId);

		public OcrProfile Get(int workspaceId, int entityId)
			=> _getWorkspaceEntityByIdStrategy.Get(workspaceId, entityId);
	}
}
