using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ErrorCreateStrategy : CreateStrategy<Error>
	{
		private readonly IRestService _restService;
		private readonly IGetByIdStrategy<Error> _getByIdStrategy;

		public ErrorCreateStrategy(IRestService restService, IGetByIdStrategy<Error> getByIdStrategy)
		{
			_restService = restService;
			_getByIdStrategy = getByIdStrategy;
		}

		protected override Error DoCreate(Error entity)
		{
			var dto = new
			{
				errorDTO = entity
			};

			var artifactID = _restService.Post<int>("Relativity.Services.Error.IErrorModule/Error Manager/CreateSingleAsync", dto);

			return _getByIdStrategy.Get(artifactID);
		}
	}
}
