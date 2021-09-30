using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class GroupUpdateStrategyPreOsier : GroupUpdateStrategy
	{
		private readonly IRestService _restService;

		public GroupUpdateStrategyPreOsier(IRestService restService, IGetByNameStrategy<Group> getByNameStrategy)
			: base(getByNameStrategy)
		{
			_restService = restService;
		}

		protected override Group DoUpdate(Group group)
		{
			var dto = new GroupDTO(group);

			GroupResponse updatedGroup = _restService.Put<GroupResponse>($"relativity.groups/workspace/-1/groups/{group.ArtifactID}", dto);

			return updatedGroup.DoMappingFromResponse();
		}
	}
}
