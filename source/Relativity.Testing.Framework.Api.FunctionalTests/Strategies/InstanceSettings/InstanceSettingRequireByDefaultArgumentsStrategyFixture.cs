using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(InstanceSettingRequireStrategy))]
	internal class InstanceSettingRequireByDefaultArgumentsStrategyFixture : ApiServiceTestFixture<IInstanceSettingRequireByDefaultArgumentsStrategy>
	{
		public InstanceSettingRequireByDefaultArgumentsStrategyFixture()
		{
		}

		public InstanceSettingRequireByDefaultArgumentsStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[Test]
		public void Require_Existing()
		{
			InstanceSetting instanceSetting = null;
			const string newValue = "New value.";

			Arrange(x => x.Create(out instanceSetting));

			var result = Sut.Require(instanceSetting.Name, instanceSetting.Section, newValue);

			result.Value.Should().Be(newValue);

			result.Should().BeEquivalentTo(instanceSetting, o => o.Excluding(x => x.Value));
		}

		[Test]
		public void Require_Missing()
		{
			var entity = new InstanceSetting();
			entity.FillRequiredProperties();
			entity.Value = Randomizer.GetString();

			var result = Sut.Require(entity.Name, entity.Section, entity.Value);

			result.ArtifactID.Should().BePositive();
			result.Should().BeEquivalentTo(entity, o => o.Excluding(x => x.ArtifactID));
		}
	}
}
