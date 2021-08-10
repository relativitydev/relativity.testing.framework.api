using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;

namespace Relativity.Testing.Framework.Api.Tests.Strategies.Documents
{
    [TestFixture]
    [TestOf(typeof(DocumentImportHelper))]
    public class DocumentImportHelperFixture
    {
        private DocumentImportHelper _documentImportHelper;
        private Mock<IImportApiService> _mockImportApiService;

        [SetUp]
        public void SetUp()
        {
            _mockImportApiService = new Mock<IImportApiService>();

            _documentImportHelper = new DocumentImportHelper(_mockImportApiService.Object);
        }

        [Test]
        public void JobOnComplete_WithJobReportWithFatalException_ShouldThrowJobReportException()
        {
            var jobReport = new JobReport(//with a fatal exception); 
            DocumentImportHelper.JobOnComplete(jobReport);
            //Assert that JobReportException jobReportException = new JobReportException("Import job failed", jobReport.FatalException); is thrown. 
        }
    }
}
