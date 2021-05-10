using System;
using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Mapping;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ProductionsGetStatusStrategy : IGetProductionStatusStrategy
	{
		private readonly IRestService _restService;

		public ProductionsGetStatusStrategy(IRestService restService)
		{
			_restService = restService;
		}

		public ProductionStatus GetStatus(int workspaceId, int entityId)
		{
			var dto = new
			{
				workspaceArtifactID = workspaceId,
				productionArtifactID = entityId
			};

			var response = _restService.Post<JObject>("Relativity.Productions.Services.IProductionModule/Production Manager/GetProductionStatusDetails", dto);

			var status = response["StatusDetails"]["Status"];

			var getEnumStatus = ChoiceNameToEnumMapper.GetEnumValue(typeof(ProductionStatus), status.ToString());

			return (ProductionStatus)Enum.Parse(typeof(ProductionStatus), getEnumStatus.ToString());
		}
	}
}
