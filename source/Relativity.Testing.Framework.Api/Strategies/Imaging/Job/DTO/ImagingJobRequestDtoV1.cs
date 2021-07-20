using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ImagingJobRequestDtoV1
	{
		public ImagingJobRequestDtoV1(ImagingSetJobRequest imagingSetJobRequest)
		{
			ImagingSetRequest = imagingSetJobRequest ?? new ImagingSetJobRequest();
		}

		public ImagingSetJobRequest ImagingSetRequest { get; set; }
	}
}
