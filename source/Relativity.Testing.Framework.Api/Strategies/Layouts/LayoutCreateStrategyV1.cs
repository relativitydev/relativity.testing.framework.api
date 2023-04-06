using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies.Layouts;
using Relativity.Testing.Framework.Api.Strategies.Layouts.DTO;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class LayoutCreateStrategyV1 : LayoutCreateAbstractStrategy
	{
		private readonly IRestService _restService;

		public LayoutCreateStrategyV1(IRestService restService, ICreateWorkspaceEntityStrategy<ObjectType> createWorkspaceEntityStrategy)
			: base(createWorkspaceEntityStrategy)
		{
			_restService = restService;
		}

		protected override Layout SendRequest(int workspaceId, object dto, Layout entity)
		{
			var result = _restService.Post<LayoutDTOV1>($"/Relativity.Rest/API/relativity-data-visualization/v1/workspaces/{workspaceId}/layouts", dto);

			return LayoutDTOMapper.DoMappingFromDTO(result);
		}
	}
}
