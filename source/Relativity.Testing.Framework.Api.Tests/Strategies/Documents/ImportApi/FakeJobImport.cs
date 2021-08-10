using System;
using System.Linq;
using System.Reflection;

namespace Relativity.Testing.Framework.Api.Tests.Strategies.Documents.ImportApi
{
    public class FakeJobImport : IJobImport
    {
        public static JobReport Get(long maxTransferredItems = long.MaxValue, long numberOfItemLevelErrors = 0, bool fatalException = false)
        {
            ConstructorInfo[] constructorInfos = typeof(JobReport).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
            JobReport jobReport = (JobReport)constructorInfos.First().Invoke(new object[0]);

            if (fatalException)
            {
                var prop = jobReport.GetType().GetProperty("FatalException", BindingFlags.NonPublic | BindingFlags.Instance);
                prop.SetValue(jobReport, new NullReferenceException());
            }

            for (long i = 0; i < numberOfItemLevelErrors && i < maxTransferredItems; i++)
            {
                jobReport.ErrorRows.Add(new JobReport.RowError(i, string.Empty, i.ToString()));
            }

            return jobReport;
        }

        public void RegisterEventHandlers()
        {
            // Method intentionally left empty.
        }

        public void Execute()
        {
            // Method intentionally left empty.
        }
    }
}
