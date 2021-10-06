using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class UserCreateStrategyV1 : UserCreateStrategy
	{
		public UserCreateStrategyV1(
			IRestService restService,
			IRequireStrategy<Client> clientRequireStrategy,
			IChoiceResolveByObjectFieldAndNameStrategy choiceResolveByObjectFieldAndNameStrategy,
			IUserAddToGroupStrategy userAddToGroupStrategy)
			: base(restService, clientRequireStrategy, choiceResolveByObjectFieldAndNameStrategy, userAddToGroupStrategy)
		{
		}

		protected override int CreateUser(User entity)
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
						AllowDocumentSkipPreferenceChange = entity.DocumentSkip == UserDocumentSkip.Enabled,
						entity.DefaultSelectedFileType,
						entity.DocumentViewer,
						SkipDefaultPreference = entity.SkipDefaultPreference == UserSkipDefaultPreference.Skip,
					},
					entity.DisableOnDate,
					entity.TrustedIPs,
					entity.RelativityAccess,
					entity.EmailPreference,
					SavedSearchDefaultsToPublic = entity.AdvancedSearchPublicByDefault,
				}
			};

			return RestService.Post<Artifact>("Relativity-Identity/v1/users", dto).ArtifactID;
		}
	}
}
