using System.Linq;
using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal abstract class LayoutGetByIdAbstractStrategy : IGetWorkspaceEntityByIdStrategy<Layout>
	{
		private readonly IObjectService _objectService;

		protected LayoutGetByIdAbstractStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public virtual Layout Get(int workspaceId, int entityId)
		{
			var layoutExists = _objectService.Query<Layout>()
				.For(workspaceId)
				.FetchOnlyArtifactID()
				.Where(x => x.ArtifactID, entityId)
				.Any();

			if (!layoutExists)
			{
				return null;
			}

			return DoGet(workspaceId, entityId);
		}

		protected abstract Layout DoGet(int workspaceId, int entityId);
	}
}
