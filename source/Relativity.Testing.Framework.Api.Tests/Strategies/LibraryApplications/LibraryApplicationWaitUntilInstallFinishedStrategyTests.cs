using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using static Relativity.Testing.Framework.Api.Strategies.LibraryApplicationInstallStatusResponse;

namespace Relativity.Testing.Framework.Api.Tests.Strategies.LibraryApplications
{
	[TestFixture]
	public class LibraryApplicationWaitUntilInstallFinishedStrategyTests
	{
		private Mock<IRestService> _mockRestService;

		private LibraryApplicationWaitUntilInstallFinishedStrategy _unit;

		[SetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();

			_unit = new LibraryApplicationWaitUntilInstallFinishedStrategy(_mockRestService.Object);
		}

		[TestCase(true, TestName = "Null ValidationMessages; Expected exception message")]
		[TestCase(false, TestName = "Empty ValidationMessages; Expected exception message")]
		public void NoValidationMessageOnResponse_DoNotThrowAnException(bool actuallyNull)
		{
			List<string> testMessages = actuallyNull ? null : new List<string>();

			var noMessagesResponse = new LibraryApplicationInstallStatusResponse
			{
				InstallStatus = new InstallStatusModel
				{
					Code = RelativityApplicationInstallStatusCode.Failed
				},
				ValidationMessages = testMessages
			};
			_mockRestService.Setup(
				x => x.Get<LibraryApplicationInstallStatusResponse>(It.IsAny<string>(), null))
					.Returns(noMessagesResponse);

			var exceptionMessage = Assert.Throws<Exception>(() => _unit.WaitUntilInstallFinished(-1, -1)).Message;

			Assert.That(exceptionMessage, Does.Contain("Relativity ADS did not return any validation messages."));
		}

		[Test]
		public void MultipleValidationMessageOnResponse_AllIncludedInException()
		{
			string shouldAppearInUserException1 = "foo";
			string shouldAppearInUserException2 = "bar";
			var testMessages = new List<string> { shouldAppearInUserException1, shouldAppearInUserException2 };

			var messagesResponse = new LibraryApplicationInstallStatusResponse
			{
				InstallStatus = new InstallStatusModel
				{
					Code = RelativityApplicationInstallStatusCode.Failed
				},
				ValidationMessages = testMessages
			};
			_mockRestService.Setup(
				x => x.Get<LibraryApplicationInstallStatusResponse>(It.IsAny<string>(), null))
					.Returns(messagesResponse);

			var exceptionMessage = Assert.Throws<Exception>(() => _unit.WaitUntilInstallFinished(-1, -1)).Message;

			Assert.That(exceptionMessage, Does.Contain(shouldAppearInUserException1));
			Assert.That(exceptionMessage, Does.Contain(shouldAppearInUserException2));
		}
	}
}
