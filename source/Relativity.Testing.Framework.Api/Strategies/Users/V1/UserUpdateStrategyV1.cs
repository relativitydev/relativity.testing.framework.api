using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class UserUpdateStrategyV1 : UserUpdateStrategy
	{
		private readonly IGetByIdStrategy<User> _getUserByIdStrategy;

		public UserUpdateStrategyV1(
			IRestService restService,
			IChoiceResolveByObjectFieldAndNameStrategy choiceResolveByObjectFieldAndNameStrategy,
			IUserAddToGroupStrategy userAddToGroupStrategy,
			IUserSetPasswordStrategy userSetPasswordStrategy,
			IGetByIdStrategy<User> getUserByIdStrategy)
			: base(restService, choiceResolveByObjectFieldAndNameStrategy, userAddToGroupStrategy, userSetPasswordStrategy)
		{
			_getUserByIdStrategy = getUserByIdStrategy;
		}

		protected override User UpdateUser(User entity)
		{
			var dto = new
			{
				UserRequest = new
				{
					entity.FirstName,
					entity.LastName,
					entity.EmailAddress,
					Type = new { ChoiceResolveByObjectFieldAndNameStrategy.ResolveReference("User", "User Type", entity.Type).ArtifactID },
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
						AllowDocumentSkipPreferenceChange = entity.DocumentSkip == UserDocumentSkip.Enabled,
						entity.DefaultSelectedFileType,
						entity.DocumentViewer,
						SkipDefaultPreference = entity.SkipDefaultPreference == UserSkipDefaultPreference.Skip
					},
					entity.DisableOnDate,
					entity.TrustedIPs,
					entity.RelativityAccess,
					entity.EmailPreference,
					SavedSearchDefaultsToPublic = entity.AdvancedSearchPublicByDefault,
				}
			};

			RestService.Put($"Relativity-Identity/v1/users/{entity.ArtifactID}", dto);

			return _getUserByIdStrategy.Get(entity.ArtifactID);
		}
	}
}
