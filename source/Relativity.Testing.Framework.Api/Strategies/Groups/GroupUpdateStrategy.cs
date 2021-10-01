using System;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal abstract class GroupUpdateStrategy : IGroupUpdateStrategy
	{
		private readonly IGetByNameStrategy<Group> _getByNameStrategy;

		protected GroupUpdateStrategy(IGetByNameStrategy<Group> getByNameStrategy)
		{
			_getByNameStrategy = getByNameStrategy;
		}

		public Group Update(Group group)
		{
			if (group is null)
			{
				throw new ArgumentNullException(nameof(group));
			}

			if (group.ArtifactID < 1)
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

			return DoUpdate(group);
		}

		protected abstract Group DoUpdate(Group group);

		private int GetGroupArtifactIDByGroupName(Group group)
		{
			Group entityByName = _getByNameStrategy.Get(group.Name);

			if (entityByName == null)
			{
				throw ObjectNotFoundException.CreateForNotFoundByName<Group>(group.Name);
			}

			return entityByName.ArtifactID;
		}
	}
}
