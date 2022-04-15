using System;
using System.Linq;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal abstract class UserUpdateStrategy : IUpdateStrategy<User>
	{
		protected UserUpdateStrategy(
			IRestService restService,
			IChoiceResolveByObjectFieldAndNameStrategy choiceResolveByObjectFieldAndNameStrategy,
			IUserAddToGroupStrategy userAddToGroupStrategy,
			IUserSetPasswordStrategy userSetPasswordStrategy)
		{
			RestService = restService;
			ChoiceResolveByObjectFieldAndNameStrategy = choiceResolveByObjectFieldAndNameStrategy;
			UserAddToGroupStrategy = userAddToGroupStrategy;
			UserSetPasswordStrategy = userSetPasswordStrategy;
		}

		protected IRestService RestService { get; }

		protected IChoiceResolveByObjectFieldAndNameStrategy ChoiceResolveByObjectFieldAndNameStrategy { get; }

		protected IUserAddToGroupStrategy UserAddToGroupStrategy { get; }

		protected IUserSetPasswordStrategy UserSetPasswordStrategy { get; }

		protected abstract User UpdateUser(User entity);

		public void Update(User entity)
		{
			if (entity is null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			var createdUser = UpdateUser(entity);

			if (entity.Password != null)
			{
				UserSetPasswordStrategy.SetPassword(createdUser.ArtifactID, entity.Password);
			}

			entity.Groups.RemoveAll(x => createdUser.Groups.Any(y => y.ArtifactID == x.ArtifactID));

			if (entity.Groups.Any())
			{
				foreach (var group in entity.Groups)
				{
					UserAddToGroupStrategy.AddToGroup(createdUser.ArtifactID, group.ArtifactID);
				}
			}
		}
	}
}
