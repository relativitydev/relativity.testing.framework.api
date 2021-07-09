using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ICreateStrategy<InstanceSetting>))]
	internal class InstanceSettingCreateStrategyFixture : ApiServiceTestFixture<ICreateStrategy<InstanceSetting>>
	{
		[Test]
		public void Create_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Create(null));
		}

		[Test]
		public void Create_WithEmptyEntity()
		{
			var result = Sut.Create(new InstanceSetting());

			result.ArtifactID.Should().BePositive();
			result.Name.Should().NotBeNullOrEmpty();
			result.Section.Should().NotBeNullOrEmpty();
			result.ValueType.Should().Be(InstanceSettingValueType.Text);
		}

		[Test]
		public void Create_WithFilledEntity()
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

			var result = Sut.Create(entity);

			result.ArtifactID.Should().BePositive();
			result.Should().BeEquivalentTo(entity, o => o.Excluding(x => x.ArtifactID));
		}
	}
}
