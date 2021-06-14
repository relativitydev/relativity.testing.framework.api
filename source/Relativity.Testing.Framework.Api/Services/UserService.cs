using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class UserService : IUserService
	{
		private readonly ICreateStrategy<User> _createStrategy;

		private readonly IRequireWithEnsureNewStrategy<User> _requireWithEnsureNewStrategy;

		private readonly IDeleteByIdStrategy<User> _deleteByIdStrategy;

		private readonly IGetByIdStrategy<User> _getByIdStrategy;

		private readonly IUserGetByEmailStrategy _getByEmailStrategy;

		private readonly IUserExistsByEmailStrategy _existsByEmailStrategy;

		private readonly IUserAddToGroupStrategy _addToGroupStrategy;

		private readonly IUserRemoveFromGroupStrategy _removeFromGroupStrategy;

		private readonly IUpdateStrategy<User> _updateStrategy;

		public UserService(
			ICreateStrategy<User> createStrategy,
			IRequireWithEnsureNewStrategy<User> requireWithEnsureNewStrategy,
			IDeleteByIdStrategy<User> deleteByIdStrategy,
			IGetByIdStrategy<User> getByIdStrategy,
			IUserGetByEmailStrategy getByEmailStrategy,
			IUserExistsByEmailStrategy existsByEmailStrategy,
			IUserAddToGroupStrategy addToGroupStrategy,
			IUserRemoveFromGroupStrategy removeFromGroupStrategy,
			IUpdateStrategy<User> updateStrategy)
		{
			_createStrategy = createStrategy;
			_requireWithEnsureNewStrategy = requireWithEnsureNewStrategy;
			_deleteByIdStrategy = deleteByIdStrategy;
			_getByIdStrategy = getByIdStrategy;
			_getByEmailStrategy = getByEmailStrategy;
			_existsByEmailStrategy = existsByEmailStrategy;
			_addToGroupStrategy = addToGroupStrategy;
			_removeFromGroupStrategy = removeFromGroupStrategy;
			_updateStrategy = updateStrategy;
		}

		public User Create(User entity)
			=> _createStrategy.Create(entity);

		public User Require(User entity, bool ensureNew = true)
			=> _requireWithEnsureNewStrategy.Require(entity, ensureNew);

		public void Delete(int id)
			=> _deleteByIdStrategy.Delete(id);

		public User Get(int id)
			=> _getByIdStrategy.Get(id);

		public User GetByEmail(string email)
			=> _getByEmailStrategy.Get(email);

		public bool ExistsByEmail(string email)
			=> _existsByEmailStrategy.Exists(email);

		public void AddToGroup(int userId, int groupId)
			=> _addToGroupStrategy.AddToGroup(userId, groupId);

		public void RemoveFromGroup(int userId, int groupId)
			=> _removeFromGroupStrategy.RemoveFromGroup(userId, groupId);

		public void Update(User entity)
			=> _updateStrategy.Update(entity);
	}
}
