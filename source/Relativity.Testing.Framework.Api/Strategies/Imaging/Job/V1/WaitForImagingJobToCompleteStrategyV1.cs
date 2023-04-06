using System;
using System.Diagnostics;
using System.Threading;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class WaitForImagingJobToCompleteStrategyV1 : IWaitForImagingJobToCompleteStrategy
	{
		private readonly IImagingSetStatusGetStrategy _imagingSetStatusGetStrategy;
		private readonly IImagingSetValidatorV1 _imagingSetValidator;
		private readonly int _waitTime = 1000;

		public WaitForImagingJobToCompleteStrategyV1(
			IImagingSetStatusGetStrategy imagingSetStatusGetStrategy,
			IImagingSetValidatorV1 imagingSetValidator)
		{
			_imagingSetStatusGetStrategy = imagingSetStatusGetStrategy;
			_imagingSetValidator = imagingSetValidator;
		}

		public void Wait(int workspaceId, int imagingSetId, double timeout = 2.0)
		{
			ValidateTimeoutValue(timeout);
			_imagingSetValidator.ValidateIds(workspaceId, imagingSetId);

			var changeStatusTimeout = TimeSpan.FromMinutes(timeout);
			bool completed = false;
			var watch = Stopwatch.StartNew();

			while (!completed)
			{
				CheckTimeoutAndThrowExceptionIfReached(imagingSetId, changeStatusTimeout, watch, timeout);

				string status = _imagingSetStatusGetStrategy.Get(workspaceId, imagingSetId).Status;
				completed = CheckIfJobIsCompleted(status);
				if (!completed)
				{
					Thread.Sleep(_waitTime);
				}
			}
		}

		private void ValidateTimeoutValue(double timeout)
		{
			if (timeout <= 0)
			{
				throw new ArgumentException("Timeout must be greater than 0.");
			}
		}

		private static void CheckTimeoutAndThrowExceptionIfReached(int imagingSetId, TimeSpan changeStatusTimeout, Stopwatch watch, double timeout)
		{
			if (watch.Elapsed > changeStatusTimeout)
			{
				throw new InvalidOperationException(
					$"Imaging Job for Imaging Set with id={imagingSetId} was not completed within the {timeout} minutes time limit." +
					"Please check the error log in Relativity, or confirm that the job took longer than expected.");
			}
		}

		private static bool CheckIfJobIsCompleted(string status)
		{
			return status.Equals("Completed") || status.Equals("Completed with Errors");
		}
	}
}
