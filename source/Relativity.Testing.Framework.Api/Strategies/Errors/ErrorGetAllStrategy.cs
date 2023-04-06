using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ErrorGetAllStrategy : IGetAllStrategy<Error>
	{
		private readonly IObjectService _objectService;

		public ErrorGetAllStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public Error[] GetAll()
		{
			return _objectService.GetAll<Error>();
		}
	}
}
