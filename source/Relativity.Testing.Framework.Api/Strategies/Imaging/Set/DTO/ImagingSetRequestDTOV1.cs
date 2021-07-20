using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ImagingSetRequestDtoV1
	{
		public ImagingSetRequestDtoV1(ImagingSetRequest request)
		{
			Request = request;
		}

		public ImagingSetRequest Request { get; set; }
	}
}
