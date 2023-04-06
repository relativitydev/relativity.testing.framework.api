using System;
using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class WorkspaceGetByNameStrategyV1 : IGetByNameStrategy<Workspace>
	{
		private readonly IObjectService _objectService;
		private readonly IGetByIdStrategy<Workspace> _getWorkspaceByIdStrategy;

		public WorkspaceGetByNameStrategyV1(IObjectService objectService, IGetByIdStrategy<Workspace> getWorkspaceByIdStrategy)
		{
			_objectService = objectService;
			_getWorkspaceByIdStrategy = getWorkspaceByIdStrategy;
		}

		public Workspace Get(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException(nameof(name));
			}

			var artifact = _objectService.Query<Workspace>().FetchOnlyArtifactID().Where(x => x.Name, name).FirstOrDefault();

			if (artifact == null)
			{
				return null;
			}

			return _getWorkspaceByIdStrategy.Get(artifact.ArtifactID);
		}
	}
}
