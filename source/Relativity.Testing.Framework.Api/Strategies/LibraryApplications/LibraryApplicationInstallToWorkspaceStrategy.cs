using System;
using System.Net.Http;
using Newtonsoft.Json;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class LibraryApplicationInstallToWorkspaceStrategy : ILibraryApplicationInstallToWorkspaceStrategy
	{
		private readonly IRestService _restService;
		private readonly ILibraryApplicationWaitUntilInstallFinishedStrategy _relativityApplicationWaitUntilInstallFinishedStrategy;

		public LibraryApplicationInstallToWorkspaceStrategy(IRestService restService, ILibraryApplicationWaitUntilInstallFinishedStrategy relativityApplicationWaitUntilInstallFinishedStrategy)
		{
			_restService = restService;
			_relativityApplicationWaitUntilInstallFinishedStrategy = relativityApplicationWaitUntilInstallFinishedStrategy;
		}

		public void InstallToWorkspace(int workspaceId, int applicationId)
		{
			if (applicationId == 0)
			{
				throw new ArgumentException("Parameter can't be 0.", nameof(applicationId));
			}

			object dto = new
			{
				request = new
				{
					WorkspaceIDs = new[] { workspaceId }
				}
			};
			string json = _restService.Post<string>($"Relativity.LibraryApplications/workspace/-1/libraryapplications/{applicationId}/install", dto);

			dynamic jsonResponse = JsonConvert.DeserializeObject(json);
			string installStatusCode = jsonResponse.Results[0].InstallStatus.Code.Value;
			int applicationInstallId = int.Parse(jsonResponse.Results[0].ApplicationInstallID.Value.ToString());

			RelativityApplicationInstallStatusCode statusCode = (RelativityApplicationInstallStatusCode)Enum.Parse(typeof(RelativityApplicationInstallStatusCode), installStatusCode, true);

			if (statusCode == RelativityApplicationInstallStatusCode.Failed || statusCode == RelativityApplicationInstallStatusCode.Canceled)
			{
				var errorMessage = string.Join(Environment.NewLine, jsonResponse.Results[0].Messages);

				throw new HttpRequestException(errorMessage);
			}

			if (statusCode != RelativityApplicationInstallStatusCode.Completed)
			{
				_relativityApplicationWaitUntilInstallFinishedStrategy.WaitUntilInstallFinished(applicationId, applicationInstallId);
			}
		}
	}
}
