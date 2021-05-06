using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ICreateStrategy<Error>))]
	internal class ErrorCreateStrategyFixture : ApiServiceTestFixture<ICreateStrategy<Error>>
	{
		public ErrorCreateStrategyFixture()
		{
		}

		public ErrorCreateStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[Test]
		public void Create_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Create(null));
		}

		[Test]
		public void Create_WithFilledEntity()
		{
			var entity = new Error { Message = "Test message" };

			var result = Sut.Create(entity.Copy());

			result.ArtifactID.Should().BePositive();
			result.Should().BeEquivalentTo(entity, o => o.Excluding(x => x.ArtifactID));
		}
	}
}
