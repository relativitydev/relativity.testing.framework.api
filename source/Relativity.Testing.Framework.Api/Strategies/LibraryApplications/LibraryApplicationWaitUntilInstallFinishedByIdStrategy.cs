using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class LibraryApplicationWaitUntilInstallFinishedByIdStrategy : ILibraryApplicationWaitUntilInstallFinishedByIdStrategy
	{
		private readonly IRestService _restService;

		public LibraryApplicationWaitUntilInstallFinishedByIdStrategy(IRestService restService)
		{
			_restService = restService;
		}

		public void WaitUntilInstallFinished(int id)
		{
			Stopwatch watch = new Stopwatch();
			bool keepPolling = true;
			watch.Start();
			LibraryApplicationInstallStatusDto statusCodeRespose = null;

			while (keepPolling)
			{
				statusCodeRespose = GetStatus(id);

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
						var exeptionMessage = statusCodeRespose.ValidationMessages != null && statusCodeRespose.ValidationMessages.Any()
							? $"Application failed to install. Exception Message: {string.Join("\n", statusCodeRespose.ValidationMessages)}"
							: $"Application failed to install.";
						throw new Exception(exeptionMessage);
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

		private LibraryApplicationInstallStatusDto GetStatus(int id)
		{
			return _restService.Get<LibraryApplicationInstallStatusDto>($"relativity-environment/v1/workspace/-1/libraryapplications/{id}/libraryinstall");
		}
	}
}
