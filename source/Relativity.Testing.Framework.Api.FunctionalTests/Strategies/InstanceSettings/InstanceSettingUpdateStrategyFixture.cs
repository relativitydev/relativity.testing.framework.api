using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(InstanceSettingUpdateStrategy))]
	internal class InstanceSettingUpdateStrategyFixture : ApiServiceTestFixture<IUpdateStrategy<InstanceSetting>>
	{
		private IGetByIdStrategy<InstanceSetting> _getByIdStrategy;

		public InstanceSettingUpdateStrategyFixture()
		{
		}

		public InstanceSettingUpdateStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		protected override void OnSetUpTest()
		{
			base.OnSetUpTest();
			_getByIdStrategy = Facade.Resolve<IGetByIdStrategy<InstanceSetting>>();
		}

		[Test]
		public void Update_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Update(null));
		}

		[Test]
		public void Update()
		{
			InstanceSetting instanceSetting = null;

			Arrange(x => x.Create(out instanceSetting));

			var instanceSettingToUpdate = new InstanceSetting
			{
				ArtifactID = instanceSetting.ArtifactID,
				Value = Randomizer.GetString("AT_Value_"),
				Machine = Randomizer.GetString("AT_Machine_")
			};

			Sut.Update(instanceSettingToUpdate);

			var result = _getByIdStrategy.Get(instanceSetting.ArtifactID);

			result.Value.Should().Be(instanceSettingToUpdate.Value);
			result.Machine.Should().Be(instanceSettingToUpdate.Machine);
			result.Should().BeEquivalentTo(instanceSetting, o => o.Excluding(x => x.Value).Excluding(x => x.Machine));
		}
	}
}
