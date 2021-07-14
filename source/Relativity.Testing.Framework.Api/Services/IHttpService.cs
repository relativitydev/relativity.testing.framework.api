using System.Net.Http;
using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the HTTP service that provides a set of methods to communicate with Relativity REST API.
	/// </summary>
	/// <example>
	/// <code>
	/// _httpService = relativityFacade.Resolve&lt;IHttpService&gt;();
	/// </code>
	/// </example>
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
		/// <example>
		/// <code>
		/// JObject result = _restService.Get&lt;JObject&gt;($"relativity.agents/workspace/-1/agents/{id}");
		/// </code>
		/// </example>
		TResult Get<TResult>(string relativeUri, UserCredentials userCredentials = null);

		/// <summary>
		/// Executes a async GET HTTP request to the specified <paramref name="relativeUri"/>,
		/// then deserializes the response content to <typeparamref name="TResult"/> and returns it.
		/// </summary>
		/// <typeparam name="TResult">The type of the result to deserialize response content to.</typeparam>
		/// <param name="relativeUri">The endpoint relative URI.</param>
		/// <param name="userCredentials">User credentials to be used when perfroming action over Relativity Api.</param>
		/// <returns>The task with response content deserialized to <typeparamref name="TResult"/>.</returns>
		/// <exception cref="HttpRequestException">
		/// The response has <see cref="HttpResponseMessage.IsSuccessStatusCode"/> equal to <see langword="false"/>.
		/// </exception>
		/// <example>
		/// <code>
		/// string url = "some/url"
		/// ImagingSet result = await _restService.GetAsync&lt;ImagingSet&gt;(url).ConfigureAwait(false);
		/// </code>
		/// </example>
		Task<TResult> GetAsync<TResult>(string relativeUri, UserCredentials userCredentials = null);

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
		/// <example>
		/// <code>
		/// var dto = new
		/// {
		/// 	Request = new
		/// 	{
		/// 		Object = new
		/// 		{
		/// 			ArtifactID = 12345
		/// 		}
		/// 	}
		/// };
		///
		/// _restService.Post&lt;string&gt;($"Relativity.Objects/workspace/{workspaceId}/object/delete", dto);
		/// </code>
		/// </example>
		/// <example>
		/// <code>
		/// var dto = new
		/// {
		/// 	batchSet = new
		/// 	{
		/// 		Name = "Test Batch Set",
		/// 		BatchPrefix = "TEST",
		/// 		BatchSize = 10,
		/// 	}
		/// };
		///
		/// BatchSet result =  _restService.Post&lt;BatchSet&gt;($"Relativity.Services.Review.Batching.IBatchingModule/workspaces/{workspaceId}/batching/CreateBatchSet", dto, new UserCredentials { Username = "FakeAccount", Password = "FakePassword" });
		/// </code>
		/// </example>
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
		/// <example>
		/// <code>
		/// var dto = new
		/// {
		/// 	workspaceArtifactID = 54321,
		/// 	searchArtifactID = 12345
		/// };
		///
		/// _restService.Post("Relativity.Services.Search.ISearchModule/Keyword%20Search%20Manager/DeleteSingleAsync", dto);
		/// </code>
		/// </example>
		void Post(string relativeUri, object content = null, UserCredentials userCredentials = null);

		/// <summary>
		/// Executes a async POST HTTP request to the specified <paramref name="relativeUri"/>
		/// with optionally <paramref name="content"/> serialized to JSON,
		/// then deserializes the response content to <typeparamref name="TResult"/> and returns it.
		/// </summary>
		/// <typeparam name="TResult">The type of the result to deserialize response content to.</typeparam>
		/// <param name="relativeUri">The endpoint relative URI.</param>
		/// <param name="content">The content object.</param>
		/// <param name="timeout">Number of minutes to wait before timeing out. Default is 2.</param>
		/// <param name="userCredentials">User credentials to be used when perfroming action over Relativity Api.</param>
		/// <returns>The task with response content deserialized to <typeparamref name="TResult"/>.</returns>
		/// <exception cref="HttpRequestException">
		/// The response has <see cref="HttpResponseMessage.IsSuccessStatusCode"/> equal to <see langword="false"/>.
		/// </exception>
		/// <example>
		/// <code>
		/// string url = "some/url"
		/// var request = new
		/// {
		/// 	request = new
		/// 	{
		/// 		dto = new
		/// 		{
		/// 			ArtifactID = 12345
		/// 		}
		/// 	}
		/// };
		///
		/// var applicationFieldCodeId = await _restService.PostAsync&lt;int&gt;(url, request).ConfigureAwait(false);
		/// </code>
		/// </example>
		Task<TResult> PostAsync<TResult>(string relativeUri, object content = null, double timeout = 2, UserCredentials userCredentials = null);

		/// <summary>
		/// Executes a async POST HTTP request without response to the specified <paramref name="relativeUri"/>
		/// with optionally <paramref name="content"/> serialized to JSON.
		/// </summary>
		/// <param name="relativeUri">The endpoint relative URI.</param>
		/// <param name="content">The content object.</param>
		/// <param name="userCredentials">User credentials to be used when perfroming action over Relativity Api.</param>
		/// <exception cref="HttpRequestException">
		/// The response has <see cref="HttpResponseMessage.IsSuccessStatusCode"/> equal to <see langword="false"/>.
		/// </exception>
		/// <returns>>A <see cref="Task"/> representing the asynchronous operation.</returns>
		/// <example>
		/// <code>
		/// string url = "some/url"
		/// var request = new
		/// {
		/// 	request = new
		/// 	{
		/// 		dto = new
		/// 		{
		/// 			ArtifactID = 12345
		/// 		}
		/// 	}
		/// };
		///
		/// await _restService.PostAsync(url, request).ConfigureAwait(false);
		/// </code>
		/// </example>
		Task PostAsync(string relativeUri, object content = null, UserCredentials userCredentials = null);

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
		/// <example>
		/// <code>
		/// LibraryApplicationInstallStatusResponse response;
		/// using (var form = new MultipartFormDataContent())
		/// using (var memory = new StreamContent(new MemoryStream(bytes)))
		/// {
		/// 	var optionsString = JsonConvert.SerializeObject(dto, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
		/// 	using (var optionsContent = new StringContent(optionsString, Encoding.UTF8, "application/json"))
		/// 	{
		/// 		form.Add(optionsContent, "request");
		/// 		form.Add(memory, "rapStream");
		///
		/// 		response = _restService.Put&lt;LibraryApplicationInstallStatusResponse&gt;("Relativity.LibraryApplications/workspace/-1/libraryapplications", form);
		/// 	}
		/// }
		/// </code>
		/// </example>
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
		/// <example>
		/// <code>
		/// int artifactId = 123453;
		/// var dto = new
		/// {
		/// 	groupRequest = new
		/// 	{
		/// 		Client = new
		/// 		{
		/// 			Secured = false,
		/// 			Value = new
		/// 			{
		/// 				12345
		/// 			}
		/// 		},
		/// 	}
		/// };
		///
		/// _restService.Put($"relativity.groups/workspace/-1/groups/artifactId", dto);
		/// </code>
		/// </example>
		void Put(string relativeUri, object content = null, UserCredentials userCredentials = null);

		/// <summary>
		/// Executes a async PUT HTTP request to the specified <paramref name="relativeUri"/>
		/// with optionally <paramref name="content"/> serialized to JSON,
		/// then deserializes the response content to <typeparamref name="TResult"/> and returns it.
		/// </summary>
		/// <typeparam name="TResult">The type of the result to deserialize response content to.</typeparam>
		/// <param name="relativeUri">The endpoint relative URI.</param>
		/// <param name="content">The content object.</param>
		/// <param name="userCredentials">User credentials to be used when perfroming action over Relativity Api.</param>
		/// <returns>The task with response content deserialized to <typeparamref name="TResult"/>.</returns>
		/// <exception cref="HttpRequestException">
		/// The response has <see cref="HttpResponseMessage.IsSuccessStatusCode"/> equal to <see langword="false"/>.
		/// </exception>
		/// <example>
		/// <code>
		/// LibraryApplicationInstallStatusResponse response;
		/// using (var form = new MultipartFormDataContent())
		/// using (var memory = new StreamContent(new MemoryStream(bytes)))
		/// {
		/// 	var optionsString = JsonConvert.SerializeObject(dto, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
		/// 	using (var optionsContent = new StringContent(optionsString, Encoding.UTF8, "application/json"))
		/// 	{
		/// 		form.Add(optionsContent, "request");
		/// 		form.Add(memory, "rapStream");
		///
		/// 		response = await _restService.Put&lt;LibraryApplicationInstallStatusResponse&gt;("Relativity.LibraryApplications/workspace/-1/libraryapplications", form).ConfigureAwait(false);
		/// 	}
		/// }
		/// </code>
		/// </example>
		Task<TResult> PutAsync<TResult>(string relativeUri, object content = null, UserCredentials userCredentials = null);

		/// <summary>
		/// Executes a async PUT HTTP request without response to the specified <paramref name="relativeUri"/>
		/// with optionally <paramref name="content"/> serialized to JSON.
		/// </summary>
		/// <param name="relativeUri">The endpoint relative URI.</param>
		/// <param name="content">The content object.</param>
		/// <param name="userCredentials">User credentials to be used when perfroming action over Relativity Api.</param>
		/// <exception cref="HttpRequestException">
		/// The response has <see cref="HttpResponseMessage.IsSuccessStatusCode"/> equal to <see langword="false"/>.
		/// </exception>
		/// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		/// <example>
		/// <code>
		/// int artifactId = 123453;
		/// var dto = new
		/// {
		/// 	groupRequest = new
		/// 	{
		/// 		Client = new
		/// 		{
		/// 			Secured = false,
		/// 			Value = new
		/// 			{
		/// 				12345
		/// 			}
		/// 		},
		/// 	}
		/// };
		///
		/// await _restService.Put($"relativity.groups/workspace/-1/groups/artifactId", dto).ConfigureAwait(false);
		/// </code>
		/// </example>
		Task PutAsync(string relativeUri, object content = null, UserCredentials userCredentials = null);

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
		/// <example>
		/// <code>
		/// int workspaceId = 12345;
		/// int applicationFieldCodeId = 345;
		/// string url = $"relativity-imaging/v1/workspaces/{workspaceId}/application-field-codes/{applicationFieldCodeId}";
		/// _restService.Delete&lt;bool&gt;(url);
		/// </code>
		/// </example>
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
		/// <example>
		/// <code>
		/// int agentId = 1234;
		/// var dto = new
		/// {
		/// 	force = true
		/// };
		///
		/// _restService.Delete($"relativity.agents/workspace/-1/agents/{agentId}", dto);
		/// </code>
		/// </example>
		void Delete(string relativeUri, object content = null, UserCredentials userCredentials = null);

		/// <summary>
		/// Executes a ascyn DELETE HTTP request to the specified <paramref name="relativeUri"/>
		/// with optionally <paramref name="content"/> serialized to JSON,
		/// then deserializes the response content to <typeparamref name="TResult"/> and returns it.
		/// </summary>
		/// <typeparam name="TResult">The type of the result to deserialize response content to.</typeparam>
		/// <param name="relativeUri">The endpoint relative URI.</param>
		/// <param name="content">The content object.</param>
		/// <param name="userCredentials">User credentials to be used when perfroming action over Relativity Api.</param>
		/// <returns>The task with response content deserialized to <typeparamref name="TResult"/>.</returns>
		/// <exception cref="HttpRequestException">
		/// The response has <see cref="HttpResponseMessage.IsSuccessStatusCode"/> equal to <see langword="false"/>.
		/// </exception>
		/// <example>
		/// <code>
		/// string url = "some/url";
		/// await _restService.DeleteAsync&lt;bool&gt;(url).ConfigureAwait(false);
		/// </code>
		/// </example>
		Task<TResult> DeleteAsync<TResult>(string relativeUri, object content = null, UserCredentials userCredentials = null);

		/// <summary>
		/// Executes a async DELETE HTTP request without response to the specified <paramref name="relativeUri"/>
		/// with optionally <paramref name="content"/> serialized to JSON.
		/// </summary>
		/// <param name="relativeUri">The endpoint relative URI.</param>
		/// <param name="content">The content object.</param>
		/// <param name="userCredentials">User credentials to be used when perfroming action over Relativity Api.</param>
		/// <exception cref="HttpRequestException">
		/// The response has <see cref="HttpResponseMessage.IsSuccessStatusCode"/> equal to <see langword="false"/>.
		/// </exception>
		/// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		/// <example>
		/// <code>
		/// string url = "some/url";
		/// await _restService.DeleteAsync(url).ConfigureAwait(false);
		/// </code>
		/// </example>
		Task DeleteAsync(string relativeUri, object content = null, UserCredentials userCredentials = null);
	}
}
