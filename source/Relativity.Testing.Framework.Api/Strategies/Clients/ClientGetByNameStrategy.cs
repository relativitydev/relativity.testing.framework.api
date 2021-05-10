using System;
using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ClientGetByNameStrategy : IGetByNameStrategy<Client>
	{
		private readonly IObjectService _objectService;

		public ClientGetByNameStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public Client Get(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException(nameof(name));
			}

			return _objectService.Query<Client>().Where(x => x.Name, name).FirstOrDefault();
		}
	}
}
