﻿using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IFolderGetWorkspaceRootFolderStrategy
	{
		Folder Get(int workspaceArtifactID);
	}
}
