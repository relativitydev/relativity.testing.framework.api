using System.Net.Http;
using Relativity.Testing.Framework.Api.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the HTTP service that provides a set of methods to communicate with Relativity REST API.
	/// </summary>
	public interface IHttpService
	{
		/// <summary>
		/// Gets the base URL address.
		/// </summary>
		string BaseUrl { get; }

		/// <summary>
		/// Gets the username.
		/// </summary>
		string Username { get; }

		/// <summary>
		/// Executes a GET HTTP request to the specified <paramref name="relativeUri"/>,
		/// then deserializes the response content to <typeparamref name="TResult"/> and returns it.
		/// </summary>
		/// <typeparam name="TResult">The type of the result to deserialize response content to.</typeparam>
		/// <param name="relativeUri">The endpoint relative URI.</param>
		/// <param name="userCredentials">User credentials to be used when perfroming action over Relativity Api.</param>
		/// <returns>The response content deserialized to <typeparamref name="TResult"/>.</returns>
		/// <exception cref="HttpRequestException">
		/// The response has <see cref="HttpResponseMessage.IsSuccessStatusCode"/> equal to <see langword="false"/>.
		/// </exception>
		TResult Get<TResult>(string relativeUri, UserCredentials userCredentials = null);

		/// <summary>
		/// Executes a POST HTTP request to the specified <paramref name="relativeUri"/>
		/// with optionally <paramref name="content"/> serialized to JSON,
		/// then deserializes the response content to <typeparamref name="TResult"/> and returns it.
		/// </summary>
		/// <typeparam name="TResult">The type of the result to deserialize response content to.</typeparam>
		/// <param name="relativeUri">The endpoint relative URI.</param>
		/// <param name="content">The content object.</param>
		/// <param name="timeout">Number of minutes to wait before timeing out. Default is 2.</param>
		/// <param name="userCredentials">User credentials to be used when perfroming action over Relativity Api.</param>
		/// <returns>The response content deserialized to <typeparamref name="TResult"/>.</returns>
		/// <exception cref="HttpRequestException">
		/// The response has <see cref="HttpResponseMessage.IsSuccessStatusCode"/> equal to <see langword="false"/>.
		/// </exception>
		TResult Post<TResult>(string relativeUri, object content = null, double timeout = 2, UserCredentials userCredentials = null);

		/// <summary>
		/// Executes a POST HTTP request without response to the specified <paramref name="relativeUri"/>
		/// with optionally <paramref name="content"/> serialized to JSON.
		/// </summary>
		/// <param name="relativeUri">The endpoint relative URI.</param>
		/// <param name="content">The content object.</param>
		/// <param name="userCredentials">User credentials to be used when perfroming action over Relativity Api.</param>
		/// <exception cref="HttpRequestException">
		/// The response has <see cref="HttpResponseMessage.IsSuccessStatusCode"/> equal to <see langword="false"/>.
		/// </exception>
		void Post(string relativeUri, object content = null, UserCredentials userCredentials = null);

		/// <summary>
		/// Executes a PUT HTTP request to the specified <paramref name="relativeUri"/>
		/// with optionally <paramref name="content"/> serialized to JSON,
		/// then deserializes the response content to <typeparamref name="TResult"/> and returns it.
		/// </summary>
		/// <typeparam name="TResult">The type of the result to deserialize response content to.</typeparam>
		/// <param name="relativeUri">The endpoint relative URI.</param>
		/// <param name="content">The content object.</param>
		/// <param name="userCredentials">User credentials to be used when perfroming action over Relativity Api.</param>
		/// <returns>The response content deserialized to <typeparamref name="TResult"/>.</returns>
		/// <exception cref="HttpRequestException">
		/// The response has <see cref="HttpResponseMessage.IsSuccessStatusCode"/> equal to <see langword="false"/>.
		/// </exception>
		TResult Put<TResult>(string relativeUri, object content = null, UserCredentials userCredentials = null);

		/// <summary>
		/// Executes a PUT HTTP request without response to the specified <paramref name="relativeUri"/>
		/// with optionally <paramref name="content"/> serialized to JSON.
		/// </summary>
		/// <param name="relativeUri">The endpoint relative URI.</param>
		/// <param name="content">The content object.</param>
		/// <param name="userCredentials">User credentials to be used when perfroming action over Relativity Api.</param>
		/// <exception cref="HttpRequestException">
		/// The response has <see cref="HttpResponseMessage.IsSuccessStatusCode"/> equal to <see langword="false"/>.
		/// </exception>
		void Put(string relativeUri, object content = null, UserCredentials userCredentials = null);

		/// <summary>
		/// Executes a DELETE HTTP request to the specified <paramref name="relativeUri"/>
		/// with optionally <paramref name="content"/> serialized to JSON,
		/// then deserializes the response content to <typeparamref name="TResult"/> and returns it.
		/// </summary>
		/// <typeparam name="TResult">The type of the result to deserialize response content to.</typeparam>
		/// <param name="relativeUri">The endpoint relative URI.</param>
		/// <param name="content">The content object.</param>
		/// <param name="userCredentials">User credentials to be used when perfroming action over Relativity Api.</param>
		/// <returns>The response content deserialized to <typeparamref name="TResult"/>.</returns>
		/// <exception cref="HttpRequestException">
		/// The response has <see cref="HttpResponseMessage.IsSuccessStatusCode"/> equal to <see langword="false"/>.
		/// </exception>
		TResult Delete<TResult>(string relativeUri, object content = null, UserCredentials userCredentials = null);

		/// <summary>
		/// Executes a DELETE HTTP request without response to the specified <paramref name="relativeUri"/>
		/// with optionally <paramref name="content"/> serialized to JSON.
		/// </summary>
		/// <param name="relativeUri">The endpoint relative URI.</param>
		/// <param name="content">The content object.</param>
		/// <param name="userCredentials">User credentials to be used when perfroming action over Relativity Api.</param>
		/// <exception cref="HttpRequestException">
		/// The response has <see cref="HttpResponseMessage.IsSuccessStatusCode"/> equal to <see langword="false"/>.
		/// </exception>
		void Delete(string relativeUri, object content = null, UserCredentials userCredentials = null);
	}
}
