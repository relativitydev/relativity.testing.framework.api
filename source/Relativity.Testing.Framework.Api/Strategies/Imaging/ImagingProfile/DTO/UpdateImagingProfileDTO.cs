using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	public class UpdateImagingProfileDTO : CreateNativeImagingProfileDTO
	{
		/// <summary>
		/// Gets or sets the imaging method used for running jobs with the imaging profile. The method options are basic or native.
		/// </summary>
		public ImagingProfileType ImagingMethod { get; set; }
	}
}
