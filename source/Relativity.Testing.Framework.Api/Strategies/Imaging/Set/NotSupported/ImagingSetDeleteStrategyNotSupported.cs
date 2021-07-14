using System;
using System.Threading.Tasks;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class ImagingSetDeleteStrategyNotSupported : IImagingSetDeleteStrategy
	{
		private const string _NOT_SUPPORTED_EXCEPTION_MESSAGE = "The method Delete Imaging Set does not support version of Relativity lower than 12.1.";

		public void Delete(int workspaceId, int imagingSetId)
		{
			throw new ArgumentException(_NOT_SUPPORTED_EXCEPTION_MESSAGE);
		}

		public Task DeleteAsync(int workspaceId, int imagingSetId)
		{
			throw new ArgumentException(_NOT_SUPPORTED_EXCEPTION_MESSAGE);
		}
	}
}
