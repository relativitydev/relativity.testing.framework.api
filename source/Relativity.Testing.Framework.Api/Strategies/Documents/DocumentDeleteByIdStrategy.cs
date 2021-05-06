using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class DocumentDeleteByIdStrategy : IDeleteWorkspaceEntityByIdStrategy<Document>
	{
		private readonly IObjectService _objectService;

		public DocumentDeleteByIdStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public void Delete(int workspaceId, int entityId)
		{
			_objectService.Delete(workspaceId, entityId);
		}
	}
}
