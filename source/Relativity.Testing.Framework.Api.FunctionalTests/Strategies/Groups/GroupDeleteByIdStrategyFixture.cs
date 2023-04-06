using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IDeleteByIdStrategy<Group>))]
	public class GroupDeleteByIdStrategyFixture : ApiTestFixture
	{
		private IDeleteByIdStrategy<Group> _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = Facade.Resolve<IDeleteByIdStrategy<Group>>();
		}

		[Test]
		public void Delete_Missing()
		{
			int id = 9_999_999;

			var exception = Assert.Throws<ObjectNotFoundException>(() =>
				_sut.Delete(id));

			exception.Message.Should().StartWith($"Failed to find Group entity by {id} ID.");
		}

		[Test]
		public void Delete_Existing()
		{
			Group toDelete = null;

			Arrange(x => x.Create(out toDelete));

			_sut.Delete(toDelete.ArtifactID);

			Facade.Resolve<IGetAllByNamesStrategy<Group>>().GetAll(new List<string> { toDelete.Name }).
				Should().BeEmpty();
		}
	}
}
