using Relativity.Testing.Framework.Api.DTO;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies.Layouts.DTO;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class ViewCreateStrategyV1 : CreateWorkspaceEntityStrategy<View>
	{
		private readonly IRestService _restService;
		private readonly IGetWorkspaceEntityByIdStrategy<View> _getWorkspaceEntityByIdStrategy;

		public ViewCreateStrategyV1(
			IRestService restService,
			IGetWorkspaceEntityByIdStrategy<View> getWorkspaceEntityByIdStrategy)
		{
			_restService = restService;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
		}

		protected override View DoCreate(int workspaceId, View entity)
		{
			entity.FillRequiredProperties();

			var dto = new
			{
				viewRequest = ViewDTOMapper.ConvertToDTO(entity)
			};

			var artifactId = _restService.Post<int>($"/Relativity.Rest/API/relativity-data-visualization/V1/workspaces/{workspaceId}/views", dto);

			return _getWorkspaceEntityByIdStrategy.Get(workspaceId, artifactId);
		}
	}
}
