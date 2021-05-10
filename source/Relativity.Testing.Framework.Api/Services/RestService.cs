using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Configuration;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class RestService : HttpService, IRestService
	{
		public RestService(IConfigurationService configurationService)
			: base(
				  $"{configurationService.RelativityInstance.ServerBindingType}://{configurationService.RelativityInstance.RestServicesHostAddress}/Relativity.REST/api/",
				  configurationService.RelativityInstance.AdminUsername,
				  configurationService.RelativityInstance.AdminPassword)
		{
		}

		protected override string BuildResponseFailureMessage(HttpResponseMessage response)
		{
			string message = base.BuildResponseFailureMessage(response);

			if (ShouldTryToExtractConcreteErrorMessage(response)
				&& TryExtractConcreteErrorMessage(response, out string concreteMessage))
			{
				return new StringBuilder().
					AppendLine(concreteMessage).
					AppendLine().
					AppendLine("Details:").
					Append(message).
					ToString();
			}

			return message;
		}

		private static bool ShouldTryToExtractConcreteErrorMessage(HttpResponseMessage response)
		{
			return response.StatusCode == HttpStatusCode.BadRequest;
		}

		private static bool TryExtractConcreteErrorMessage(HttpResponseMessage response, out string message)
		{
			string contentText = ExtractContentTextSafely(response);

			if (!string.IsNullOrEmpty(contentText))
			{
				var json = JObject.Parse(contentText);
				if (json.TryGetValue("Message", out JToken jToken))
				{
					message = (string)jToken;
					return true;
				}
			}

			message = null;
			return false;
		}
	}
}
