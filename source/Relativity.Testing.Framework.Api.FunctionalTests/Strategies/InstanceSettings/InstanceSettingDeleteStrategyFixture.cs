using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(InstanceSettingDeleteByIdStrategy))]
	internal class InstanceSettingDeleteStrategyFixture : ApiServiceTestFixture<IDeleteByIdStrategy<InstanceSetting>>
	{
		public InstanceSettingDeleteStrategyFixture()
		{
		}

		public InstanceSettingDeleteStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[Test]
		public void Delete_Missing()
		{
			Assert.Throws<HttpRequestException>(() =>
				Sut.Delete(int.MaxValue));
		}

		[Test]
		public void Delete_Existing()
		{
			InstanceSetting toDelete = null;

			Arrange(x => x.Create(out toDelete));

			Sut.Delete(toDelete.ArtifactID);

			Facade.Resolve<IGetByIdStrategy<InstanceSetting>>().Get(toDelete.ArtifactID).
				Should().BeNull();
		}
	}
}
