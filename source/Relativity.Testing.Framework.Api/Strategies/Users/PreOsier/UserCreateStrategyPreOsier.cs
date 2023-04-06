using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class UserCreateStrategyPreOsier : UserCreateStrategy
	{
		private readonly IUserGetByEmailStrategy _userGetByEmailStrategy;

		public UserCreateStrategyPreOsier(
			IRestService restService,
			IRequireStrategy<Client> clientRequireStrategy,
			IChoiceResolveByObjectFieldAndNameStrategy choiceResolveByObjectFieldAndNameStrategy,
			IUserAddToGroupStrategy userAddToGroupStrategy,
			IUserSetPasswordStrategy userSetPasswordStrategy,
			IUserGetByEmailStrategy userGetByEmailStrategy)
			: base(restService, clientRequireStrategy, choiceResolveByObjectFieldAndNameStrategy, userAddToGroupStrategy, userSetPasswordStrategy)
		{
			_userGetByEmailStrategy = userGetByEmailStrategy;
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

			RestService.Post("Relativity.Users/workspace/-1/Users", dto);

			var createdUser = _userGetByEmailStrategy.Get(entity.EmailAddress);

			return createdUser.ArtifactID;
		}
	}
}
