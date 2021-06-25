using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(InstanceSettingGetByIdStrategy))]
	internal class InstanceSettingGetByIdStrategyFixture : ApiServiceTestFixture<IGetByIdStrategy<InstanceSetting>>
	{
		[Test]
		public void Get_Missing()
		{
			var result = Sut.Get(int.MaxValue);

			result.Should().BeNull();
		}

		[Test]
		public void Get_Existing()
		{
			InstanceSetting expectedEntity = null;

			Arrange(x => x.Create<InstanceSetting>(3).PickMiddle(out expectedEntity));

			var result = Sut.Get(expectedEntity.ArtifactID);

			result.Should().BeEquivalentTo(expectedEntity);
		}
	}
}
