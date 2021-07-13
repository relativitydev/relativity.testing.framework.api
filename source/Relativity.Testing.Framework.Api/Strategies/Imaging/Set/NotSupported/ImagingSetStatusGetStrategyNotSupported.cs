using System;
using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	public class ImagingSetStatusGetStrategyNotSupported : IImagingSetStatusGetStrategy
	{
		private const string _NOT_SUPPORTED_EXCEPTION_MESSAGE = "The method Get Imaging Set Status does not support version of Relativity lower than 12.1.";

		public ImagingSetDetailedStatus Get(int workspaceId, int imagingSetId)
		{
			throw new ArgumentException(_NOT_SUPPORTED_EXCEPTION_MESSAGE);
		}

		public Task<ImagingSetDetailedStatus> GetAsync(int workspaceId, int imagingSetId)
		{
			throw new ArgumentException(_NOT_SUPPORTED_EXCEPTION_MESSAGE);
		}
	}
}
