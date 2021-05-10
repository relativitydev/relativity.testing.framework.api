using System;
using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ProductionsGetByNameStrategy : IGetWorkspaceEntityByNameStrategy<Production>
	{
		private readonly IObjectService _objectService;

		private readonly IGetWorkspaceEntityByIdStrategy<Production> _getWorkspaceEntityByIdStrategy;

		public ProductionsGetByNameStrategy(
			IObjectService objectService,
			IGetWorkspaceEntityByIdStrategy<Production> getWorkspaceEntityByIdStrategy)
		{
			_objectService = objectService;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
		}

		public Production Get(int workspaceId, string entityName)
		{
			if (entityName == null)
			{
				throw new ArgumentNullException(nameof(entityName));
			}

			var artifact = _objectService.Query<Production>()
				.For(workspaceId)
				.FetchOnlyArtifactID()
				.Where(x => x.Name, entityName)
				.FirstOrDefault();

			if (artifact == null)
			{
				return null;
			}

			return _getWorkspaceEntityByIdStrategy.Get(workspaceId, artifact.ArtifactID);
		}
	}
}
