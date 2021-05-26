using System;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Logging;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Tests.Services
{
	[TestOf(typeof(AccountPoolService))]
	public class AccountPoolServiceFixture
	{
		private readonly Mock<ILogService> _mockLogService = new Mock<ILogService>();
		private readonly Mock<ICreateStrategy<User>> _mockUserCreateStrategy = new Mock<ICreateStrategy<User>>();
		private readonly Mock<IUserExistsByEmailStrategy> _mockUserExistsByEmailStrategy = new Mock<IUserExistsByEmailStrategy>();
		private readonly Mock<IUserGetByEmailStrategy> _mockUserGetByEmailStrategy = new Mock<IUserGetByEmailStrategy>();
		private readonly Mock<IDeleteByIdStrategy<User>> _mockUserDeleteByIdStrategy = new Mock<IDeleteByIdStrategy<User>>();
		private readonly Mock<IGetAllByNamesStrategy<Group>> _mockGroupGetAllByNamesStrategy = new Mock<IGetAllByNamesStrategy<Group>>();
		private readonly Mock<IObjectService> _mockObjectService = new Mock<IObjectService>();

		private AccountPoolService _sut;

		[SetUp]
		public void Setup()
		{
			_sut = new AccountPoolService(
				_mockLogService.Object,
				_mockUserCreateStrategy.Object,
				_mockUserExistsByEmailStrategy.Object,
				_mockUserGetByEmailStrategy.Object,
				_mockUserDeleteByIdStrategy.Object,
				_mockGroupGetAllByNamesStrategy.Object,
				_mockObjectService.Object);
		}

		[Test]
		public void Recreate_WhenUserRemovalTakeTooLong_ShouldTimeoutAndThrowException()
		{
			_mockUserGetByEmailStrategy.Setup(x => x.Get(It.IsAny<string>())).Returns(new User());

			Assert.Throws<InvalidOperationException>(() => _sut.DeleteAndAcquireStandardAccount());
		}

		[Test]
		public void Recreate_WhenUserRemovalTakeLongButInLimit_ShouldDeleteAndCreateNewUser()
		{
			_mockUserGetByEmailStrategy.SetupSequence(x => x.Get(It.IsAny<string>()))
				.Returns(new User())
				.Returns(new User())
				.Returns(new User())
				.Returns(new User())
				.Returns(null);

			Assert.DoesNotThrow(() => _sut.DeleteAndAcquireStandardAccount());
			_mockUserCreateStrategy.Verify(x => x.Create(It.IsAny<User>()), Times.Once);
		}
	}
}
