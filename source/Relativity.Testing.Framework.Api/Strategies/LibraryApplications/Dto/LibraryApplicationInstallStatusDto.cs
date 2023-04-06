using System;
using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class LibraryApplicationInstallStatusDto
	{
		public int ApplicationInstallID { get; set; }

		public DateTime CompletedOn { get; set; }

		public InstallStatusModelDto InstallStatus { get; set; }

		public List<string> ValidationMessages { get; set; }

		public Artifact WorkspaceIdentifier { get; set; }

		public Artifact ApplicationIdentifier { get; set; }
	}
}
