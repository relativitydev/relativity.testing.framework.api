using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class WorkspaceGetAllStrategy : IGetAllStrategy<Workspace>
	{
		private readonly IObjectService _objectService;

		public WorkspaceGetAllStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public Workspace[] GetAll()
		{
			return _objectService.Query<Workspace>().ToArray();
		}
	}
}
