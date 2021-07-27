using System;
using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class ImagingJobSubmitMassDocumentStrategyNotSupported : IImagingJobSubmitMassDocumentStrategy
	{
		private const string _NOT_SUPPORTED_EXCEPTION_MESSAGE = "The method Submit Mass Document does not support version of Relativity lower than 12.1.";

		public long SubmitMassDocument(int workspaceId, ImagingMassJobRequest imagingMassJobRequest)
		{
			throw new ArgumentException(_NOT_SUPPORTED_EXCEPTION_MESSAGE);
		}

		public Task<long> SubmitMassDocumentAsync(int workspaceId, ImagingMassJobRequest imagingMassJobRequest)
		{
			throw new ArgumentException(_NOT_SUPPORTED_EXCEPTION_MESSAGE);
		}
	}
}
