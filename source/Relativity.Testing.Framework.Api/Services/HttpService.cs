using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Relativity.Testing.Framework.Api.Attributes;

namespace Relativity.Testing.Framework.Api.Services
{
	[DoNotRetry]
	public class HttpService : IHttpService
	{
		private readonly string _basicAuthorizationParameter;
		private readonly HttpClient _client;

		public HttpService(string baseUrlAddress, string username, string password)
		{
			if (baseUrlAddress is null)
				throw new ArgumentNullException(nameof(baseUrlAddress));

			if (username is null)
				throw new ArgumentNullException(nameof(username));

			if (password is null)
				throw new ArgumentNullException(nameof(password));

			BaseUrl = baseUrlAddress;
			Username = username;

			_basicAuthorizationParameter = GenerateBasicAuthorizationParameter(username, password);
			_client = CreateHttpClient();
		}

		public string BaseUrl { get; }

		public string Username { get; }

		private static string GenerateBasicAuthorizationParameter(string username, string password)
		{
			string unencodedUsernameAndPassword = $"{username}:{password}";

			byte[] unencodedBytes = Encoding.ASCII.GetBytes(unencodedUsernameAndPassword);
			string base64UsernameAndPassword = Convert.ToBase64String(unencodedBytes);

			return $"Basic {base64UsernameAndPassword}";
		}

		public TResult Get<TResult>(string relativeUri)
		{
			return Send<TResult>(HttpMethod.Get, relativeUri);
		}

		public TResult Post<TResult>(string relativeUri, object content = null, double timeout = 2)
		{
			return Send<TResult>(HttpMethod.Post, relativeUri, content, timeout);
		}

		public void Post(string relativeUri, object content = null)
		{
			Post<string>(relativeUri, content);
		}

		public TResult Put<TResult>(string relativeUri, object content = null)
		{
			return Send<TResult>(HttpMethod.Put, relativeUri, content);
		}

		public void Put(string relativeUri, object content = null)
		{
			Put<string>(relativeUri, content);
		}

		public TResult Delete<TResult>(string relativeUri, object content = null)
		{
			return Send<TResult>(HttpMethod.Delete, relativeUri, content);
		}

		public void Delete(string relativeUri, object content = null)
		{
			Delete<string>(relativeUri, content);
		}

		private TResult Send<TResult>(HttpMethod method, string relativeUri, object content = null, double timeout = 2)
		{
			ValidateArguments(method, relativeUri);
			HttpContent httpContent = GetHttpContent(content);

			using (HttpRequestMessage request = new HttpRequestMessage(method, relativeUri) { Content = httpContent })
			using (CancellationTokenSource cancellationToken = new CancellationTokenSource(TimeSpan.FromMinutes(timeout)))
			{
				HttpResponseMessage response = _client.SendAsync(request, cancellationToken.Token).Result;

				if (typeof(TResult) == typeof(HttpResponseMessage))
				{
					return (TResult)(object)response;
				}

				using (response)
				{
					CheckResponseStatus(response);
					return DeserializeContent<TResult>(response);
				}
			}
		}

		private static void ValidateArguments(HttpMethod method, string relativeUri)
		{
			if (method is null)
				throw new ArgumentNullException(nameof(method));

			if (relativeUri is null)
				throw new ArgumentNullException(nameof(relativeUri));
		}

		private static HttpContent GetHttpContent(object content)
		{
			return (content as HttpContent) ?? ConvertToStringContent(content);
		}

		private HttpClient CreateHttpClient()
		{
			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri(BaseUrl);

			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			client.DefaultRequestHeaders.Add("X-CSRF-Header", "-");
			client.DefaultRequestHeaders.Add("Authorization", _basicAuthorizationParameter);
			client.DefaultRequestHeaders.Add("X-Kepler-Version", "2.0");

			client.Timeout = Timeout.InfiniteTimeSpan;

			return client;
		}

		private static HttpContent ConvertToStringContent(object content)
		{
			if (content != null)
			{
				string contentAsString = SerializeToJsonString(content);
				return new StringContent(contentAsString, Encoding.UTF8, "application/json");
			}
			else
			{
				return null;
			}
		}

		private void CheckResponseStatus(HttpResponseMessage response)
		{
			if (!response.IsSuccessStatusCode)
			{
				string message = BuildResponseFailureMessage(response);

				throw new HttpRequestException(message);
			}
		}

		protected virtual string BuildResponseFailureMessage(HttpResponseMessage response)
		{
			StringBuilder builder = new StringBuilder(response.ToString());

			string contentText = ExtractContentTextSafely(response);

			if (!string.IsNullOrWhiteSpace(contentText))
				builder.AppendLine().AppendLine().Append($"Content text: {contentText}");

			return builder.ToString();
		}

		protected static string ExtractContentTextSafely(HttpResponseMessage response)
		{
			try
			{
				return ExtractContentText(response);
			}
			catch
			{
				return null;
			}
		}

		private static string ExtractContentText(HttpResponseMessage response)
		{
			return response.Content.ReadAsStringAsync().Result;
		}

		private static TResult DeserializeContent<TResult>(HttpResponseMessage response)
		{
			string content = ExtractContentText(response);
			return DeserializeFromJsonString<TResult>(content);
		}

		private static string SerializeToJsonString(object content)
		{
			if (content is string stringContent)
				return stringContent;

			return JsonConvert.SerializeObject(content, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
		}

		private static TResult DeserializeFromJsonString<TResult>(string jsonString)
		{
			if (typeof(TResult) == typeof(string))
				return (TResult)(object)jsonString;

			return JsonConvert.DeserializeObject<TResult>(jsonString);
		}
	}
}
