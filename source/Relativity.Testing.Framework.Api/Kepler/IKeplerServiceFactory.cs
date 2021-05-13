using System;

namespace Relativity.Testing.Framework.Api.Kepler
{
	/// <summary>
	/// Represents the Kepler service factory that provides a set of methods to communicate with Kepler services.
	/// </summary>
	public interface IKeplerServiceFactory
	{
		/// <summary>
		/// Gets a service proxy for the curent user.
		/// </summary>
		/// <typeparam name="T">Kepler service interface.</typeparam>
		/// <returns>Service proxy for the given type.</returns>
		T GetServiceProxy<T>()
			where T : IDisposable;

		/// <summary>
		/// Gets a service proxy for the specified user.
		/// </summary>
		/// <typeparam name="T">Kepler service interface.</typeparam>
		/// <param name="username">The username.</param>
		/// <param name="password">The password.</param>
		/// <returns>Service proxy for the given type.</returns>
		T GetServiceProxy<T>(string username, string password)
			where T : IDisposable;
	}
}
