using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class ViewGetByIdStrategyV1 : IGetWorkspaceEntityByIdStrategy<View>
	{
		private readonly IRestService _restService;
		private readonly IExistsWorkspaceEntityByIdStrategy<View> _existsWorkspaceEntityByIdStrategy;

		public ViewGetByIdStrategyV1(
			IRestService restService,
			IExistsWorkspaceEntityByIdStrategy<View> existsWorkspaceEntityByIdStrategy)
		{
			_restService = restService;
			_existsWorkspaceEntityByIdStrategy = existsWorkspaceEntityByIdStrategy;
		}

		public View Get(int workspaceId, int entityId)
		{
			if (!_existsWorkspaceEntityByIdStrategy.Exists(workspaceId, entityId))
			{
				return null;
			}

			var result = _restService.Get<JObject>($"Relativity.Rest/API/relativity-data-visualization/V1/workspaces/{workspaceId}/views/{entityId}");
			return ConvertResponse(result).ToObject<View>();
		}

		private JObject ConvertResponse(JObject response)
		{
			response["ObjectType"] = response["ObjectType"]["Value"];
			response["Name"] = response["ObjectIdentifier"]["Name"];
			response["ArtifactID"] = response["ObjectIdentifier"]["ArtifactID"];
			response["RelativityApplications"] = response["RelativityApplications"]["ViewableItems"];
			response["Fields"] = response["Fields"]["ViewableItems"];
			return response;
		}
	}
}
