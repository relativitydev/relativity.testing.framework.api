using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class WorkspaceExistsByIdStrategy : IExistsByIdStrategy<Workspace>
	{
		private readonly IObjectService _objectService;

		public WorkspaceExistsByIdStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public bool Exists(int id)
		{
			return _objectService.Query<Workspace>().FetchOnlyArtifactID().Where("Case Artifact ID", id).Any();
		}
	}
}
