using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// A DTO used for creation of Native Imaging Profile.
	/// </summary>
	public class CreateNativeImagingProfileDTO : CreateImagingProfileDTO
	{
		/// <summary>
		/// Gets or sets the native imaging options used by this Imaging Profile, such as image format, dithering algorithm, and output quality (DPI).
		/// </summary>
		public NativeImagingEngineOptions NativeOptions { get; set; }

		/// <summary>
		/// Gets or sets the spreadsheet options used for native imaging, such as page orientation, page size, and the display of row and column headings.
		/// </summary>
		/// <seealso cref="ImagingSpreadsheetOptions"/>
		public ImagingSpreadsheetOptions SpreadsheetOptions { get; set; }

		/// <summary>
		/// Gets or sets the email options used for native imaging, such as page orientation, the removal of indentations, and the display of SMTP addresses.
		/// </summary>
		public ImagingEmailOptions EmailOptions { get; set; }

		/// <summary>
		/// Gets or sets word processing options used for native imaging, such as page orientation, or displaying field codes, comments, and hidden text.
		/// </summary>
		public ImagingWordOptions WordProcessingOptions { get; set; }

		/// <summary>
		/// Gets or sets options used for native imaging documents from presentation software, such as Microsoft PowerPoint.
		/// </summary>
		public ImagingPresentationOptions PresentationOptions { get; set; }

		/// <summary>
		/// Gets or sets the HTML option used for the imaging profile. It removes non-breaking spaces codes.
		/// </summary>
		public ImagingHtmlOptions HtmlOptions { get; set; }

		/// <summary>
		/// Gets or sets the collection of Application Field Codes linked to this imaging profile.
		/// </summary>
		public IEnumerable<NamedArtifact> ApplicationFieldCodes { get; set; }
	}
}
