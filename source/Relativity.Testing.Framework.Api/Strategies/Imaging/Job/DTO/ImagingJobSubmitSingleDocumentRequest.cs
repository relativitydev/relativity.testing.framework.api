using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// ImageDocumentJob - This is used by image on the fly.  It should not be used for mass imaging.
	/// It allows one document to use an alternate native (ie... for PDF image upload).
	/// </summary>
	public class ImagingJobSubmitSingleDocumentRequest : ImagingJobRequest
	{
		/// <summary>
		/// Gets or sets the id of the imaging profile to use.
		/// </summary>
		public int ProfileID { get; set; }

		/// <summary>
		/// Gets or sets optional - location of an alternate native to use while imaging the document
		/// This file will not be attached to the document, it is only used to generate images.
		/// </summary>
		public string AlternateNativeLocation { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether to remove alternate native files (if specified) after they are imaged.
		/// </summary>
		public bool RemoveAlternateNativeAfterImaging { get; set; }
	}
}
