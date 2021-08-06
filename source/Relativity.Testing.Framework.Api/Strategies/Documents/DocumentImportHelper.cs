using System;
using System.Collections.Generic;
using System.Linq;
using kCura.Relativity.DataReaderClient;
using Relativity.Testing.Framework.Api.Exceptions;
using Relativity.Testing.Framework.Api.Services;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class DocumentImportHelper
	{
		public DocumentImportHelper(IImportApiService importApiService)
		{
			ImportApiService = importApiService;
		}

		protected IImportApiService ImportApiService { get; private set; }

		protected static void SubscribeToImportJob(IImportNotifier importApiJob)
		{
			importApiJob.OnComplete += JobOnComplete;
			importApiJob.OnFatalException += JobOnFatalException;
		}

		protected static void UnsubscribeFromImportJob(IImportNotifier importApiJob)
		{
			importApiJob.OnComplete -= JobOnComplete;
			importApiJob.OnFatalException -= JobOnFatalException;
		}

		protected static void JobOnComplete(JobReport jobReport)
		{
			if (jobReport.FatalException != null)
			{
				throw jobReport.FatalException;
			}

			if (jobReport.ErrorRowCount > 0)
			{
				IEnumerable<string> errors = jobReport.ErrorRows.Select(x => $"{x.Identifier} - {x.Message}");
				throw new Exception($"Import API job completed with errors:{string.Join("\n", errors)}");
			}
		}

		protected static void JobOnFatalException(JobReport jobReport)
		{
			JobReportException jobReportException = new JobReportException("JobReportException", jobReport.FatalException);
			throw jobReportException;
		}
	}
}
