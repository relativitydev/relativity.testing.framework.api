using System;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetByNameStrategy<LibraryApplication>))]
	internal class LibraryApplicationGetByNameStrategyFixture : ApiServiceTestFixture<IGetByNameStrategy<LibraryApplication>>
	{
		public LibraryApplicationGetByNameStrategyFixture()
		{
		}

		public LibraryApplicationGetByNameStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[Test]
		public void Get_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Get(null));
		}

		[Test]
		public void Get_Missing()
		{
			var result = Sut.Get(Guid.NewGuid().ToString());

			result.Should().BeNull();
		}

		[Test]
		public void Get_Existing()
		{
			var existingApplicationLibrary = "Production";

			var result = Sut.Get(existingApplicationLibrary);

			using (new AssertionScope())
			{
				result.Name.Should().Be(existingApplicationLibrary);
				result.ArtifactID.Should().BePositive();
				result.Version.Should().NotBeNullOrWhiteSpace();
				result.FileName.Should().NotBeNullOrWhiteSpace();
				result.Guids.Should().NotBeNullOrEmpty();

				result.CreatedOn.Should().BeAfter(DateTime.MinValue);
				result.CreatedBy.Should().NotBeNull();
				result.CreatedBy.ArtifactID.Should().BePositive();
				result.CreatedBy.Name.Should().NotBeNullOrWhiteSpace();

				result.LastModifiedOn.Should().BeAfter(DateTime.MinValue);
				result.LastModifiedBy.Should().NotBeNull();
				result.LastModifiedBy.ArtifactID.Should().BePositive();
				result.LastModifiedBy.Name.Should().NotBeNullOrWhiteSpace();
			}
		}
	}
}
