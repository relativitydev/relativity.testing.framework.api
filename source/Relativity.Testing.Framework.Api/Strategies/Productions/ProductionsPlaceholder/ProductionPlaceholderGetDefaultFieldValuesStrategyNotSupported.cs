using System;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class ProductionPlaceholderGetDefaultFieldValuesStrategyNotSupported : IProductionPlaceholderGetDefaultFieldValuesStrategy
	{
		public DefaultFieldValue<NamedArtifact> Get(int workspaceArtifactID)
		{
			throw new ArgumentException("The method Get Production Placeholder Default Field Values does not support version of Relativity lower than 12.1.");
		}
	}
}
