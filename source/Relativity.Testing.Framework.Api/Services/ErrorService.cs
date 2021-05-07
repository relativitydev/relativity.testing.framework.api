using System;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class ErrorService : IErrorService
	{
		private readonly ICreateStrategy<Error> _createStrategy;
		private readonly IGetByIdStrategy<Error> _getByIdStrategy;
		private readonly IGetAllStrategy<Error> _getAllStrategy;
		private readonly IGetAllByDateStrategy<Error> _getAllByDateStrategy;

		public ErrorService(
			ICreateStrategy<Error> createStrategy,
			IGetByIdStrategy<Error> getByIdStrategy,
			IGetAllStrategy<Error> getAllStrategy,
			IGetAllByDateStrategy<Error> getAllByDateStrategy)
		{
			_createStrategy = createStrategy;
			_getByIdStrategy = getByIdStrategy;
			_getAllStrategy = getAllStrategy;
			_getAllByDateStrategy = getAllByDateStrategy;
		}

		public Error Create(Error entity)
			=> _createStrategy.Create(entity);

		public Error Get(int entityId)
			=> _getByIdStrategy.Get(entityId);

		public Error[] GetAll()
			=> _getAllStrategy.GetAll();

		public Error[] GetAllByDate(DateTime from, DateTime to)
			=> _getAllByDateStrategy.GetAll(from, to);
	}
}
