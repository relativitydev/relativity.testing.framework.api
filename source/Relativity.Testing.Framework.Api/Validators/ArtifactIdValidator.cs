using System;

namespace Relativity.Testing.Framework.Api.Validators
{
	internal class ArtifactIdValidator : IArtifactIdValidator
	{
		public void Validate(int artifactId, string artifactName)
		{
			if (artifactId < 1)
			{
				throw new ArgumentException($"{artifactName} ID should be greater than zero.");
			}
		}
	}
}
