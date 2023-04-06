﻿using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IMatterGetEligibleClientsStrategy
	{
		ArtifactIdNamePair[] GetAll();
	}
}
