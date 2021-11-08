using System;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class GroupRequireStrategy : IRequireStrategy<Group>
	{
		private readonly ICreateStrategy<Group> _createStrategy;
		private readonly IGroupUpdateStrategy _updateStrategy;
		private readonly IGetByNameStrategy<Group> _getByNameStrategy;

		public GroupRequireStrategy(
			ICreateStrategy<Group> createStrategy,
			IGroupUpdateStrategy updateStrategy,
			IGetByNameStrategy<Group> getByNameStrategy)
		{
			_createStrategy = createStrategy;
			_updateStrategy = updateStrategy;
			_getByNameStrategy = getByNameStrategy;
		}

		public Group Require(Group entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}
			else if (entity.ArtifactID != 0)
			{
				return _updateStrategy.Update(entity);
			}
			else if (entity.Name != null)
			{
				Group existedEntity = _getByNameStrategy.Get(entity.Name);

				if (existedEntity != null)
				{
					entity.ArtifactID = existedEntity.ArtifactID;

					return _updateStrategy.Update(entity);
				}
			}

			return _createStrategy.Create(entity);
		}
	}
}
