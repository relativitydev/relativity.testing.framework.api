using System;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class MatterRequireStrategy : IRequireStrategy<Matter>
	{
		private readonly ICreateStrategy<Matter> _createStrategy;
		private readonly IMatterUpdateStrategy _updateStrategy;
		private readonly IMatterGetByIdStrategy _getByIdStrategy;
		private readonly IMatterGetByNameAndClientIdStrategy _matterGetByNameAndClientIdStrategy;

		public MatterRequireStrategy(
			ICreateStrategy<Matter> createStrategy,
			IMatterUpdateStrategy updateStrategy,
			IMatterGetByIdStrategy getByIdStrategy,
			IMatterGetByNameAndClientIdStrategy matterGetByNameAndClientIdStrategy)
		{
			_createStrategy = createStrategy;
			_updateStrategy = updateStrategy;
			_getByIdStrategy = getByIdStrategy;
			_matterGetByNameAndClientIdStrategy = matterGetByNameAndClientIdStrategy;
		}

		public Matter Require(Matter entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			if (entity.ArtifactID != 0)
			{
				_updateStrategy.Update(entity);
				return _getByIdStrategy.Get(entity.ArtifactID);
			}

			if (entity.Name != null)
			{
				var existedEntity = _matterGetByNameAndClientIdStrategy.Get(entity.Name, entity.Client.ArtifactID);
				if (existedEntity != null)
				{
					entity.ArtifactID = existedEntity.ArtifactID;
					_updateStrategy.Update(entity);
					return _getByIdStrategy.Get(entity.ArtifactID);
				}
			}

			return _createStrategy.Create(entity);
		}
	}
}
