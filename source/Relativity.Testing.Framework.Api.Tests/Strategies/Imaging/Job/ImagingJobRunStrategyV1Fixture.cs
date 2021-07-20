using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(ImagingJobRunStrategyV1))]
	public class ImagingJobRunStrategyV1Fixture
	{
		private const int _WORKSPACE_ID = 100000;
		private const int _IMAGING_SET_ID = 100001;

		private readonly string _runUrl = $"relativity-imaging/v1/workspaces/{_WORKSPACE_ID}/imaging-sets/{_IMAGING_SET_ID}/run";

		private ImagingJobRunStrategyV1 _sut;
		private Mock<IRestService> _mockRestService;
		private Mock<IImagingSetValidatorV1> _mockImagingSetValidator;

		[SetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();
			_mockRestService
				.Setup(restService => restService.PostAsync<ImagingJobIdResponseDtoV1>(_runUrl, It.IsAny<ImagingJobRequestDtoV1>(), 2, null))
				.Returns(Task.FromResult(new ImagingJobIdResponseDtoV1()));
			_mockRestService
				.Setup(restService => restService.Post<ImagingJobIdResponseDtoV1>(_runUrl, It.IsAny<ImagingJobRequestDtoV1>(), 2, null))
				.Returns(new ImagingJobIdResponseDtoV1());
			_mockImagingSetValidator = new Mock<IImagingSetValidatorV1>();

			_sut = new ImagingJobRunStrategyV1(_mockRestService.Object, _mockImagingSetValidator.Object);
		}

		[Test]
		public void Run_WithAnyParameters_ShouldCallImagingSetValidator()
		{
			_sut.Run(_WORKSPACE_ID, _IMAGING_SET_ID);
			_mockImagingSetValidator.Verify(validator => validator.ValidateIds(_WORKSPACE_ID, _IMAGING_SET_ID), Times.Once);
		}

		[Test]
		public async Task RunAsync_WithAnyParameters_ShouldCallImagingSetValidator()
		{
			await _sut.RunAsync(_WORKSPACE_ID, _IMAGING_SET_ID).ConfigureAwait(false);
			_mockImagingSetValidator.Verify(validator => validator.ValidateIds(_WORKSPACE_ID, _IMAGING_SET_ID), Times.Once);
		}

		[Test]
		public void Run_ShouldCallIRestService()
		{
			_sut.Run(_WORKSPACE_ID, _IMAGING_SET_ID);
			_mockRestService.Verify(restService => restService.Post<ImagingJobIdResponseDtoV1>(_runUrl, It.IsAny<ImagingJobRequestDtoV1>(), 2, null), Times.Once);
		}

		[Test]
		public async Task RunAsync_ShouldCallIRestService()
		{
			await _sut.RunAsync(_WORKSPACE_ID, _IMAGING_SET_ID).ConfigureAwait(false);
			_mockRestService.Verify(restService => restService.PostAsync<ImagingJobIdResponseDtoV1>(_runUrl, It.IsAny<ImagingJobRequestDtoV1>(), 2, null), Times.Once);
		}
	}
}
