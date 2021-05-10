using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class WorkspaceGetByIdStrategy : IGetByIdStrategy<Workspace>
	{
		private readonly IObjectService _objectService;

		public WorkspaceGetByIdStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public Workspace Get(int id)
		{
			return _objectService.Query<Workspace>().Where("Case Artifact ID", id).FirstOrDefault();
		}
	}
}
