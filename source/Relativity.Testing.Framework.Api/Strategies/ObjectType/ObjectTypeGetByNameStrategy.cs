using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ObjectTypeGetByNameStrategy : IGetWorkspaceEntityByNameStrategy<Relativity.Testing.Framework.Models.ObjectType>
	{
		private readonly IObjectService _objectService;
		private readonly IRestService _restService;

		public ObjectTypeGetByNameStrategy(IObjectService objectService, IRestService restService)
		{
			_objectService = objectService;
			_restService = restService;
		}

		public Relativity.Testing.Framework.Models.ObjectType Get(int workspaceId, string entityName)
		{
			var objectType = _objectService.Query<Relativity.Testing.Framework.Api.Strategies.ObjectTypeGetByNameStrategy.ObjectType>().
				For(workspaceId).
				Select(x => new { x.Name, x.ArtifactID }).
				FirstOrDefault(x => x.Name == entityName);

			if (objectType == null)
			{
				return null;
			}

			return _restService.Get<Relativity.Testing.Framework.Models.ObjectType>($"relativity.objectTypes/workspace/{workspaceId}/objectTypes/{objectType.ArtifactID}");
		}

#pragma warning disable S3459 // Unassigned members should be removed
		private class ObjectType
		{
			/// <summary>
			/// Gets or sets Name.
			/// </summary>
			public string Name { get; set; }

			/// <summary>
			/// Gets or sets ArtifactTypeID.
			/// </summary>
			public int ArtifactID { get; set; }
		}
#pragma warning restore S3459 // Unassigned members should be removed
	}
}
