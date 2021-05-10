using System;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class UserRequireStrategy : IRequireWithEnsureNewStrategy<User>
	{
		private readonly ICreateStrategy<User> _createStrategy;
		private readonly IUpdateStrategy<User> _updateStrategy;
		private readonly IGetByIdStrategy<User> _getByIdStrategy;
		private readonly IUserGetByEmailStrategy _userGetByEmailStrategy;
		private readonly IDeleteByIdStrategy<User> _deleteByIdStrategy;

		public UserRequireStrategy(
			ICreateStrategy<User> createStrategy,
			IUpdateStrategy<User> updateStrategy,
			IGetByIdStrategy<User> getByIdStrategy,
			IUserGetByEmailStrategy userGetByEmailStrategy,
			IDeleteByIdStrategy<User> deleteByIdStrategy)
		{
			_createStrategy = createStrategy;
			_updateStrategy = updateStrategy;
			_getByIdStrategy = getByIdStrategy;
			_userGetByEmailStrategy = userGetByEmailStrategy;
			_deleteByIdStrategy = deleteByIdStrategy;
		}

		public User Require(User entity, bool ensureNew = true)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			if (!ensureNew)
			{
				if (entity.ArtifactID != 0)
				{
					_updateStrategy.Update(entity);
					return _getByIdStrategy.Get(entity.ArtifactID);
				}

				if (entity.EmailAddress != null)
				{
					var existedEntity = _userGetByEmailStrategy.Get(entity.EmailAddress);
					if (existedEntity != null)
					{
						entity.ArtifactID = existedEntity.ArtifactID;
						_updateStrategy.Update(entity);
						return _getByIdStrategy.Get(entity.ArtifactID);
					}
				}
			}
			else
			{
				if (entity.ArtifactID != 0)
				{
					_deleteByIdStrategy.Delete(entity.ArtifactID);
				}
				else if (entity.EmailAddress != null)
				{
					var existedEntity = _userGetByEmailStrategy.Get(entity.EmailAddress);
					if (existedEntity != null)
					{
						_deleteByIdStrategy.Delete(existedEntity.ArtifactID);
					}
				}
			}

			return _createStrategy.Create(entity);
		}
	}
}
