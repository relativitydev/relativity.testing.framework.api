using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(InstanceSettingAddValueStrategy))]
	internal class InstanceSettingAddValueStrategyFixture : ApiServiceTestFixture<IInstanceSettingAddValueStrategy>
	{
		private IGetByIdStrategy<InstanceSetting> _getByIdStrategy;

		public InstanceSettingAddValueStrategyFixture()
		{
		}

		public InstanceSettingAddValueStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		protected override void OnSetUpTest()
		{
			base.OnSetUpTest();
			_getByIdStrategy = Facade.Resolve<IGetByIdStrategy<InstanceSetting>>();
		}

		[Test]
		public void AddValue_OldValueEmpty()
		{
			InstanceSetting instanceSetting = null;
			const string valueToAdd = "New Value";
			const string delimiter = " ";

			Arrange(x => x.Create(out instanceSetting));

			Sut.AddValue(instanceSetting.Name, instanceSetting.Section, valueToAdd, delimiter);

			var result = _getByIdStrategy.Get(instanceSetting.ArtifactID);

			result.Value.Should().Be(instanceSetting.Value + valueToAdd);
			result.Should().BeEquivalentTo(instanceSetting, o => o.Excluding(x => x.Value));
		}

		[Test]
		public void AddValue_OldValueNotEmpty()
		{
			const string valueToAdd = "New Value";
			const string delimiter = " ";

			InstanceSetting instanceSetting = new InstanceSetting();
			instanceSetting.FillRequiredProperties();
			instanceSetting.Value = "Old Value";

			Arrange(x => x.Create(instanceSetting).Pick(out instanceSetting));

			Sut.AddValue(instanceSetting.Name, instanceSetting.Section, valueToAdd, delimiter);

			var result = _getByIdStrategy.Get(instanceSetting.ArtifactID);

			result.Value.Should().Be(instanceSetting.Value + delimiter + valueToAdd);
			result.Should().BeEquivalentTo(instanceSetting, o => o.Excluding(x => x.Value));
		}
	}
}
