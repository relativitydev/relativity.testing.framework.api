using System;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the error API service.
	/// </summary>
	/// <example>
	/// <code>
	/// _errorService = relativityFacade.Resolve&lt;IErrorService&gt;();
	/// </code>
	/// </example>
	public interface IErrorService
	{
		/// <summary>
		/// Creates the specified error.
		/// </summary>
		/// <param name="entity">The entity to create.</param>
		/// <returns>The created entity.</returns>
		/// <example>
		/// <code>
		/// var entity = new Error { Message = "Test message" };
		/// var error = _errorService.Create(entity);
		/// </code>
		/// </example>
		Error Create(Error entity);

		/// <summary>
		/// Gets the error by the specified ID.
		/// </summary>
		/// <param name="entityId">The Artifact ID of the error.</param>
		/// <returns>>The <see cref="Entity"/> error or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// var error = _errorService.Get(someExistingErrorArtifactId);
		/// </code>
		/// </example>
		Error Get(int entityId);

		/// <summary>
		/// Gets all errors.
		/// </summary>
		/// <returns>The collection of <see cref="Framework.Models.Error"/> errors.</returns>
		/// <example>
		/// <code>
		/// var errors = _errorService.GetAll(entity);
		/// </code>
		/// </example>
		Error[] GetAll();

		/// <summary>
		/// Gets all errors by specific date.
		/// </summary>
		/// <param name="from">The start of a date range.</param>
		/// <param name="to">The end of a date range.</param>
		/// <returns>The collection of <see cref="Framework.Models.Error"/> errors.</returns>
		/// <example>
		/// <code>
		/// var currentDateTime = DateTime.Now;
		/// var error = _errorService.GetAllByDate(currentDateTime.AddDays(-1), currentDateTime);
		/// </code>
		/// </example>
		Error[] GetAllByDate(DateTime from, DateTime to);
	}
}
