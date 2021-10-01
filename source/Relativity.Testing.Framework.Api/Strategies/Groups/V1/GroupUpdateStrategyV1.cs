using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class GroupUpdateStrategyV1 : GroupUpdateStrategy
	{
		private readonly IRestService _restService;

		public GroupUpdateStrategyV1(IRestService restService, IGetByNameStrategy<Group> getByNameStrategy)
			: base(getByNameStrategy)
		{
			_restService = restService;
		}

		protected override Group DoUpdate(Group group)
		{
			var dto = new GroupDTO(group);

			GroupResponse updatedGroup = _restService.Put<GroupResponse>($"Relativity-Identity/v1/groups/{group.ArtifactID}", dto);

			return updatedGroup.MapToGroup();
		}
	}
}
