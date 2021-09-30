using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ICreateStrategy<Group>))]
	public class GroupCreateStrategyFixture : ApiTestFixture
	{
		private ICreateStrategy<Group> _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = Facade.Resolve<ICreateStrategy<Group>>();
		}

		[Test]
		public void Create_WithEmptyEntity()
		{
			Group result = _sut.Create(new Group());

			result.ArtifactID.Should().BePositive();
			result.Name.Should().NotBeNullOrEmpty();
			result.Client.Should().NotBeNull();
		}

		[Test]
		public void Create_WithFilledEntity()
		{
			Client client = null;

			Arrange(x => x.Create(out client));

			var entity = new Group
			{
				Name = Randomizer.GetString("AT_"),
				Client = client
			};

			Group result = _sut.Create(entity.Copy());

			result.ArtifactID.Should().BePositive();
			result.Should().BeEquivalentTo(
				entity,
				o => o.Excluding(group => group.ArtifactID)
					.Excluding(group => group.Client.Number)
					.Excluding(group => group.Client.Status.ArtifactID)
					.Excluding(group => group.Type)
					.Excluding(group => group.Actions)
					.Excluding(group => group.Guids)
					.Excluding(group => group.Meta)
					.Excluding(group => group.CreatedOn)
					.Excluding(group => group.CreatedBy)
					.Excluding(group => group.LastModifiedOn)
					.Excluding(group => group.LastModifiedBy));
		}
	}
}
