using System;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class UserRequireStrategy : IRequireWithEnsureNewStrategy<User>
	{
		private readonly ICreateStrategy<User> _createStrategy;
		private readonly IUpdateStrategy<User> _updateStrategy;
		private readonly IUserGetByEmailStrategy _userGetByEmailStrategy;
		private readonly IDeleteByIdStrategy<User> _deleteByIdStrategy;
		private readonly IWaitUserDeletedStrategy _waitUserDeletedStrategy;

		public UserRequireStrategy(
			ICreateStrategy<User> createStrategy,
			IUpdateStrategy<User> updateStrategy,
			IUserGetByEmailStrategy userGetByEmailStrategy,
			IDeleteByIdStrategy<User> deleteByIdStrategy,
			IWaitUserDeletedStrategy waitUserDeletedStrategy)
		{
			_createStrategy = createStrategy;
			_updateStrategy = updateStrategy;
			_userGetByEmailStrategy = userGetByEmailStrategy;
			_deleteByIdStrategy = deleteByIdStrategy;
			_waitUserDeletedStrategy = waitUserDeletedStrategy;
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
					return _updateStrategy.Update(entity);
				}

				if (entity.EmailAddress != null)
				{
					var existedEntity = _userGetByEmailStrategy.Get(entity.EmailAddress);
					if (existedEntity != null)
					{
						entity.ArtifactID = existedEntity.ArtifactID;
						return _updateStrategy.Update(entity);
					}
				}
			}
			else
			{
				if (entity.ArtifactID != 0)
				{
					_deleteByIdStrategy.Delete(entity.ArtifactID);
					_waitUserDeletedStrategy.Wait(entity.ArtifactID);
				}
				else if (entity.EmailAddress != null)
				{
					var existedEntity = _userGetByEmailStrategy.Get(entity.EmailAddress);
					if (existedEntity != null)
					{
						_deleteByIdStrategy.Delete(existedEntity.ArtifactID);
						_waitUserDeletedStrategy.Wait(entity.ArtifactID);
					}
				}
			}

			return _createStrategy.Create(entity);
		}
	}
}
