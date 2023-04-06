using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(InstanceSettingRequireStrategy))]
	internal class InstanceSettingRequireNotContainsValueStrategyFixture : ApiServiceTestFixture<IInstanceSettingRequireNotContainsValueStrategy>
	{
		private IInstanceSettingGetByNameAndSectionStrategy _getByNameAndSectionStrategy;

		protected override void OnSetUpTest()
		{
			base.OnSetUpTest();
			_getByNameAndSectionStrategy = Facade.Resolve<IInstanceSettingGetByNameAndSectionStrategy>();
		}

		[Test]
		public void RequireNotContainsValue_Missing()
		{
			string name = Randomizer.GetString("AT_Name_");
			string section = Randomizer.GetString("AT_Section_");
			string value = Randomizer.GetString("AT_Value_");

			Sut.RequireNotContainsValue(name, section, value, ";");

			var result = _getByNameAndSectionStrategy.Get(name, section);

			result.Should().BeNull();
		}

		[Test]
		public void RequireNotContainsValue_AlreadyNotContains()
		{
			string name = Randomizer.GetString("AT_Name_");
			string section = Randomizer.GetString("AT_Section_");
			string value = Randomizer.GetString("AT_Value_");

			Arrange(x => x.Create(new InstanceSetting { Name = name, Section = section, Value = value }));

			Sut.RequireNotContainsValue(name, section, "SomethingThatIsNotHere", ";");

			var result = _getByNameAndSectionStrategy.Get(name, section);

			result.Value.Should().Be(value);
		}

		[Test]
		public void RequireNotContainsValue_Contains_IsLastValue()
		{
			string name = Randomizer.GetString("AT_Name_");
			string section = Randomizer.GetString("AT_Section_");
			string value = Randomizer.GetString("AT_Value_");

			Arrange(x => x.Create(new InstanceSetting { Name = name, Section = section, Value = value }));

			Sut.RequireNotContainsValue(name, section, value, ";");

			var result = _getByNameAndSectionStrategy.Get(name, section);

			result.Should().BeNull();
		}

		[Test]
		public void RequireNotContainsValue_Contains_IsNotLastValue()
		{
			string name = Randomizer.GetString("AT_Name_");
			string section = Randomizer.GetString("AT_Section_");
			string valueToKeep = Randomizer.GetString("AT_ValueToKeep_");
			string valueToRemove = Randomizer.GetString("AT_ValueToRemove_");

			Arrange(x => x.Create(new InstanceSetting { Name = name, Section = section, Value = string.Join(";", new[] { valueToKeep, valueToRemove }) }));

			Sut.RequireNotContainsValue(name, section, valueToRemove, ";");

			var result = _getByNameAndSectionStrategy.Get(name, section);

			result.Value.Should().Be(valueToKeep);
		}
	}
}
