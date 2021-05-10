using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(InstanceSettingRequireStrategy))]
	internal class InstanceSettingRequireStrategyFixture : ApiServiceTestFixture<IRequireStrategy<InstanceSetting>>
	{
		private IGetByIdStrategy<InstanceSetting> _getByIdStrategy;

		public InstanceSettingRequireStrategyFixture()
		{
		}

		public InstanceSettingRequireStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		protected override void OnSetUpTest()
		{
			base.OnSetUpTest();
			_getByIdStrategy = Facade.Resolve<IGetByIdStrategy<InstanceSetting>>();
		}

		[Test]
		public void Require_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Require(null));
		}

		[Test]
		public void Require_Existing()
		{
			InstanceSetting instanceSetting = null;

			Arrange(x => x.Create(out instanceSetting));

			var instanceSettingToUpdate = new InstanceSetting
			{
				ArtifactID = instanceSetting.ArtifactID,
				Value = Randomizer.GetString("AT_Value_"),
				Machine = Randomizer.GetString("AT_Machine_")
			};

			Sut.Require(instanceSettingToUpdate);

			var result = _getByIdStrategy.Get(instanceSetting.ArtifactID);

			result.Value.Should().Be(instanceSettingToUpdate.Value);
			result.Machine.Should().Be(instanceSettingToUpdate.Machine);
			result.Should().BeEquivalentTo(instanceSetting, o => o.Excluding(x => x.Value).Excluding(x => x.Machine));
		}

		[Test]
		public void Require_Missing()
		{
			var entity = new InstanceSetting
			{
				Name = Randomizer.GetString("AT_Name_"),
				Value = "4",
				Description = Randomizer.GetString("AT_Description_"),
				Section = Randomizer.GetString("AT_Section_"),
				Machine = Randomizer.GetString("AT_Machine_"),
				InitialValue = Randomizer.GetString("AT_InitialValue_"),
				ValueType = InstanceSettingValueType.PositiveInt32
			};

			var result = Sut.Require(entity);

			result.ArtifactID.Should().BePositive();
			result.Should().BeEquivalentTo(entity, o => o.Excluding(x => x.ArtifactID));
		}
	}
}
