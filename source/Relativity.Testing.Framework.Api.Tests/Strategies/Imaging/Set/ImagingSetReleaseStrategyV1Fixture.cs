using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(ImagingSetReleaseStrategyV1))]
	public class ImagingSetReleaseStrategyV1Fixture
	{
		private const int _WORKSPACE_ID = 100000;
		private const int _IMAGING_SET_ID = 100001;

		private readonly string _releaseUrl = $"relativity-imaging/v1/workspaces/{_WORKSPACE_ID}/imaging-sets/{_IMAGING_SET_ID}/release-images";

		private ImagingSetReleaseStrategyV1 _sut;
		private Mock<IRestService> _mockRestService;
		private Mock<IImagingSetValidatorV1> _mockImagingSetValidator;

		[SetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();
			_mockImagingSetValidator = new Mock<IImagingSetValidatorV1>();

			_sut = new ImagingSetReleaseStrategyV1(_mockRestService.Object, _mockImagingSetValidator.Object);
		}

		[Test]
		public void Release_WithAnyParameters_ShouldCallImagingSetValidator()
		{
			_sut.Release(_WORKSPACE_ID, _IMAGING_SET_ID);
			_mockImagingSetValidator.Verify(validator => validator.ValidateIds(_WORKSPACE_ID, _IMAGING_SET_ID), Times.Once);
		}

		[Test]
		public void Release_ShouldCallIRestService()
		{
			_sut.Release(_WORKSPACE_ID, _IMAGING_SET_ID);
			_mockRestService.Verify(restService => restService.Post(_releaseUrl, null, null), Times.Once);
		}
	}
}
