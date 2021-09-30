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
			IUserAddToGroupStrategy userAddToGroupStrategy)
		{
			RestService = restService;
			ChoiceResolveByObjectFieldAndNameStrategy = choiceResolveByObjectFieldAndNameStrategy;
			UserAddToGroupStrategy = userAddToGroupStrategy;
		}

		protected IRestService RestService { get; }

		protected IChoiceResolveByObjectFieldAndNameStrategy ChoiceResolveByObjectFieldAndNameStrategy { get; }

		protected IUserAddToGroupStrategy UserAddToGroupStrategy { get; }

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
				AddPasswordProvider(createdUser.ArtifactID);

				SetPassword(createdUser.ArtifactID, entity.Password);
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

		private void AddPasswordProvider(int userArtifactId)
		{
			var passwordProviderProfile = new
			{
				Profile = new
				{
					userId = userArtifactId,
					Password = new
					{
						IsEnabled = true,
						MustResetPasswordOnNextLogin = false
					}
				}
			};

			RestService.Post("Relativity.Services.Security.ISecurityModule/Login Profile Manager/SaveLoginProfileAsync", passwordProviderProfile);
		}

		private void SetPassword(int userArtifactId, string passwordToSet)
		{
			var password = new
			{
				UserId = userArtifactId,
				Password = passwordToSet
			};

			RestService.Post("Relativity.Services.Security.ISecurityModule/Login Profile Manager/SetPasswordAsync", password);
		}
	}
}
