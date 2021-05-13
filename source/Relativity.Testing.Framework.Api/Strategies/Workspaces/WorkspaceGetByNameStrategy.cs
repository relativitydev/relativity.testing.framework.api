using System;
using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class WorkspaceGetByNameStrategy : IGetByNameStrategy<Workspace>
	{
		private readonly IObjectService _objectService;

		public WorkspaceGetByNameStrategy(IObjectService objectService)
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
