using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class DocumentGetByControlNumberStrategy : IGetWorkspaceEntityByNameStrategy<Document>
	{
		private readonly IObjectService _objectService;

		public DocumentGetByControlNumberStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public Document Get(int workspaceId, string entityName)
		{
			return _objectService.Query<Document>().
				For(workspaceId).
				Where(x => x.ControlNumber, entityName).
				FirstOrDefault();
		}
	}
}
