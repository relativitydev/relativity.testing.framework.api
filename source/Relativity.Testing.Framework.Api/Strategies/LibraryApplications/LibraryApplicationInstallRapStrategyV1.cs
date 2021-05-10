using System.IO;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Relativity.Testing.Framework.Api.Attributes;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[DoNotRetry]
	[VersionRange(">=12.1")]
	internal class LibraryApplicationInstallRapStrategyV1 : ILibraryApplicationInstallRapStrategy
	{
		private readonly IRestService _restService;
		private readonly ILibraryApplicationWaitUntilInstallFinishedByIdStrategy _libraryApplicationWaitUntilInstallFinishedByGuidStrategy;

		public LibraryApplicationInstallRapStrategyV1(
			IRestService restService,
			ILibraryApplicationWaitUntilInstallFinishedByIdStrategy libraryApplicationWaitUntilInstallFinishedByGuidStrategy)
		{
			_restService = restService;
			_libraryApplicationWaitUntilInstallFinishedByGuidStrategy = libraryApplicationWaitUntilInstallFinishedByGuidStrategy;
		}

		public int InstallToLibrary(string pathToRap, LibraryApplicationInstallOptions options = null)
		{
			if (!File.Exists(pathToRap))
			{
				throw new FileNotFoundException($"File $'{pathToRap}' does not exist on disk.", pathToRap);
			}

			if (options == null)
			{
				options = new LibraryApplicationInstallOptions();
			}

			byte[] bytes = File.ReadAllBytes(pathToRap);

			var dto = new
			{
				request = options
			};

			LibraryApplicationInstallStatusResponse response;

			using (var form = new MultipartFormDataContent())
			using (var memory = new StreamContent(new MemoryStream(bytes)))
			{
				var optionsString = JsonConvert.SerializeObject(dto, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
				using (var optionsContent = new StringContent(optionsString, Encoding.UTF8, "application/json"))
				{
					form.Add(optionsContent, "request");
					form.Add(memory, "rapStream");

					response = _restService.Put<LibraryApplicationInstallStatusResponse>("relativity-environment/v1/workspace/-1/libraryapplications", form);
				}
			}

			if (response.InstallStatus.Code != RelativityApplicationInstallStatusCode.Completed)
			{
				_libraryApplicationWaitUntilInstallFinishedByGuidStrategy.WaitUntilInstallFinished(response.ApplicationIdentifier.ArtifactID);
			}

			return response.ApplicationIdentifier.ArtifactID;
		}
	}
}
