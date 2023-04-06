using System;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(IWaitUserDeletedStrategy))]
	public class WaitUserDeletedStrategyFixture
	{
		private Mock<IExistsByIdStrategy<User>> _mockExistsByIdStrategy;
		private Mock<IUserExistsByEmailStrategy> _mockExistsByEmailStrategy;
		private IWaitUserDeletedStrategy _waitUserDeleted;

		[OneTimeSetUp]
		public void SetUp()
		{
			_mockExistsByIdStrategy = new Mock<IExistsByIdStrategy<User>>();
			_mockExistsByEmailStrategy = new Mock<IUserExistsByEmailStrategy>();
			_waitUserDeleted = new WaitUserDeletedStrategy(_mockExistsByIdStrategy.Object, _mockExistsByEmailStrategy.Object, TimeSpan.FromSeconds(1));
		}

		[Test]
		public void Wait_ByArtifactID_PollsUntilUserDoesNotExist()
		{
			int userID = 12345;

			_mockExistsByIdStrategy.SetupSequence(x => x.Exists(userID))
				.Returns(true)
				.Returns(false);

			_waitUserDeleted.Wait(userID);
			_mockExistsByIdStrategy.Verify(mock => mock.Exists(userID), Times.Exactly(2));
		}

		[Test]
		public void Wait_ByEmail_PollsUntilUserDoesNotExist()
		{
			string email = "AUser@test.com";

			_mockExistsByEmailStrategy.SetupSequence(x => x.Exists(email))
				.Returns(true)
				.Returns(false);

			_waitUserDeleted.Wait(email);
			_mockExistsByEmailStrategy.Verify(mock => mock.Exists(email), Times.Exactly(2));
		}

		[Test]
		public void Wait_ByArtifactID_IfTakesTooLong_ShouldTimeoutAndThrowException()
		{
			_mockExistsByIdStrategy.Setup(x => x.Exists(It.IsAny<int>())).Returns(true);
			Assert.Throws<InvalidOperationException>(() => _waitUserDeleted.Wait(1));
		}

		[Test]
		public void Wait_ByEmail_IfTakesTooLong_ShouldTimeoutAndThrowException()
		{
			_mockExistsByEmailStrategy.Setup(x => x.Exists(It.IsAny<string>())).Returns(true);
			Assert.Throws<InvalidOperationException>(() => _waitUserDeleted.Wait("a"));
		}
	}
}
