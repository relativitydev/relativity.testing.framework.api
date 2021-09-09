namespace Relativity.Testing.Framework.Api.Services.Imaging
{
	/// <summary>
	/// Represents the Imaging Environment API service.
	/// </summary>
	/// <example>
	/// <code>
	/// _imagingEnvironmentService = relativityFacade.Resolve&lt;IImagingEnvironmentService&gt;();
	/// </code>
	/// </example>
	public interface IImagingEnvironmentService
	{
		/// <summary>
		/// Retrieves the max size of a mass imaging job.
		/// </summary>
		/// <returns>The maximum number of documents allowed to be mass imaged in one job.</returns>
		/// <example>
		/// <code>
		/// int massImagingJobSize = _imagingEnvironmentService.GetMassImagingJobSize();
		/// </code>
		/// </example>
		int GetMassImagingJobSize();

		/// <summary>
		/// Cleans up inactive imaging jobs.
		/// </summary>
		/// <example>
		/// <code>
		/// _imagingEnvironmentService.RemoveInactiveImagingJobs();
		/// </code>
		/// </example>
		void RemoveInactiveImagingJobs();
	}
}
