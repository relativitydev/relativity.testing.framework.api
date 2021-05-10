using System;
using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies.Groups
{
	internal class GroupGetByNameStrategy : IGetByNameStrategy<Group>
	{
		private readonly IObjectService _objectService;

		public GroupGetByNameStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public Group Get(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException(nameof(name));
			}

			return _objectService.Query<Group>().Where(x => x.Name, name).FirstOrDefault();
		}
	}
}
