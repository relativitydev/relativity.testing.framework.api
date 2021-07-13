using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ImagingSetRequestDTOV1
	{
		public ImagingSetRequestDTOV1(ImagingSetRequest request)
		{
			Request = request;
		}

		public ImagingSetRequest Request { get; set; }
	}
}
