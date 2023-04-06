using System;
using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ErrorGetAllByDateStrategy : IGetAllByDateStrategy<Error>
	{
		private readonly IObjectService _objectService;

		public ErrorGetAllByDateStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public Error[] GetAll(DateTime from, DateTime to)
		{
			var condition = $"(('Timestamp' >= {from.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm")}:00.00Z AND 'Timestamp' <= {to.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm")}:00.00Z))";

			return _objectService.Query<Error>()
				.Where(condition).ToArray();
		}
	}
}
