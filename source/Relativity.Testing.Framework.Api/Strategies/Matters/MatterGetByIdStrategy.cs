using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class MatterGetByIdStrategy : ObjectQueryGetByIdStrategy<Matter>
	{
		public MatterGetByIdStrategy(IObjectService objectService)
			: base(objectService)
		{
		}
	}
}
