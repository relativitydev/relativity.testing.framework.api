using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	public class ErrorGetByIdStrategy : IGetByIdStrategy<Error>
	{
		private readonly IObjectService _objectService;
		private readonly IRestService _restService;

		public ErrorGetByIdStrategy(IObjectService objectService, IRestService restService)
		{
			_objectService = objectService;
			_restService = restService;
		}

		public Error Get(int id)
		{
			bool isExist = _objectService.Query<Error>().
				FetchOnlyArtifactID().
				Where(x => x.ArtifactID, id).
				Any();

			if (!isExist)
			{
				return null;
			}

			return _restService.Get<Error>($"Relativity.Services.Error.IErrorModule/workspace/-1/errors/{id}");
		}
	}
}
