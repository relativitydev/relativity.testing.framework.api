using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ErrorCreateStrategy : CreateStrategy<Error>
	{
		private readonly IRestService _restService;

		public ErrorCreateStrategy(IRestService restService)
		{
			_restService = restService;
		}

		protected override Error DoCreate(Error entity)
		{
			var dto = new
			{
				errorDTO = entity
			};

			entity.ArtifactID = _restService.Post<int>("Relativity.Services.Error.IErrorModule/Error Manager/CreateSingleAsync", dto);

			return entity;
		}
	}
}
