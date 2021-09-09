﻿using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IProductionPlaceholderGetDefaultFieldValuesStrategy
	{
		DefaultFieldValue<NamedArtifact> Get(int workspaceArtifactID);
	}
}
