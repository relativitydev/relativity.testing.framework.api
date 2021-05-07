using System;
using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class FieldGetByNameStrategy<T> : IGetWorkspaceEntityByNameStrategy<T>
		where T : Field
	{
		private readonly IObjectService _objectService;
		private readonly IRestService _restService;

		public FieldGetByNameStrategy(
			IObjectService objectService,
			IRestService restService)
		{
			_objectService = objectService;
			_restService = restService;
		}

		public T Get(int workspaceId, string entityName)
		{
			if (entityName is null)
			{
				throw new ArgumentNullException(nameof(entityName));
			}

			var artifact = _objectService.Query<T>().
				FetchOnlyArtifactID().
				For(workspaceId).
				Where(x => x.Name, entityName).
				FirstOrDefault();

			if (artifact == null)
			{
				return null;
			}

			return _restService.Get<T>($"Relativity.Fields/workspace/{workspaceId}/fields/{artifact.ArtifactID}");
		}
	}
}
