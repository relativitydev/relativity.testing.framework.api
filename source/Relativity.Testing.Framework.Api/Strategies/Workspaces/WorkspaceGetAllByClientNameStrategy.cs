using System;
using System.Collections.Generic;
using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class WorkspaceGetAllByClientNameStrategy : IGetAllByClientNameStrategy<Workspace>
	{
		private readonly IObjectService _objectService;

		public WorkspaceGetAllByClientNameStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public IEnumerable<Workspace> GetAllByClientName(string clientName)
		{
			if (clientName == null)
			{
				throw new ArgumentNullException(nameof(clientName));
			}

			return _objectService.Query<Workspace>().Where(x => x.Client, clientName).ToArray();
		}
	}
}
