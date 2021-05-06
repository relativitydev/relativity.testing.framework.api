using System;
using System.Linq;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class UserUpdateStrategy : IUpdateStrategy<User>
	{
		private readonly IRestService _restService;

		private readonly IChoiceResolveByObjectFieldAndNameStrategy _choiceResolveByObjectFieldAndNameStrategy;

		private readonly IUserGetByEmailStrategy _userGetByEmailStrategy;

		private readonly IUserAddToGroupStrategy _userAddToGroupStrategy;

		public UserUpdateStrategy(
			IRestService restService,
			IChoiceResolveByObjectFieldAndNameStrategy choiceResolveByObjectFieldAndNameStrategy,
			IUserGetByEmailStrategy userGetByEmailStrategy,
			IUserAddToGroupStrategy userAddToGroupStrategy)
		{
			_restService = restService;
			_choiceResolveByObjectFieldAndNameStrategy = choiceResolveByObjectFieldAndNameStrategy;
			_userGetByEmailStrategy = userGetByEmailStrategy;
			_userAddToGroupStrategy = userAddToGroupStrategy;
		}

		public void Update(User entity)
		{
			if (entity is null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

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
					Client = entity.Client == null ? null : new
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

			_restService.Put($"Relativity.Users/workspace/-1/Users/{entity.ArtifactID}", dto);

			var createdUser = _userGetByEmailStrategy.Get(entity.EmailAddress);

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
					_userAddToGroupStrategy.AddToGroup(createdUser.ArtifactID, group.ArtifactID);
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
