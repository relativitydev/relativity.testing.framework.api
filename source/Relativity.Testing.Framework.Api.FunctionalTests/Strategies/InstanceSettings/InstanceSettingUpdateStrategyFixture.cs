using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IUpdateStrategy<InstanceSetting>))]
	internal class InstanceSettingUpdateStrategyFixture : ApiServiceTestFixture<IUpdateStrategy<InstanceSetting>>
	{
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

			var result = Sut.Update(instanceSettingToUpdate);

			result.Value.Should().Be(instanceSettingToUpdate.Value);
			result.Machine.Should().Be(instanceSettingToUpdate.Machine);
			result.Should().BeEquivalentTo(instanceSetting, o => o.Excluding(x => x.Value).Excluding(x => x.Machine));
		}
	}
}
