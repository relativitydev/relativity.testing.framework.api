using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class MarkupSetGetByNameStrategy : IGetWorkspaceEntityByNameStrategy<MarkupSet>
	{
		private readonly IObjectService _objectService;

		public MarkupSetGetByNameStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public MarkupSet Get(int workspaceId, string entityName)
		{
			return _objectService.Query<MarkupSet>().
				For(workspaceId).
				Where(x => x.Name, entityName).
				FirstOrDefault();
		}
	}
}
