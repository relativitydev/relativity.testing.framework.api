using System;
using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class InstanceSettingGetByNameAndSectionStrategy : IInstanceSettingGetByNameAndSectionStrategy
	{
		private readonly IObjectService _objectService;

		public InstanceSettingGetByNameAndSectionStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public InstanceSetting Get(string name, string section)
		{
			if (name is null)
			{
				throw new ArgumentNullException(nameof(name));
			}

			if (section is null)
			{
				throw new ArgumentNullException(nameof(section));
			}

			return _objectService.Query<InstanceSetting>()
				.Where(x => x.Name, name)
				.Where(x => x.Section, section)
				.FirstOrDefault();
		}
	}
}
