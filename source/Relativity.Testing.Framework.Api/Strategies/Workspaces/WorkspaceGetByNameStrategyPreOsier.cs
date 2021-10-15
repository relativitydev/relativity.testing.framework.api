using System;
using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class WorkspaceGetByNameStrategyPreOsier : IGetByNameStrategy<Workspace>
	{
		private readonly IObjectService _objectService;

		public WorkspaceGetByNameStrategyPreOsier(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public Workspace Get(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException(nameof(name));
			}

			return _objectService.Query<Workspace>().Where(x => x.Name, name).FirstOrDefault();
		}
	}
}
