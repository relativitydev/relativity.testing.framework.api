using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(LibraryApplicationInstallToWorkspaceStrategy))]
	internal class LibraryApplicationInstallRapStrategyFixture : ApiServiceTestFixture<ILibraryApplicationInstallRapStrategy>
	{
		private IGetByIdStrategy<LibraryApplication> _getByIdStrategy;

		public LibraryApplicationInstallRapStrategyFixture()
		{
		}

		public LibraryApplicationInstallRapStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		protected override void OnSetUpTest()
		{
			base.OnSetUpTest();

			_getByIdStrategy = Facade.Resolve<IGetByIdStrategy<LibraryApplication>>();
		}

		[Test]
		public void Install_Missing()
		{
			Assert.Throws<FileNotFoundException>(() =>
				Sut.InstallToLibrary(Guid.NewGuid().ToString()));
		}

		[Test]
		public void Install_Existing()
		{
			var artifactId = Sut.InstallToLibrary($@"{AppDomain.CurrentDomain.BaseDirectory}\files\Transcripts.rap");
			_getByIdStrategy.Get(artifactId).Should().NotBeNull();
		}
	}
}
