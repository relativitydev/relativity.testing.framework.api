using System;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class RemoveInactiveImagingJobsStrategyNotSupported : IRemoveInactiveImagingJobsStrategy
	{
		public void Remove()
		{
			throw new ArgumentException("The method Remove Inactive Imaging Jobs does not support version of Relativity lower than 12.1.");
		}
	}
}
