using System.Linq;
using Relativity.Testing.Framework.Api.Models;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class BatchSetExistsByIdStrategy : IExistsBatchSetByIdStrategy
	{
		private readonly IObjectService _objectService;

		public BatchSetExistsByIdStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public bool Exists(int workspaceId, int entityId, UserCredentials userCredentials = null)
		{
			return _objectService.Query<BatchSet>()
				.For(workspaceId)
				.With(userCredentials)
				.FetchOnlyArtifactID()
				.Where(x => x.ArtifactID, entityId)
				.Any();
		}
	}
}
