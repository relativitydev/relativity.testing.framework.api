using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(ImagingSetStatusGetStrategyV1))]
	public class ImagingSetStatusGetStrategyV1Fixture
	{
		private const int _WORKSPACE_ID = 100000;
		private const int _IMAGING_SET_ID = 100001;

		private readonly string _getUrl = $"relativity-imaging/v1/workspaces/{_WORKSPACE_ID}/imaging-sets/{_IMAGING_SET_ID}/status";
		private ImagingSetStatusGetStrategyV1 _sut;
		private Mock<IRestService> _mockRestService;
		private Mock<IImagingSetValidatorV1> _imagingSetValidator;

		[SetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();
			_imagingSetValidator = new Mock<IImagingSetValidatorV1>();

			_sut = new ImagingSetStatusGetStrategyV1(_mockRestService.Object, _imagingSetValidator.Object);
		}

		[Test]
		public void Get_WithAnyParameters_ShouldCallImagingSetValidator()
		{
			_sut.Get(_WORKSPACE_ID, _IMAGING_SET_ID);
			_imagingSetValidator.Verify(validator => validator.ValidateIds(_WORKSPACE_ID, _IMAGING_SET_ID), Times.Once);
		}

		[Test]
		public async Task GetAsync_WithAnyParameters_ShouldCallImagingSetValidator()
		{
			await _sut.GetAsync(_WORKSPACE_ID, _IMAGING_SET_ID).ConfigureAwait(false);
			_imagingSetValidator.Verify(validator => validator.ValidateIds(_WORKSPACE_ID, _IMAGING_SET_ID), Times.Once);
		}

		[Test]
		public void Get_ShouldCallIRestService()
		{
			_sut.Get(_WORKSPACE_ID, _IMAGING_SET_ID);
			_mockRestService.Verify(restService => restService.Get<ImagingSetDetailedStatus>(_getUrl, null), Times.Once);
		}

		[Test]
		public async Task GetAsync_ShouldCallIRestService()
		{
			await _sut.GetAsync(_WORKSPACE_ID, _IMAGING_SET_ID).ConfigureAwait(false);
			_mockRestService.Verify(restService => restService.GetAsync<ImagingSetDetailedStatus>(_getUrl, null), Times.Once);
		}
	}
}
