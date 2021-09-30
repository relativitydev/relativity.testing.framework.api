using System.Collections.Generic;
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

		private readonly IUserGetGroupsStrategy _userGetGroupsStrategy;

		public UserService(
			ICreateStrategy<User> createStrategy,
			IRequireWithEnsureNewStrategy<User> requireWithEnsureNewStrategy,
			IDeleteByIdStrategy<User> deleteByIdStrategy,
			IGetByIdStrategy<User> getByIdStrategy,
			IUserGetByEmailStrategy getByEmailStrategy,
			IUserExistsByEmailStrategy existsByEmailStrategy,
			IUserAddToGroupStrategy addToGroupStrategy,
			IUserRemoveFromGroupStrategy removeFromGroupStrategy,
			IUpdateStrategy<User> updateStrategy,
			IUserGetGroupsStrategy userGetGroupsStrategy)
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
			_userGetGroupsStrategy = userGetGroupsStrategy;
		}

		public User Create(User user)
			=> _createStrategy.Create(user);

		public User Require(User user, bool ensureNew = true)
			=> _requireWithEnsureNewStrategy.Require(user, ensureNew);

		public void Delete(int artifactID)
			=> _deleteByIdStrategy.Delete(artifactID);

		public User Get(int artifactID)
			=> _getByIdStrategy.Get(artifactID);

		public User GetByEmail(string email)
			=> _getByEmailStrategy.Get(email);

		public bool ExistsByEmail(string email)
			=> _existsByEmailStrategy.Exists(email);

		public void AddToGroup(int userArtifactID, int groupArtifactID)
			=> _addToGroupStrategy.AddToGroup(userArtifactID, groupArtifactID);

		public void RemoveFromGroup(int userArtifactID, int groupArtifactID)
			=> _removeFromGroupStrategy.RemoveFromGroup(userArtifactID, groupArtifactID);

		public void Update(User user)
			=> _updateStrategy.Update(user);

		public IList<NamedArtifact> GetGroups(int userArtifactID)
			=> _userGetGroupsStrategy.GetGroups(userArtifactID);
	}
}
