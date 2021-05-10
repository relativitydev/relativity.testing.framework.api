using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class MatterGetAllStrategy : IGetAllStrategy<Matter>
	{
		private readonly IObjectService _objectService;

		public MatterGetAllStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public Matter[] GetAll()
		{
			return _objectService.Query<Matter>().
				ToArray();
		}
	}
}
