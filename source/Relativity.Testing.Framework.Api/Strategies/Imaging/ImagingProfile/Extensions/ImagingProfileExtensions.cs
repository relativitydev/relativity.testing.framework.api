using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal static class ImagingProfileExtensions
	{
		internal static UpdateImagingProfileDTO MapToUpdateDTO(this ImagingProfile value)
		{
			return new UpdateImagingProfileDTO
			{
				ApplicationFieldCodes = value.ApplicationFieldCodes,
				BasicOptions = value.BasicOptions,
				EmailOptions = value.EmailOptions,
				HtmlOptions = value.HtmlOptions,
				ImagingMethod = value.ImagingMethod,
				Keywords = value.Keywords,
				Name = value.Name,
				NativeOptions = value.NativeOptions,
				NativeTypes = value.NativeTypes,
				Notes = value.Notes,
				PresentationOptions = value.PresentationOptions,
				SpreadsheetOptions = value.SpreadsheetOptions,
				WordProcessingOptions = value.WordProcessingOptions
			};
		}
	}
}
