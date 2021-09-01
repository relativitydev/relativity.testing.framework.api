using System;
using System.Diagnostics;
using System.Threading;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class LibraryApplicationWaitUntilInstallFinishedStrategy : ILibraryApplicationWaitUntilInstallFinishedStrategy
	{
		private readonly IRestService _restService;

		public LibraryApplicationWaitUntilInstallFinishedStrategy(IRestService restService)
		{
			_restService = restService;
		}

		public void WaitUntilInstallFinished(int applicationId, int applicationInstallId)
		{
			Stopwatch watch = new Stopwatch();
			bool keepPolling = true;
			watch.Start();
			LibraryApplicationInstallStatusResponse statusCodeRespose = null;

			while (keepPolling)
			{
				statusCodeRespose = GetStatus(applicationId, applicationInstallId);

				switch (statusCodeRespose.InstallStatus.Code)
				{
					case RelativityApplicationInstallStatusCode.Unknown:
						// Could represent a variety of statuses from the underlying Data enum (NotInstalled, OutOfDate, Modified)
						break;
					case RelativityApplicationInstallStatusCode.Pending:
						break;
					case RelativityApplicationInstallStatusCode.InProgress:
						Console.WriteLine("Application install is InProgress.");
						break;
					case RelativityApplicationInstallStatusCode.Failed:
						string exceptionMsg = (statusCodeRespose.ValidationMessages == null || statusCodeRespose.ValidationMessages.Count == 0)
							? "Application failed to install. Relativity ADS did not return any validation messages. Relativity logs and errors may contain more information."
							: $"Application failed to install. Exception Message: {string.Join("\n", statusCodeRespose.ValidationMessages)}";
						throw new Exception(exceptionMsg);
					case RelativityApplicationInstallStatusCode.Completed:
					case RelativityApplicationInstallStatusCode.Canceled:
						keepPolling = false;
						break;
					default:
						break;
				}

				if (keepPolling && watch.Elapsed.TotalMinutes > 10 && statusCodeRespose.InstallStatus.Code != RelativityApplicationInstallStatusCode.InProgress)
				{
					// TODO: Query this automatically for the user so that don't have to fish.
					throw new Exception($"Install status was not achieved in the 10 minute window. Check RelativityLogs or Errors for more information.");
				}

				if (keepPolling)
				{
					Thread.Sleep(1000);
				}
			}
		}

		private LibraryApplicationInstallStatusResponse GetStatus(int applicationId, int applicationInstallId)
		{
			return _restService.Get<LibraryApplicationInstallStatusResponse>($"Relativity.LibraryApplications/workspace/-1/libraryapplications/{applicationId}/install/{applicationInstallId}");
		}
	}
}
