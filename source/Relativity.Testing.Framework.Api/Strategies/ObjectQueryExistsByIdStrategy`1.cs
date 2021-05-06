using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ObjectQueryExistsByIdStrategy<T> : IExistsByIdStrategy<T>
		where T : Artifact
	{
		private readonly IObjectService _objectService;

		public ObjectQueryExistsByIdStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public bool Exists(int id)
		{
			return _objectService.Query<T>().
				FetchOnlyArtifactID().
				Where(x => x.ArtifactID, id).
				Any();
		}
	}
}
