using System;
using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class ImagingSetGetStrategyNotSupported : IImagingSetGetStrategy
	{
		private const string _NOT_SUPPORTED_EXCEPTION_MESSAGE = "The method Get Imaging Set does not support version of Relativity lower than 12.1.";

		public ImagingSet Get(int workspaceId, int imagingSetId)
		{
			throw new ArgumentException(_NOT_SUPPORTED_EXCEPTION_MESSAGE);
		}

		public Task<ImagingSet> GetAsync(int workspaceId, int imagingSetId)
		{
			throw new ArgumentException(_NOT_SUPPORTED_EXCEPTION_MESSAGE);
		}
	}
}
