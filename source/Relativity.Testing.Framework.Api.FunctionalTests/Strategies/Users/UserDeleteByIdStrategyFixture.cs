using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IDeleteByIdStrategy<User>))]
	public class UserDeleteByIdStrategyFixture : ApiTestFixture
	{
		private IDeleteByIdStrategy<User> _sut;

		public UserDeleteByIdStrategyFixture()
		{
		}

		public UserDeleteByIdStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[SetUp]
		public void SetUp()
		{
			_sut = Facade.Resolve<IDeleteByIdStrategy<User>>();
		}

		[Test]
		public void Delete_Missing()
		{
			int id = 9_999_999;

			var exception = Assert.Throws<ObjectNotFoundException>(() =>
				_sut.Delete(id));

			exception.Message.Should().StartWith($"Failed to find User entity by {id} ID.");
		}

		[Test]
		public void Delete_Existing()
		{
			User toDelete = null;

			Arrange(x => x.Create(out toDelete));

			_sut.Delete(toDelete.ArtifactID);

			Facade.Resolve<IUserGetByEmailStrategy>().Get(toDelete.EmailAddress).
				Should().BeNull();
		}
	}
}
