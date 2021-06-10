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
		private Mock<IUserGetByEmailStrategy> _mockUserGetByEmailStrategy;
		private Mock<IGetByIdStrategy<User>> _mockGetByIdStrategy;
		private IWaitUserDeletedStrategy _waitUserDeleted;

		[OneTimeSetUp]
		public void SetUp()
		{
			_mockUserGetByEmailStrategy = new Mock<IUserGetByEmailStrategy>();
			_mockGetByIdStrategy = new Mock<IGetByIdStrategy<User>>();
			_waitUserDeleted = new WaitUserDeletedStrategy(_mockUserGetByEmailStrategy.Object, _mockGetByIdStrategy.Object, TimeSpan.FromSeconds(1));
		}

		[Test]
		public void Wait_ByArtifactID_PollsUntilUserDoesNotExist()
		{
			int userID = 12345;

			_mockGetByIdStrategy.SetupSequence(x => x.Get(userID))
				.Returns(new User())
				.Returns(null);

			_waitUserDeleted.Wait(userID);
			_mockGetByIdStrategy.Verify(mock => mock.Get(userID), Times.Exactly(2));
		}

		[Test]
		public void Wait_ByEmail_PollsUntilUserDoesNotExist()
		{
			string email = "AUser@test.com";

			_mockUserGetByEmailStrategy.SetupSequence(x => x.Get(email))
				.Returns(new User())
				.Returns(null);

			_waitUserDeleted.Wait(email);
			_mockUserGetByEmailStrategy.Verify(mock => mock.Get(email), Times.Exactly(2));
		}

		[Test]
		public void Wait_ByArtifactID_IfTakesTooLong_ShouldTimeoutAndThrowException()
		{
			_mockGetByIdStrategy.Setup(x => x.Get(It.IsAny<int>())).Returns(new User());
			Assert.Throws<InvalidOperationException>(() => _waitUserDeleted.Wait(1));
		}

		[Test]
		public void Wait_ByEmail_IfTakesTooLong_ShouldTimeoutAndThrowException()
		{
			_mockUserGetByEmailStrategy.Setup(x => x.Get(It.IsAny<string>())).Returns(new User());
			Assert.Throws<InvalidOperationException>(() => _waitUserDeleted.Wait("a"));
		}
	}
}
