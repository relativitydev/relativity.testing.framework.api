﻿using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IProductionsDataSourceGetDefaultFieldValuesStrategy))]
	internal class ProductionsDataSourceGetDefaultFieldValuesStrategyFixture : ApiServiceTestFixture<IProductionsDataSourceGetDefaultFieldValuesStrategy>
	{
		[Test]
		public void Get_ShouldNotThrown()
		{
			Assert.DoesNotThrow(() => Sut.Get(DefaultWorkspace.ArtifactID));
		}

		[Test]
		public void Get_ResponseNotEmpty()
		{
			var result = Sut.Get(DefaultWorkspace.ArtifactID);
			Assert.AreNotEqual(0, result.BurnRedactions.ArtifactID);
			Assert.AreNotEqual(0, result.UseImagePlaceholder.ArtifactID);
		}
	}
}
