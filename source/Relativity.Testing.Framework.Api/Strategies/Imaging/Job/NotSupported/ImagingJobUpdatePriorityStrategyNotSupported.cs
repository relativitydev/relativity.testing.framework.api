using System;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class ImagingJobUpdatePriorityStrategyNotSupported : IImagingJobUpdatePriorityStrategy
	{
		private const string _NOT_SUPPORTED_EXCEPTION_MESSAGE = "The method Imaging Job Update Priority does not support version of Relativity lower than 12.1.";

		public ImagingJobActionResponse UpdatePriority(int workspaceId, long imagingJobId, ImagingJobPriorityRequest updateJobPriorityRequest)
		{
			throw new ArgumentException(_NOT_SUPPORTED_EXCEPTION_MESSAGE);
		}
	}
}
