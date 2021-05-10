using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class GroupGetByIdStrategy : ObjectQueryGetByIdStrategy<Group>
	{
		public GroupGetByIdStrategy(IObjectService objectService)
			: base(objectService)
		{
		}
	}
}
