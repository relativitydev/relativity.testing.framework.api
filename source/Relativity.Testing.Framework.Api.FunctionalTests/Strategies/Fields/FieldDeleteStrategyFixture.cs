using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(FieldDeleteByIdStrategy))]
	internal class FieldDeleteStrategyFixture : ApiServiceTestFixture<IDeleteWorkspaceEntityByIdStrategy<Field>>
	{
		public FieldDeleteStrategyFixture()
		{
		}

		public FieldDeleteStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[Test]
		public void Delete_Missing()
		{
			Assert.Throws<HttpRequestException>(() =>
				Sut.Delete(-1, int.MaxValue));
		}

		[Test]
		public void Delete_FixedLengthTextField()
		{
			TestDelete<FixedLengthTextField>();
		}

		private void TestDelete<TFieldModel>()
			where TFieldModel : Field, new()
		{
			TFieldModel toDelete = null;

			Arrange(() =>
			{
				toDelete = Facade.Resolve<ICreateWorkspaceEntityStrategy<TFieldModel>>()
					.Create(-1, new TFieldModel());
			});

			Sut.Delete(-1, toDelete.ArtifactID);

			Facade.Resolve<IGetWorkspaceEntityByIdStrategy<TFieldModel>>().Get(-1, toDelete.ArtifactID).
				Should().BeNull();
		}
	}
}
