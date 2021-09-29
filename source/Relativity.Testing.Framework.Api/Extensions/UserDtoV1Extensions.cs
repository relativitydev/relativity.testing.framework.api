using System.Collections.Generic;
using System.Linq;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Extensions
{
	internal static class UserDtoV1Extensions
	{
		public static User MapToUser(this UserDtoV1 userDto, Client client, IList<NamedArtifact> groups)
		{
			var user = new User
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
				SkipDefaultPreference = userDto.DocumentViewerProperties.SkipDefaultPreference ? UserSkipDefaultPreference.Skip : UserSkipDefaultPreference.Normal,
				AdvancedSearchPublicByDefault = userDto.SavedSearchDefaultsToPublic,
				CanChangeDocumentViewer = userDto.DocumentViewerProperties.AllowDocumentViewerChange,
				ShowFilters = userDto.DefaultFilterVisibility,
				Name = userDto.LastName,
				DocumentSkip = userDto.DocumentViewerProperties.AllowDocumentSkipPreferenceChange ? UserDocumentSkip.Enabled : UserDocumentSkip.Disabled,
				Groups = groups == null ? new List<Artifact>() : groups.Select(x => new Artifact { ArtifactID = x.ArtifactID }).ToList()
			};

			return user;
		}
	}
}
