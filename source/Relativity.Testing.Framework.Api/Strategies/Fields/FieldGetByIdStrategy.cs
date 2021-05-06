using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class FieldGetByIdStrategy<T> : IGetWorkspaceEntityByIdStrategy<T>
		where T : Field
	{
		private readonly IRestService _restService;
		private readonly IObjectService _objectService;

		public FieldGetByIdStrategy(
			IRestService restService,
			IObjectService objectService)
		{
			_restService = restService;
			_objectService = objectService;
		}

		public T Get(int workspaceId, int entityId)
		{
			var artifact = _objectService.Query<T>().
				FetchOnlyArtifactID().
				For(workspaceId).
				Where(x => x.ArtifactID, entityId).
				FirstOrDefault();

			if (artifact == null)
			{
				return null;
			}

			return _restService.Get<T>($"Relativity.Fields/workspace/{workspaceId}/fields/{entityId}");
		}
	}
}
