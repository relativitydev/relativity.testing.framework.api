using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(InstanceSettingUpdateValueStrategy))]
	internal class InstanceSettingUpdateValueStrategyPrePrairieSmokeFixture : ApiServiceTestFixture<IInstanceSettingUpdateValueStrategy>
	{
		private IGetByIdStrategy<InstanceSetting> _getByIdStrategy;

		protected override void OnSetUpTest()
		{
			base.OnSetUpTest();
			_getByIdStrategy = Facade.Resolve<IGetByIdStrategy<InstanceSetting>>();
		}

		[Test]
		public void UpdateValue()
		{
			InstanceSetting instanceSetting = null;
			const string newValue = "New Value";

			Arrange(x => x.Create(out instanceSetting));

			Sut.UpdateValue(instanceSetting.Name, instanceSetting.Section, newValue);

			var result = _getByIdStrategy.Get(instanceSetting.ArtifactID);

			result.Value.Should().Be(newValue);
			result.Should().BeEquivalentTo(instanceSetting, o => o.Excluding(x => x.Value));
		}
	}
}
