using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class LibraryApplicationGetAllStrategy : IGetAllWorkspaceEntitiesStrategy<RelativityApplication>
	{
		private readonly IObjectService _objectService;

		public LibraryApplicationGetAllStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public RelativityApplication[] GetAll(int workspaceId)
		{
			return _objectService.Query<RelativityApplication>().
				For(workspaceId).
				ToArray();
		}
	}
}
