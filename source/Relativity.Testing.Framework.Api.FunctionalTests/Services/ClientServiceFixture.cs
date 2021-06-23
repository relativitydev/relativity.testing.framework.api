using System;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Services
{
	[TestOf(typeof(ClientService))]
	[NonParallelizable]
	public class ClientServiceFixture : ApiTestFixture
	{
		private IClientService _sut;

		public ClientServiceFixture()
		{
		}

		public ClientServiceFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			_sut = Facade.Resolve<IClientService>();
		}

		[Test]
		public void Delete_WithNegativeId_ThrowsArgumentException()
		{
			var clientId = -2;

			Assert.Throws<ArgumentException>(() =>
				_sut.Delete(clientId));
		}

		[Test]
		public void Get_WithNegativeId_ThrowsArgumentException()
		{
			var clientId = -2;

			Assert.Throws<ArgumentException>(() =>
				_sut.Get(clientId));
		}

		[Test]
		public void Get_WithNullName_ThrowsArgumentException()
		{
			Assert.Throws<ArgumentException>(() =>
				_sut.Get(null));
		}

		[Test]
		public void Get_WithEmptyName_ThrowsArgumentException()
		{
			Assert.Throws<ArgumentException>(() =>
				_sut.Get(string.Empty));
		}

		[Test]
		public void Get_WithWhitespaceName_ThrowsArgumentException()
		{
			Assert.Throws<ArgumentException>(() =>
				_sut.Get("	"));
		}
	}
}
