using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ImagingJobRequestDTOV1
	{
		public ImagingJobRequestDTOV1(ImagingSetJobRequest imagingSetJobRequest)
		{
			ImagingSetRequest = imagingSetJobRequest ?? new ImagingSetJobRequest();
		}

		public ImagingSetJobRequest ImagingSetRequest { get; set; }
	}
}
