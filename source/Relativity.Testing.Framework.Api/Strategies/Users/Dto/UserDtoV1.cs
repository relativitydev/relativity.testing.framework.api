using System;
using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class UserDtoV1
	{
		public int ArtifactID { get; set; }

		public List<Guid> Guids { get; set; } = new List<Guid>();

		public bool AllowSettingsChange { get; set; }

		public Securable<NamedArtifact> Client { get; set; }

		public bool DefaultFilterVisibility { get; set; }

		public DateTime? DisableOnDate { get; set; }

		public DocumentViewerPropertiesDtoV1 DocumentViewerProperties { get; set; }

		public string EmailAddress { get; set; }

		public UserEmailPreference EmailPreference { get; set; }

		public string FirstName { get; set; }

		public int ItemListPageLength { get; set; }

		public string LastName { get; set; }

		public bool RelativityAccess { get; set; }

		public bool SavedSearchDefaultsToPublic { get; set; }

		public string TrustedIPs { get; set; }

		public NamedArtifact Type { get; set; }

		public string Keywords { get; set; }

		public string Notes { get; set; }

		public DateTime CreatedOn { get; set; }

		public NamedArtifact CreatedBy { get; set; }

		public NamedArtifact LastModifiedBy { get; set; }

		public DateTime LastModifiedOn { get; set; }

		public Meta Meta { get; set; }

		public List<HttpAction> Actions { get; set; }

		public DateTime? LastLoginDate { get; set; }
	}
}
