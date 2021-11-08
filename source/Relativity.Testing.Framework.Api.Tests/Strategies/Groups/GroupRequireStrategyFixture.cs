using System;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(GroupRequireStrategy))]
	internal class GroupRequireStrategyFixture
	{
		private readonly Group _groupWithArtifactID = new Group
		{
			ArtifactID = 12345
		};

		private Mock<ICreateStrategy<Group>> _mockCreateStrategy;
		private Mock<IGroupUpdateStrategy> _mockGroupUpdateStrategy;
		private Mock<IGetByNameStrategy<Group>> _mockGetByNameStrategy;

		private GroupRequireStrategy _sut;

		[SetUp]
		public void SetUp()
		{
			_mockCreateStrategy = new Mock<ICreateStrategy<Group>>();
			_mockGroupUpdateStrategy = new Mock<IGroupUpdateStrategy>();
			_mockGetByNameStrategy = new Mock<IGetByNameStrategy<Group>>();

			_sut = new GroupRequireStrategy(_mockCreateStrategy.Object, _mockGroupUpdateStrategy.Object, _mockGetByNameStrategy.Object);
		}

		[Test]
		public void Require_WithNull_ThrowsArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => _sut.Require(null));
		}

		[Test]
		public void Require_FoundByArtifactID_OnlyCallsUpdateStrategy()
		{
			_sut.Require(_groupWithArtifactID);

			_mockGroupUpdateStrategy.Verify(groupUpdateService => groupUpdateService.Update(It.IsAny<Group>()), Times.Once);
			_mockGetByNameStrategy.Verify(groupGetStrategy => groupGetStrategy.Get(It.IsAny<string>()), Times.Never);
			_mockCreateStrategy.Verify(groupCreateStrategy => groupCreateStrategy.Create(It.IsAny<Group>()), Times.Never);
		}

		[Test]
		public void Require_FoundByName_OnlyCallsGetAndUpdateStrategies()
		{
			Group groupWithNoArtifactID = new Group
			{
				Name = "Hug"
			};

			_mockGetByNameStrategy.Setup(groupGetStrategy => groupGetStrategy.Get(It.IsAny<string>())).Returns(_groupWithArtifactID);

			_sut.Require(groupWithNoArtifactID);

			_mockGetByNameStrategy.Verify(groupGetStrategy => groupGetStrategy.Get(It.IsAny<string>()), Times.Once);
			_mockGroupUpdateStrategy.Verify(groupUpdateService => groupUpdateService.Update(It.IsAny<Group>()), Times.Once);
			_mockCreateStrategy.Verify(groupCreateStrategy => groupCreateStrategy.Create(It.IsAny<Group>()), Times.Never);
		}

		[Test]
		public void Require_NotFoundByName_OnlyCallsGetAndCreateStrategies()
		{
			Group groupWithNoArtifactID = new Group
			{
				Name = "Hug"
			};

			_mockGetByNameStrategy.Setup(groupGetStrategy => groupGetStrategy.Get(It.IsAny<string>())).Returns((Group)null);

			_sut.Require(groupWithNoArtifactID);

			_mockGetByNameStrategy.Verify(groupGetStrategy => groupGetStrategy.Get(It.IsAny<string>()), Times.Once);
			_mockGroupUpdateStrategy.Verify(groupUpdateService => groupUpdateService.Update(It.IsAny<Group>()), Times.Never);
			_mockCreateStrategy.Verify(groupCreateStrategy => groupCreateStrategy.Create(It.IsAny<Group>()), Times.Once);
		}
	}
}
