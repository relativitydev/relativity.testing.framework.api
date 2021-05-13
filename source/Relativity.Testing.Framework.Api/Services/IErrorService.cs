using System;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the error API service.
	/// </summary>
	public interface IErrorService
	{
		/// <summary>
		/// Creates the specified error.
		/// </summary>
		/// <param name="entity">The entity to create.</param>
		/// <returns>The created entity.</returns>
		Error Create(Error entity);

		/// <summary>
		/// Gets the error by the specified ID.
		/// </summary>
		/// <param name="entityId">The Artifact ID of the error.</param>
		/// <returns>>The <see cref="Entity"/> error or <see langword="null"/>.</returns>
		Error Get(int entityId);

		/// <summary>
		/// Gets all errors.
		/// </summary>
		/// <returns>The collection of <see cref="Error"/> errors.</returns>
		Error[] GetAll();

		/// <summary>
		/// Gets all errors by specific date.
		/// </summary>
		/// <param name="from">The start of a date range.</param>
		/// <param name="to">The end of a date range.</param>
		/// <returns>The collection of <see cref="Error"/> errors.</returns>
		Error[] GetAllByDate(DateTime from, DateTime to);
	}
}
