using System;
using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class FolderDto : NamedArtifact
	{
		public NamedArtifact ParentFolder { get; set; }

		public bool AccessControlListIsInherited { get; set; }

		public bool HasChildren { get; set; }

		public bool Selected { get; set; }

		public FolderPermissionDto Permissions { get; set; }

		public List<Folder> Children { get; set; }

		public DateTime SystemCreatedOn { get; set; }

		public DateTime SystemLastModifiedOn { get; set; }
	}
}
