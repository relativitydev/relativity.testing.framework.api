using System;
using System.Collections.Generic;
using System.Linq;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class GroupRequireStrategy : IRequireStrategy<Group>
	{
		private readonly ICreateStrategy<Group> _createStrategy;
		private readonly IUpdateStrategy<Group> _updateStrategy;
		private readonly IGetByIdStrategy<Group> _getByIdStrategy;
		private readonly IGetAllByNamesStrategy<Group> _getAllByNamesStrategy;

		public GroupRequireStrategy(
			ICreateStrategy<Group> createStrategy,
			IUpdateStrategy<Group> updateStrategy,
			IGetByIdStrategy<Group> getByIdStrategy,
			IGetAllByNamesStrategy<Group> getAllByNamesStrategy)
		{
			_createStrategy = createStrategy;
			_updateStrategy = updateStrategy;
			_getByIdStrategy = getByIdStrategy;
			_getAllByNamesStrategy = getAllByNamesStrategy;
		}

		public Group Require(Group entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			if (entity.ArtifactID != 0)
			{
				return _updateStrategy.Update(entity);
			}

			if (entity.Name != null)
			{
				var existedEntity = _getAllByNamesStrategy.GetAll(new List<string> { entity.Name }).FirstOrDefault();
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
