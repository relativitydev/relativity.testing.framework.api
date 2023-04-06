using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal static class ApplicationFieldCodeExtensions
	{
		internal static ApplicationFieldCodeCreateRequest MapToCreateRequest(this ApplicationFieldCode value)
		{
			return new ApplicationFieldCodeCreateRequest
			{
				Application = value.Application,
				FieldCode = value.FieldCode,
				ImagingProfiles = value.ImagingProfiles,
				Option = value.Option,
				RelativityField = value.RelativityField
			};
		}
	}
}
