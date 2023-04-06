using System;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class ImagingJobSubmitSingleDocumentStrategyNotSupported : IImagingJobSubmitSingleDocumentStrategy
	{
		private const string _NOT_SUPPORTED_EXCEPTION_MESSAGE = "The method Submit Single Document Job does not support version of Relativity lower than 12.1.";

		public long SubmitSingleDocument(int workspaceId, int documentArtifactId, SingleDocumentImagingJobRequest singleDocumentImagingJobRequest)
		{
			throw new ArgumentException(_NOT_SUPPORTED_EXCEPTION_MESSAGE);
		}
	}
}
