using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class BatchesGetByIdStrategy : IGetWorkspaceEntityByIdStrategy<Batch>
	{
		private readonly IObjectService _objectService;

		public BatchesGetByIdStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public Batch Get(int workspaceId, int entityId)
		{
			return _objectService.Query<Batch>().
				For(workspaceId).
				Where(x => x.ArtifactID, entityId).
				FirstOrDefault();
		}
	}
}
