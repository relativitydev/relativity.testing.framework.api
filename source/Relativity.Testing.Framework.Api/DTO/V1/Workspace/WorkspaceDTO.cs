using System;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.DTO
{
	internal class WorkspaceDTO
	{
		public int ArtifactID { get; set; }

		public string Name { get; set; }

		public Securable<NamedArtifact> Client { get; set; }

		public Securable<NamedArtifact> Matter { get; set; }

		public Securable<NamedArtifact> Template { get; set; }

		public NamedArtifact Status { get; set; }

		public ChoiceFieldValueDto SqlFullTextLanguage { get; set; }

		public Securable<NamedArtifact> SqlServer { get; set; }

		public Securable<NamedArtifact> ProductionRestrictions { get; set; }

		public Securable<Group> WorkspaceAdminGroup { get; set; }

		public Securable<NamedArtifact> ResourcePool { get; set; }

		public Securable<NamedArtifact> DefaultFileRepository { get; set; }

		public Securable<NamedArtifact> DataGridFileRepository { get; set; }

		public Securable<NamedArtifact> AzureCredentials { get; set; }

		public Securable<NamedArtifact> AzureFileSystemCredentials { get; set; }

		public Securable<NamedArtifact> DefaultCacheLocation { get; set; }

		public NamedArtifact DatabaseLocation { get; set; }

		public DateTime? CreatedOn { get; set; }

		public string DownloadHandlerUrl { get; set; }

		public bool EnableDataGrid { get; set; }

		public string Keywords { get; set; } = string.Empty;

		public string Notes { get; set; } = string.Empty;
	}
}
