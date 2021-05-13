using System;
using System.Collections.Generic;
using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class GroupGetAllByNamesStrategy : IGetAllByNamesStrategy<Group>
	{
		private readonly IObjectService _objectService;

		public GroupGetAllByNamesStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public IEnumerable<Group> GetAll(IEnumerable<string> names)
		{
			if (names == null)
			{
				throw new ArgumentNullException(nameof(names));
			}

			if (!names.Any())
			{
				throw new ArgumentException($"{nameof(names)} contains no elements.", nameof(names));
			}

			return _objectService.Query<Group>().
				Where(x => x.Name, names).ToArray();
		}
	}
}
