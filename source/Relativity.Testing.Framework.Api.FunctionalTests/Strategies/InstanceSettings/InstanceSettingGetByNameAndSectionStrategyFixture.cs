using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(InstanceSettingGetByNameAndSectionStrategy))]
	internal class InstanceSettingGetByNameAndSectionStrategyFixture : ApiServiceTestFixture<IInstanceSettingGetByNameAndSectionStrategy>
	{
		private InstanceSetting _existingEntity;

		public InstanceSettingGetByNameAndSectionStrategyFixture()
		{
		}

		public InstanceSettingGetByNameAndSectionStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			Arrange(x => x.Create<InstanceSetting>(3).PickFirst(out _existingEntity));
		}

		[Test]
		public void Get_WithNullName()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Get(null, _existingEntity.Section));
		}

		[Test]
		public void Get_MissingName()
		{
			var result = Sut.Get(Guid.NewGuid().ToString(), _existingEntity.Section);

			result.Should().BeNull();
		}

		[Test]
		public void Get_WithNullSection()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Get(_existingEntity.Name, null));
		}

		[Test]
		public void Get_MissingSection()
		{
			var result = Sut.Get(_existingEntity.Name, Guid.NewGuid().ToString());

			result.Should().BeNull();
		}

		[Test]
		public void Get_Existing()
		{
			var result = Sut.Get(_existingEntity.Name, _existingEntity.Section);

			result.Should().BeEquivalentTo(_existingEntity);
		}
	}
}
