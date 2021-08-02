using System.Threading.Tasks;

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
		/// Retrieves the max size of a mass imaging job.
		/// </summary>
		/// <returns>The maximum number of documents allowed to be mass imaged in one job.</returns>
		/// <example>
		/// <code>
		/// int massImagingJobSize = await _imagingEnvironmentService.GetMassImagingJobSizeAsync().ConfigureAwait(false);
		/// </code>
		/// </example>
		Task<int> GetMassImagingJobSizeAsync();

		/// <summary>
		/// Cleans up inactive imaging jobs.
		/// </summary>
		/// <example>
		/// <code>
		/// _imagingEnvironmentService.RemoveInactiveImagingJobs();
		/// </code>
		/// </example>
		void RemoveInactiveImagingJobs();

		/// <summary>
		/// Cleans up inactive imaging jobs.
		/// </summary>
		/// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		/// <example>
		/// <code>
		/// await _imagingEnvironmentService.RemoveInactiveImagingJobsAsync().ConfigureAwait(false);
		/// </code>
		/// </example>
		Task RemoveInactiveImagingJobsAsync();
	}
}
