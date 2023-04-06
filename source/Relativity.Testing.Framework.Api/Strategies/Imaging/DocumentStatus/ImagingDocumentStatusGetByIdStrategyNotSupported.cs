using System;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class ImagingDocumentStatusGetByIdStrategyNotSupported : IImagingDocumentStatusGetByIdStrategy
	{
		public DocumentStatus Get(int workspaceId, int documentArtifactId)
		{
			throw new ArgumentException("The method Get Document Status does not support version of Relativity lower than 12.1.");
		}
	}
}
