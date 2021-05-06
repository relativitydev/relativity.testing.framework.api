using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ResourceServerGetAllStrategy : IGetAllStrategy<ResourceServer>
	{
		private readonly IObjectService _objectService;

		public ResourceServerGetAllStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public ResourceServer[] GetAll()
		{
			return _objectService.Query<ResourceServer>().ToArray();
		}
	}
}
