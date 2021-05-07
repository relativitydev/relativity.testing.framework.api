using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	public class OcrProfileDeleteStrategy : DeleteWorkspaceEntityByIdStrategy<OcrProfile>
	{
		private readonly IObjectService _objectService;

		public OcrProfileDeleteStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		protected override void DoDelete(int workspaceId, int entityId)
		{
			_objectService.Delete(workspaceId, entityId);
		}
	}
}
