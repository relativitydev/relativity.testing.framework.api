using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ProductionsGetAllStrategy : IGetAllWorkspaceEntitiesStrategy<Production>
	{
		private readonly IObjectService _objectService;

		public ProductionsGetAllStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public Production[] GetAll(int workspaceId)
		{
			return _objectService.Query<Production>().
				For(workspaceId).
				ToArray();
		}
	}
}
