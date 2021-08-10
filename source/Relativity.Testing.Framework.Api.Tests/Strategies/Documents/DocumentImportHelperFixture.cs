using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Exceptions;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Api.Tests.Strategies.Documents.ImportApi;

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
            JobReport jobReport = FakeJobImport.Get(fatalException: true);
            Assert.Throws<JobReportException>(() => DocumentImportHelper.JobOnComplete(jobReport));
        }
    }
}
