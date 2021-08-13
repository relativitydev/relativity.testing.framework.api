using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class MatterDeleteByIdStrategyPreOsier : DeleteByIdStrategy<Matter>
	{
		private readonly IRestService _restService;

		public MatterDeleteByIdStrategyPreOsier(IRestService restService)
		{
			_restService = restService;
		}

		protected override void DoDelete(int id)
		{
			object dto = new
			{
				matterArtifactID = id
			};

			_restService.Post(
				"Relativity.Services.Matter.IMatterModule/Matter%20Manager/DeleteSingleAsync",
				dto);
		}
	}
}
