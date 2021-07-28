using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies.Layouts;
using Relativity.Testing.Framework.Api.Strategies.Layouts.DTO;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class LayoutGetByIdStrategyV1 : LayoutGetByIdAbstractStrategy
	{
		private readonly IRestService _restService;

		public LayoutGetByIdStrategyV1(IRestService restService, IObjectService objectService)
			: base(objectService)
		{
			_restService = restService;
		}

		protected override Layout DoGet(int workspaceId, int entityId)
		{
			var result = _restService.Get<LayoutDTOV1>($"/Relativity.Rest/API/relativity-data-visualization/V1/workspaces/{workspaceId}/layouts/{entityId}");
			return LayoutDTOMapper.DoMappingFromDTO(result);
		}
	}
}
