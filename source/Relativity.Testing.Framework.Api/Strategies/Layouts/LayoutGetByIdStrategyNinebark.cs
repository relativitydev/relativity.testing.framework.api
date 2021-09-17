using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.0 <12.1")]
	internal class LayoutGetByIdStrategyNinebark : LayoutGetByIdAbstractStrategy
	{
		private readonly IRestService _restService;

		public LayoutGetByIdStrategyNinebark(IRestService restService, IObjectService objectService)
			: base(objectService)
		{
			_restService = restService;
		}

		protected override Layout DoGet(int workspaceId, int entityId)
		{
			var result = _restService.Get<JObject>($"Relativity.Rest/API/Relativity.Layouts/workspace/{workspaceId}/layouts/{entityId}");

			result["ObjectType"] = result["ObjectType"]["Value"];
			result["RelativityApplications"] = result["RelativityApplications"]["ViewableItems"];
			return result.ToObject<Layout>();
		}
	}
}
