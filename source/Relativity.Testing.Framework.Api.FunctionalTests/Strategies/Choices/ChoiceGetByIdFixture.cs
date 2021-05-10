using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ChoiceGetByIdStrategy))]
	internal class ChoiceGetByIdFixture : ApiServiceTestFixture<IGetWorkspaceEntityByIdStrategy<Choice>>
	{
		public ChoiceGetByIdFixture()
		{
		}

		public ChoiceGetByIdFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[Test]
		public void Get_Missing()
		{
			var result = Sut.Get(-1, int.MaxValue);

			result.Should().BeNull();
		}

		[Test]
		public void Get_Existing()
		{
			Choice expectedEntity = null;

			Arrange(() =>
			{
				expectedEntity = Facade.Resolve<IChoiceGetAllByObjectFieldStrategy>().
					GetAll(-1, "Client", "Client Status").First();
			});

			var result = Sut.Get(-1, expectedEntity.ArtifactID);

			result.Should().BeEquivalentTo(expectedEntity);
		}
	}
}
