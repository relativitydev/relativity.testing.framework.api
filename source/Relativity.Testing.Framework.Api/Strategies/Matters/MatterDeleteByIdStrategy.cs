using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class MatterDeleteByIdStrategy : DeleteByIdStrategy<Matter>
	{
		private readonly IRestService _restService;

		public MatterDeleteByIdStrategy(IRestService restService)
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
