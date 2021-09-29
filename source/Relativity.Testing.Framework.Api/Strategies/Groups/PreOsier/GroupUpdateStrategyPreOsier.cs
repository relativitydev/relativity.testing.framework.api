using System;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class GroupUpdateStrategyPreOsier : IGroupUpdateStrategy
	{
		private readonly IRestService _restService;
		private readonly IGetByNameStrategy<Group> _getByNameStrategy;

		public GroupUpdateStrategyPreOsier(IRestService restService, IGetByNameStrategy<Group> getByNameStrategy)
		{
			_restService = restService;
			_getByNameStrategy = getByNameStrategy;
		}

		public Group Update(Group group)
		{
			if (group is null)
			{
				throw new ArgumentNullException(nameof(group));
			}

			if (group.ArtifactID == 0)
			{
				if (group.Name != null)
				{
					group.ArtifactID = GetGroupArtifactIDByGroupName(group);
				}
				else
				{
					throw new ArgumentException("This entity should have an artifact ID or name as an identifier.", nameof(group));
				}
			}

			var dto = new GroupDTO(group);

			GroupResponse updatedGroup = _restService.Put<GroupResponse>($"relativity.groups/workspace/-1/groups/{group.ArtifactID}", dto);

			return updatedGroup.DoMappingFromResponse();
		}

		private int GetGroupArtifactIDByGroupName(Group group)
		{
			Group entityByName = _getByNameStrategy.Get(group.Name);

			if (entityByName == null)
			{
				throw new ArgumentException("Can't find the entity.", nameof(group));
			}

			return group.ArtifactID;
		}
	}
}
