using System.Threading.Tasks;
using Moq;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Api.Validators;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Tests.Strategies.Imaging.Job
{
	[TestFixture]
	[TestOf(typeof(ImagingJobSubmitSingleDocumentStrategyV1))]
	public class ImagingJobSubmitSingleDocumentStrategyV1Fixture
	{
		private const int _WORKSPACE_ID = 100000;
		private const int _DOCUMENT_ID = 100001;

		private readonly SingleDocumentImagingJobRequest _singleDocumentImagingJobRequest = new SingleDocumentImagingJobRequest();

		private ImagingJobSubmitSingleDocumentStrategyV1 _sut;
		private Mock<IRestService> _mockRestService;
		private Mock<IArtifactIdValidator> _mockArtifactIdValidator;
		private Mock<IWorkspaceIdValidator> _mockWorkspaceIdValidator;

		[SetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();

			_mockArtifactIdValidator = new Mock<IArtifactIdValidator>();
			_mockWorkspaceIdValidator = new Mock<IWorkspaceIdValidator>();

			_mockRestService
				.Setup(restService => restService.Post<JObject>(It.IsAny<string>(), It.IsAny<object>(), 2, null))
				.Returns(new JObject {	{ "ImagingJobID", "2" } });

			_mockRestService
				.Setup(restService => restService.PostAsync<JObject>(It.IsAny<string>(), It.IsAny<object>(), 2, null))
				.Returns(Task.FromResult(new JObject { { "ImagingJobID", "2" } }));

			_sut = new ImagingJobSubmitSingleDocumentStrategyV1(_mockRestService.Object, _mockArtifactIdValidator.Object, _mockWorkspaceIdValidator.Object);
		}

		[Test]
		public void Run_WithAnyParameters_ShouldCallValidators()
		{
			_sut.SubmitSingleDocument(_WORKSPACE_ID, _DOCUMENT_ID, _singleDocumentImagingJobRequest);
			_mockArtifactIdValidator.Verify(x => x.Validate(_DOCUMENT_ID, "Document"), Times.Once);
			_mockWorkspaceIdValidator.Verify(x => x.Validate(_WORKSPACE_ID), Times.Once);
		}

		[Test]
		public async Task RunAsync_WithAnyParameters_ShouldCallValidators()
		{
			await _sut.SubmitSingleDocumentAsync(_WORKSPACE_ID, _DOCUMENT_ID, _singleDocumentImagingJobRequest).ConfigureAwait(false);
			_mockArtifactIdValidator.Verify(x => x.Validate(_DOCUMENT_ID, "Document"), Times.Once);
			_mockWorkspaceIdValidator.Verify(x => x.Validate(_WORKSPACE_ID), Times.Once);
		}
	}
}
