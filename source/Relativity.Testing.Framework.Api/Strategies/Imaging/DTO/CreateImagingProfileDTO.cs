using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// A DTO used for creation of an Imaging Profile.
	/// </summary>
	public abstract class CreateImagingProfileDTO
	{
		/// <summary>
		/// Gets or sets the name of the Imaging Profile.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the basic options used for Imaging, such as image output quality (DPI), image size, and image format.
		/// </summary>
		public BasicImagingEngineOptions BasicOptions { get; set; }

		/// <summary>
		/// Gets or sets the Keywords field.
		/// </summary>
		public string Keywords { get; set; }

		/// <summary>
		/// Gets or sets Notes Keywords field.
		/// </summary>
		public string Notes { get; set; }

		/// <summary>
		/// Gets or sets the Native Types instances linked to this Imaging Profile.
		/// If none are passed, a default collection will be linked to the Imaging Profile.
		/// </summary>
		public IEnumerable<NamedArtifact> NativeTypes { get; set; }
	}
}
