using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class WorkspaceGetByIdStrategyPreOsier : IGetByIdStrategy<Workspace>
	{
		private readonly IObjectService _objectService;

		public WorkspaceGetByIdStrategyPreOsier(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public Workspace Get(int id)
		{
			return _objectService.Query<Workspace>().Where("Case Artifact ID", id).FirstOrDefault();
		}
	}
}
