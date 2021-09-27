using System;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Extensions
{
	internal static class UserDtoV1Extensions
	{
		public static User MapToUser(this UserDtoV1 userDto, Client client)
		{
			return new User
			{
				ArtifactID = userDto.ArtifactID,
				FirstName = userDto.FirstName,
				LastName = userDto.LastName,
				EmailAddress = userDto.EmailAddress,
				EmailPreference = userDto.EmailPreference,
				Type = userDto.Type.Name,
				ItemListPageLength = userDto.ItemListPageLength,
				Client = client,
				DefaultSelectedFileType = userDto.DocumentViewerProperties.DefaultSelectedFileType,
				ChangeSettings = userDto.AllowSettingsChange,
				TrustedIPs = userDto.TrustedIPs,
				RelativityAccess = userDto.RelativityAccess,
				KeyboardShortcuts = userDto.DocumentViewerProperties.AllowKeyboardShortcuts,
				DocumentViewer = userDto.DocumentViewerProperties.DocumentViewer,
				FullName = $"{userDto.FirstName}, {userDto.LastName}",
				BetaUser = false,
				AdvancedSearchPublicByDefault = false,
				NativeViewerCacheAhead = false,
				ChangePassword = false,
				MaximumPasswordAge = 90,
				ChangePasswordNextLogin = true,
				Password = string.Empty,
				DocumentSkip = UserDocumentSkip.Enabled,
				DataFocus = 5,
				EnforceViewerCompatibility = false,
				SkipDefaultPreference = UserSkipDefaultPreference.Skip,
				CanChangeDocumentViewer = true,
				DisableOnDate = DateTime.UtcNow.Date.AddYears(1).AddHours(8).AddMinutes(45).AddSeconds(30),
			};
		}
	}
}
