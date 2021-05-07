using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class DocumentGetAllStrategy : IGetAllWorkspaceEntitiesStrategy<Document>
	{
		private readonly IObjectService _objectService;

		public DocumentGetAllStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public Document[] GetAll(int workspaceId)
		{
			return _objectService.Query<Document>().
				For(workspaceId).
				ToArray();
		}
	}
}
