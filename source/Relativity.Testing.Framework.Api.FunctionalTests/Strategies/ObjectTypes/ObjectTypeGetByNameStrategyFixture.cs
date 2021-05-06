using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetWorkspaceEntityByNameStrategy<ObjectType>))]
	internal class ObjectTypeGetByNameStrategyFixture : ApiServiceTestFixture<IGetWorkspaceEntityByNameStrategy<ObjectType>>
	{
		public ObjectTypeGetByNameStrategyFixture()
		{
		}

		public ObjectTypeGetByNameStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[Test]
		public void Get_Missing()
		{
			var result = Sut.Get(-1, Guid.NewGuid().ToString());

			result.Should().BeNull();
		}

		[Test]
		public void Get_Existing()
		{
			ObjectType existingObjectType = null;

			Arrange(() =>
			{
				existingObjectType = Facade.Resolve<ICreateWorkspaceEntityStrategy<ObjectType>>()
					.Create(-1, new ObjectType());
			});

			var result = Sut.Get(-1, existingObjectType.Name);

			result.Should().BeEquivalentTo(existingObjectType);
		}
	}
}
