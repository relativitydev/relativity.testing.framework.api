using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Api.Strategies.Toggle;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies.Toggles
{
	[TestOf(typeof(ToggleGetByAllStrategy))]
	internal class ToggleGetAllStrategyFixture : ApiServiceTestFixture<IGetAllStrategy<Toggle>>
	{
		[Test]
		public void GetAll()
		{
			var result = Sut.GetAll();

			result.Should().NotBeEmpty();
		}
	}
}
