using System;
using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class ImagingSetCreateStrategyNotSupported : IImagingSetCreateStrategy
	{
		private const string _NOT_SUPPORTED_EXCEPTION_MESSAGE = "The method Create Imaging Set does not support version of Relativity lower than 12.1.";

		public ImagingSet Create(int workspaceId, ImagingSetRequest imagingSetRequest)
		{
			throw new ArgumentException(_NOT_SUPPORTED_EXCEPTION_MESSAGE);
		}

		public Task<ImagingSet> CreateAsync(int workspaceId, ImagingSetRequest imagingSetRequest)
		{
			throw new ArgumentException(_NOT_SUPPORTED_EXCEPTION_MESSAGE);
		}
	}
}
