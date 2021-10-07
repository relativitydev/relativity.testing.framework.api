using System.Collections.Generic;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal abstract class UserCreateStrategy : CreateStrategy<User>
	{
		protected UserCreateStrategy(
			IRestService restService,
			IRequireStrategy<Client> clientRequireStrategy,
			IChoiceResolveByObjectFieldAndNameStrategy choiceResolveByObjectFieldAndNameStrategy,
			IUserAddToGroupStrategy userAddToGroupStrategy)
		{
			RestService = restService;
			ClientRequireStrategy = clientRequireStrategy;
			ChoiceResolveByObjectFieldAndNameStrategy = choiceResolveByObjectFieldAndNameStrategy;
			UserAddToGroupStrategy = userAddToGroupStrategy;
		}

		protected IRestService RestService { get; }

		protected IRequireStrategy<Client> ClientRequireStrategy { get; }

		protected IChoiceResolveByObjectFieldAndNameStrategy ChoiceResolveByObjectFieldAndNameStrategy { get; }

		protected IUserAddToGroupStrategy UserAddToGroupStrategy { get; }

		protected abstract int CreateUser(User entity);

		protected override User DoCreate(User entity)
		{
			entity.Client = ClientRequireStrategy.Require(entity.Client);

			var userArtifactID = CreateUser(entity);

			AddPasswordProvider(userArtifactID);

			SetPassword(userArtifactID, entity.Password);

			AddToGroups(userArtifactID, entity.Groups);

			entity.ArtifactID = userArtifactID;

			return entity;
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

		private void AddToGroups(int userArtifactId, IList<Artifact> groups)
		{
			if (groups != null)
			{
				foreach (var group in groups)
				{
					UserAddToGroupStrategy.AddToGroup(userArtifactId, group.ArtifactID);
				}
			}
		}
	}
}
