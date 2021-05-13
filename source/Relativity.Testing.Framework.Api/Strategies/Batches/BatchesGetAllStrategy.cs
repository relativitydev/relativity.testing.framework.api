using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class BatchesGetAllStrategy : IGetAllWorkspaceEntitiesStrategy<Batch>
	{
		private readonly IObjectService _objectService;

		public BatchesGetAllStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public Batch[] GetAll(int workspaceId)
		{
			return _objectService.Query<Batch>().
				For(workspaceId).
				ToArray();
		}
	}
}
