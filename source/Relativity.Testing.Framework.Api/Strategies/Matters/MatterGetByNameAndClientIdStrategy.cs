using System;
using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class MatterGetByNameAndClientIdStrategy : IMatterGetByNameAndClientIdStrategy
	{
		private readonly IObjectService _objectService;

		public MatterGetByNameAndClientIdStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public Matter Get(string name, int clientId)
		{
			if (name == null)
			{
				throw new ArgumentNullException(nameof(name));
			}

			return _objectService.Query<Matter>().
				Where(x => x.Name, name).
				Where(x => x.Client, clientId).
				FirstOrDefault();
		}
	}
}
