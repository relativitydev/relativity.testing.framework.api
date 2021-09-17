using System.Linq;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class UserCreateStrategy : CreateStrategy<User>
	{
		private readonly IRestService _restService;
		private readonly IRequireStrategy<Client> _clientRequireStrategy;
		private readonly IChoiceResolveByObjectFieldAndNameStrategy _choiceResolveByObjectFieldAndNameStrategy;
		private readonly IUserGetByEmailStrategy _userGetByEmailStrategy;
		private readonly IUserAddToGroupStrategy _userAddToGroupStrategy;
		private readonly IGetByIdStrategy<User> _getByIdStrategy;

		public UserCreateStrategy(
			IRestService restService,
			IRequireStrategy<Client> clientRequireStrategy,
			IChoiceResolveByObjectFieldAndNameStrategy choiceResolveByObjectFieldAndNameStrategy,
			IUserGetByEmailStrategy userGetByEmailStrategy,
			IUserAddToGroupStrategy userAddToGroupStrategy,
			IGetByIdStrategy<User> getByIdStrategy)
		{
			_restService = restService;
			_clientRequireStrategy = clientRequireStrategy;
			_choiceResolveByObjectFieldAndNameStrategy = choiceResolveByObjectFieldAndNameStrategy;
			_userGetByEmailStrategy = userGetByEmailStrategy;
			_userAddToGroupStrategy = userAddToGroupStrategy;
			_getByIdStrategy = getByIdStrategy;
		}

		protected override User DoCreate(User entity)
		{
			entity.Client = _clientRequireStrategy.Require(entity.Client);

			var dto = new
			{
				UserRequest = new
				{
					entity.FirstName,
					entity.LastName,
					entity.EmailAddress,
					Type = new { _choiceResolveByObjectFieldAndNameStrategy.ResolveReference("User", "User Type", entity.Type).ArtifactID },
					entity.ItemListPageLength,
					AllowSettingsChange = entity.ChangeSettings,
					DefaultFilterVisibility = entity.ShowFilters,
					Client = new
					{
						Secured = false,
						Value = new
						{
							entity.Client.ArtifactID,
						},
					},
					DocumentViewerProperties = new
					{
						AllowDocumentViewerChange = entity.CanChangeDocumentViewer,
						AllowKeyboardShortcuts = entity.KeyboardShortcuts,
						AllowDocumentSkipPreferenceChange = entity.DocumentSkip,
						entity.DefaultSelectedFileType,
						entity.DocumentViewer,
						entity.SkipDefaultPreference,
					},
					entity.DisableOnDate,
					entity.TrustedIPs,
					entity.RelativityAccess,
					entity.EmailPreference
				}
			};

			_restService.Post("Relativity.Users/workspace/-1/Users", dto);

			var createdUser = _userGetByEmailStrategy.Get(entity.EmailAddress);

			AddPasswordProvider(createdUser.ArtifactID);

			SetPassword(createdUser.ArtifactID, entity.Password);

			if (entity.Groups.Any())
			{
				foreach (var group in entity.Groups)
				{
					_userAddToGroupStrategy.AddToGroup(createdUser.ArtifactID, group.ArtifactID);
				}
			}

			return _getByIdStrategy.Get(createdUser.ArtifactID);
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

			_restService.Post("Relativity.Services.Security.ISecurityModule/Login Profile Manager/SaveLoginProfileAsync", passwordProviderProfile);
		}

		private void SetPassword(int userArtifactId, string passwordToSet)
		{
			var password = new
			{
				UserId = userArtifactId,
				Password = passwordToSet
			};

			_restService.Post("Relativity.Services.Security.ISecurityModule/Login Profile Manager/SetPasswordAsync", password);
		}
	}
}
