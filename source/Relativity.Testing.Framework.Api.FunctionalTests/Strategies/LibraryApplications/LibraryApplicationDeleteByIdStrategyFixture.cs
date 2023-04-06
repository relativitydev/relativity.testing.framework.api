using System;
using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IDeleteByIdStrategy<LibraryApplication>))]
	internal class LibraryApplicationDeleteByIdStrategyFixture : ApiServiceTestFixture<IDeleteByIdStrategy<LibraryApplication>>
	{
		private readonly string _applicationPath = $@"{AppDomain.CurrentDomain.BaseDirectory}\files\RapTemplate.rap";
		private IGetByNameStrategy<LibraryApplication> _getByNameStrategy;
		private ILibraryApplicationInstallRapStrategy _libraryApplicationInstallStrategy;

		protected override void OnSetUpTest()
		{
			base.OnSetUpTest();

			_getByNameStrategy = Facade.Resolve<IGetByNameStrategy<LibraryApplication>>();
			_libraryApplicationInstallStrategy = Facade.Resolve<ILibraryApplicationInstallRapStrategy>();

			LibraryApplication application = _getByNameStrategy.Get("RapTemplate");
			if (application == null)
			{
				_libraryApplicationInstallStrategy.InstallToLibrary(_applicationPath);
			}
		}

		[Test]
		public void Delete_Missing()
		{
			Assert.Throws<HttpRequestException>(() => Sut.Delete(12345));
		}

		[Test]
		public void Delete_Existing()
		{
			LibraryApplication application = _getByNameStrategy.Get("RapTemplate");

			Sut.Delete(application.ArtifactID);

			application = _getByNameStrategy.Get("RapTemplate");
			application.Should().BeNull();
		}
	}
}
