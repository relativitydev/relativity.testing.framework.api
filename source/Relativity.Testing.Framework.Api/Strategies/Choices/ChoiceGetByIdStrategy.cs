using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ChoiceGetByIdStrategy : IGetWorkspaceEntityByIdStrategy<Choice>
	{
		private readonly IObjectService _objectService;

		public ChoiceGetByIdStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public Choice Get(int workspaceId, int entityId)
		{
			return _objectService.Query<Choice>().
				For(workspaceId).
				Where(x => x.ArtifactID, entityId).
				FirstOrDefault();
		}
	}
}
