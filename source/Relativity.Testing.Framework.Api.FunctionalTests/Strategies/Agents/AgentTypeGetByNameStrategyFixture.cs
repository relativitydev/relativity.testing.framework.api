using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(AgentTypeGetByNameStrategy))]
	internal class AgentTypeGetByNameStrategyFixture : ApiServiceTestFixture<IGetByNameStrategy<AgentType>>
	{
		[Test]
		public void Get_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Get(null));
		}

		[Test]
		public void Get_Missing()
		{
			var result = Sut.Get(Guid.NewGuid().ToString());

			result.Should().BeNull();
		}

		[Test]
		public void Get_Existing()
		{
			AgentType existingAgentType = null;

			Arrange(() =>
			{
				existingAgentType = Facade.Resolve<IGetAllStrategy<AgentType>>()
					.GetAll()[0];
			});

			var result = Sut.Get(existingAgentType.Name);

			result.Should().BeEquivalentTo(existingAgentType);
		}
	}
}
