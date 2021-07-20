using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ImagingJobRequest
	{
		public ImagingJobRequest(ImagingSetJobRequest imagingSetJobRequest)
		{
			ImagingSetRequest = imagingSetJobRequest ?? new ImagingSetJobRequest();
		}

		public ImagingSetJobRequest ImagingSetRequest { get; set; }
	}
}
