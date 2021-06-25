using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(InstanceSettingRequireStrategy))]
	internal class InstanceSettingRequireContainsValueStrategyFixture : ApiServiceTestFixture<IInstanceSettingRequireContainsValueStrategy>
	{
		private IInstanceSettingGetByNameAndSectionStrategy _getByNameAndSectionStrategy;

		protected override void OnSetUpTest()
		{
			base.OnSetUpTest();
			_getByNameAndSectionStrategy = Facade.Resolve<IInstanceSettingGetByNameAndSectionStrategy>();
		}

		[Test]
		public void RequireContainsValue_Missing()
		{
			string name = Randomizer.GetString("AT_Name_");
			string section = Randomizer.GetString("AT_Section_");
			string value = Randomizer.GetString("AT_Value_");

			Sut.RequireContainsValue(name, section, value, ";");

			var result = _getByNameAndSectionStrategy.Get(name, section);

			result.Value.Should().Be(value);
		}

		[Test]
		public void RequireContainsValue_AlreadyContains()
		{
			string name = Randomizer.GetString("AT_Name_");
			string section = Randomizer.GetString("AT_Section_");
			string value = Randomizer.GetString("AT_Value_");

			Arrange(x => x.Create(new InstanceSetting { Name = name, Section = section, Value = value }));

			Sut.RequireContainsValue(name, section, value, ";");

			var result = _getByNameAndSectionStrategy.Get(name, section);

			result.Value.Should().Be(value);
		}

		[Test]
		public void RequireContainsValue_DoesNotContain()
		{
			string name = Randomizer.GetString("AT_Name_");
			string section = Randomizer.GetString("AT_Section_");
			string initialValue = Randomizer.GetString("AT_NotValue_");
			string addedValue = Randomizer.GetString("AT_Value_");

			Arrange(x => x.Create(new InstanceSetting { Name = name, Section = section, Value = initialValue }));

			Sut.RequireContainsValue(name, section, addedValue, ";");

			var result = _getByNameAndSectionStrategy.Get(name, section);

			result.Value.Should().Be($"{initialValue};{addedValue}");
		}
	}
}
