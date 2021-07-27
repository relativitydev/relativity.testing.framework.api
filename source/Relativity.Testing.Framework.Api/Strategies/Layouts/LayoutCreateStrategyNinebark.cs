using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies.Layouts;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.0 <12.1")]
	internal class LayoutCreateStrategyNinebark : LayoutCreateAbstractStrategy
	{
		private readonly IRestService _restService;

		public LayoutCreateStrategyNinebark(
			IRestService restService,
			ICreateWorkspaceEntityStrategy<ObjectType> createWorkspaceEntityStrategy)
			: base(createWorkspaceEntityStrategy)
		{
			_restService = restService;
		}

		protected override Layout SendRequest(int workspaceId, object dto, Layout entity)
		{
			var result = _restService.Post<JObject>($"Relativity.Layouts/workspace/{workspaceId}/layouts", dto);
			result["ObjectType"] = result["ObjectType"]["Value"];
			result["RelativityApplications"] = result["RelativityApplications"]["ViewableItems"];
			return result.ToObject<Layout>();
		}
	}
}
