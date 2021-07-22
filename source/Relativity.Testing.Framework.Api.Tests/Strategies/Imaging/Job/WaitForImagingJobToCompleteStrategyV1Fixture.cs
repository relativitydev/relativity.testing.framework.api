using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(WaitForImagingJobToCompleteStrategyV1))]
	public class WaitForImagingJobToCompleteStrategyV1Fixture
	{
		private const int _WORKSPACE_ID = 100000;
		private const int _IMAGING_SET_ID = 100001;
		private const string _INVALID_TIMEOUT_EXCEPTION_MESSAGE = "Timeout must be greater than 0.";

		private WaitForImagingJobToCompleteStrategyV1 _sut;
		private Mock<IImagingSetStatusGetStrategy> _mockImagingSetStatusGetStrategy;
		private Mock<IImagingSetValidatorV1> _mockImagingSetValidator;

		[SetUp]
		public void SetUp()
		{
			_mockImagingSetStatusGetStrategy = new Mock<IImagingSetStatusGetStrategy>();
			_mockImagingSetValidator = new Mock<IImagingSetValidatorV1>();

			_sut = new WaitForImagingJobToCompleteStrategyV1(_mockImagingSetStatusGetStrategy.Object, _mockImagingSetValidator.Object);
		}

		[Test]
		public async Task WaitAsync_WithAnyParameters_ShouldCallImagingSetValidator()
		{
			_mockImagingSetStatusGetStrategy.Setup(getStatusStrategy => getStatusStrategy.GetAsync(_WORKSPACE_ID, _IMAGING_SET_ID)).Returns(Task.FromResult(new ImagingSetDetailedStatus { Status = "Completed" }));

			await _sut.WaitAsync(_WORKSPACE_ID, _IMAGING_SET_ID).ConfigureAwait(false);

			_mockImagingSetValidator.Verify(validator => validator.ValidateIds(_WORKSPACE_ID, _IMAGING_SET_ID), Times.Once);
		}

		[Test]
		public void Wait_WithAnyParameters_ShouldCallImagingSetValidator()
		{
			_mockImagingSetStatusGetStrategy.Setup(getStatusStrategy => getStatusStrategy.Get(_WORKSPACE_ID, _IMAGING_SET_ID)).Returns(new ImagingSetDetailedStatus { Status = "Completed" });

			_sut.Wait(_WORKSPACE_ID, _IMAGING_SET_ID);

			_mockImagingSetValidator.Verify(validator => validator.ValidateIds(_WORKSPACE_ID, _IMAGING_SET_ID), Times.Once);
		}

		[Test]
		public void Wait_WithNegativeTimeout_ShouldThrowArgumentException()
		{
			ArgumentException exception = Assert.Throws<ArgumentException>(() => _sut.Wait(_WORKSPACE_ID, _IMAGING_SET_ID, -1));
			exception.Message.Should().Contain(_INVALID_TIMEOUT_EXCEPTION_MESSAGE);
		}

		[Test]
		public async Task WaitAsync_WhenStatusCompleted_ShouldCallImagingSetStatusGetStrategyOnce()
		{
			_mockImagingSetStatusGetStrategy.Setup(getStatusStrategy => getStatusStrategy.GetAsync(_WORKSPACE_ID, _IMAGING_SET_ID)).Returns(Task.FromResult(new ImagingSetDetailedStatus { Status = "Completed" }));

			await _sut.WaitAsync(_WORKSPACE_ID, _IMAGING_SET_ID).ConfigureAwait(false);

			_mockImagingSetStatusGetStrategy.Verify(validator => validator.GetAsync(_WORKSPACE_ID, _IMAGING_SET_ID), Times.Once);
		}

		[Test]
		public void Wait_WhenStatusCompleted_ShouldCallImagingSetStatusGetStrategyOnce()
		{
			_mockImagingSetStatusGetStrategy.Setup(getStatusStrategy => getStatusStrategy.Get(_WORKSPACE_ID, _IMAGING_SET_ID)).Returns(new ImagingSetDetailedStatus { Status = "Completed" });

			_sut.Wait(_WORKSPACE_ID, _IMAGING_SET_ID);

			_mockImagingSetStatusGetStrategy.Verify(validator => validator.Get(_WORKSPACE_ID, _IMAGING_SET_ID), Times.Once);
		}

		[Test]
		public async Task WaitAsync_WaitsUntilStatusIsCompleted()
		{
			await TestIfWaitCallsGetStatusStrategyTwiceWhenItReturnsCompletedStatusOnSecondCallAsync("Completed").ConfigureAwait(false);
		}

		[Test]
		public async Task WaitAsync_WaitsUntilStatusIsCompletedWithErrors()
		{
			await TestIfWaitCallsGetStatusStrategyTwiceWhenItReturnsCompletedStatusOnSecondCallAsync("Completed with Errors").ConfigureAwait(false);
		}

		private async Task TestIfWaitCallsGetStatusStrategyTwiceWhenItReturnsCompletedStatusOnSecondCallAsync(string secondCallStatusValue)
		{
			_mockImagingSetStatusGetStrategy.SetupSequence(getStatusStrategy => getStatusStrategy.GetAsync(_WORKSPACE_ID, _IMAGING_SET_ID))
				.Returns(Task.FromResult(new ImagingSetDetailedStatus { Status = "Staging" }))
				.Returns(Task.FromResult(new ImagingSetDetailedStatus { Status = secondCallStatusValue }));

			await _sut.WaitAsync(_WORKSPACE_ID, _IMAGING_SET_ID).ConfigureAwait(false);

			_mockImagingSetStatusGetStrategy.Verify(validator => validator.GetAsync(_WORKSPACE_ID, _IMAGING_SET_ID), Times.Exactly(2));
		}

		[Test]
		public void Wait_WaitsUntilStatusIsCompleted()
		{
			TestIfWaitCallsGetStatusStrategyTwiceWhenItReturnsCompletedStatusOnSecondCall("Completed");
		}

		[Test]
		public void Wait_WaitsUntilStatusIsCompletedWithErrors()
		{
			TestIfWaitCallsGetStatusStrategyTwiceWhenItReturnsCompletedStatusOnSecondCall("Completed with Errors");
		}

		private void TestIfWaitCallsGetStatusStrategyTwiceWhenItReturnsCompletedStatusOnSecondCall(string secondCallStatusValue)
		{
			_mockImagingSetStatusGetStrategy.SetupSequence(getStatusStrategy => getStatusStrategy.Get(_WORKSPACE_ID, _IMAGING_SET_ID))
				.Returns(new ImagingSetDetailedStatus { Status = "Staging" })
				.Returns(new ImagingSetDetailedStatus { Status = secondCallStatusValue });

			_sut.Wait(_WORKSPACE_ID, _IMAGING_SET_ID);

			_mockImagingSetStatusGetStrategy.Verify(validator => validator.Get(_WORKSPACE_ID, _IMAGING_SET_ID), Times.Exactly(2));
		}

		[Test]
		public void Wait_WhenStatusNotCompleted_ShouldThrowExceptionOnTimeout()
		{
			_mockImagingSetStatusGetStrategy.Setup(getStatusStrategy => getStatusStrategy.Get(_WORKSPACE_ID, _IMAGING_SET_ID)).Returns(new ImagingSetDetailedStatus { Status = "Staging" });

			_sut.Wait(_WORKSPACE_ID, _IMAGING_SET_ID, 0.2);

			_mockImagingSetStatusGetStrategy.Verify(validator => validator.Get(_WORKSPACE_ID, _IMAGING_SET_ID), Times.Once);
		}
	}
}
