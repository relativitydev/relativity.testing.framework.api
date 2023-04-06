using System;
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
		private const double _EXCEPTION_TIMEOUT = 0.2;

		private readonly string _timeoutException = $"Imaging Job for Imaging Set with id={_IMAGING_SET_ID} was not completed within the {_EXCEPTION_TIMEOUT} minutes time limit." +
			"Please check the error log in Relativity, or confirm that the job took longer than expected.";

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
		public void Wait_WhenStatusCompleted_ShouldCallImagingSetStatusGetStrategyOnce()
		{
			_mockImagingSetStatusGetStrategy.Setup(getStatusStrategy => getStatusStrategy.Get(_WORKSPACE_ID, _IMAGING_SET_ID)).Returns(new ImagingSetDetailedStatus { Status = "Completed" });

			_sut.Wait(_WORKSPACE_ID, _IMAGING_SET_ID);

			_mockImagingSetStatusGetStrategy.Verify(getStatusStrategy => getStatusStrategy.Get(_WORKSPACE_ID, _IMAGING_SET_ID), Times.Once);
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

		[Test]
		public void Wait_WhenStatusNotCompleted_ShouldThrowExceptionOnTimeout()
		{
			_mockImagingSetStatusGetStrategy.Setup(getStatusStrategy => getStatusStrategy.Get(_WORKSPACE_ID, _IMAGING_SET_ID)).Returns(new ImagingSetDetailedStatus { Status = "Staging" });

			InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
				_sut.Wait(_WORKSPACE_ID, _IMAGING_SET_ID, _EXCEPTION_TIMEOUT));

			exception.Message.Should().Contain(_timeoutException);
		}

		private void TestIfWaitCallsGetStatusStrategyTwiceWhenItReturnsCompletedStatusOnSecondCall(string secondCallStatusValue)
		{
			_mockImagingSetStatusGetStrategy.SetupSequence(getStatusStrategy => getStatusStrategy.Get(_WORKSPACE_ID, _IMAGING_SET_ID))
				.Returns(new ImagingSetDetailedStatus { Status = "Staging" })
				.Returns(new ImagingSetDetailedStatus { Status = secondCallStatusValue });

			_sut.Wait(_WORKSPACE_ID, _IMAGING_SET_ID);

			_mockImagingSetStatusGetStrategy.Verify(getStatusStrategy => getStatusStrategy.Get(_WORKSPACE_ID, _IMAGING_SET_ID), Times.Exactly(2));
		}
	}
}
