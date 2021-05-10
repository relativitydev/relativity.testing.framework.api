using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class InstanceSettingGetByIdStrategy : ObjectQueryGetByIdStrategy<InstanceSetting>
	{
		public InstanceSettingGetByIdStrategy(IObjectService objectService)
			: base(objectService)
		{
		}
	}
}
