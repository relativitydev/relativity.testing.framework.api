using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetAllByDateStrategy<Error>))]
	internal class ErrorGetAllByDateStrategyFixture : ApiServiceTestFixture<IGetAllByDateStrategy<Error>>
	{
		public ErrorGetAllByDateStrategyFixture()
		{
		}

		public ErrorGetAllByDateStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[Test]
		public void GetAllByDate()
		{
			Error entity = null;

			Arrange(x => x.Create(new Error { Message = "New error" }).Pick(out entity));

			var result = Sut.GetAll(DateTime.Now.AddHours(-1), DateTime.Now);

			result.Should().Contain(x => x.ArtifactID == entity.ArtifactID);
		}
	}
}
