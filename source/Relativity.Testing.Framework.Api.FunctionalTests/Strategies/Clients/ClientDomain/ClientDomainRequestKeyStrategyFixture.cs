using System.Threading;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[VersionRange(">=12.1")]
	[TestOf(typeof(IClientDomainRequestKeyStrategy))]
	public class ClientDomainRequestKeyStrategyFixture : ApiTestFixture
	{
		private const string _CLIENT_DOMAIN_FEATURE_AVAILABLE_NAME = "ClientDomainFeatureAvailable";
		private const string _CLIENT_DOMAIN_FEATURE_AVAILABLE_SECTION = "Relativity.Core";
		private IClientDomainRequestKeyStrategy _sut;
		private IInstanceSettingsService _instanceSettingsService;
		private bool _instanceSettingNeedsChange;

		[OneTimeSetUp]
		public void SetUp()
		{
			_sut = Facade.Resolve<IClientDomainRequestKeyStrategy>();
			_instanceSettingsService = Facade.Resolve<IInstanceSettingsService>();
			_instanceSettingNeedsChange = _instanceSettingsService.Get(_CLIENT_DOMAIN_FEATURE_AVAILABLE_NAME, _CLIENT_DOMAIN_FEATURE_AVAILABLE_SECTION).Value.Equals("False");
			if (_instanceSettingNeedsChange)
			{
				_instanceSettingsService.UpdateValue(_CLIENT_DOMAIN_FEATURE_AVAILABLE_NAME, _CLIENT_DOMAIN_FEATURE_AVAILABLE_SECTION, "True");
				bool notUpdated = CheckIfClientDomainFeatureIsDisabled();

				while (notUpdated)
				{
					Thread.Sleep(100);
					notUpdated = CheckIfClientDomainFeatureIsDisabled();
				}
			}
		}

		private bool CheckIfClientDomainFeatureIsDisabled()
		{
			return _instanceSettingsService.Get(_CLIENT_DOMAIN_FEATURE_AVAILABLE_NAME, _CLIENT_DOMAIN_FEATURE_AVAILABLE_SECTION).Equals("False");
		}

		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			if (_instanceSettingNeedsChange)
			{
				_instanceSettingsService.UpdateValue(_CLIENT_DOMAIN_FEATURE_AVAILABLE_NAME, _CLIENT_DOMAIN_FEATURE_AVAILABLE_SECTION, "False");
			}
		}

		[Test]
		public void Request_WithValidClientArtifactID_DoesNotThrowException()
		{
			Client client = null;
			Arrange(x => x.Create(out client));

			Assert.DoesNotThrow(() =>
				_sut.Request(client.ArtifactID));
		}

		[Test]
		public void Request_WithValidClientArtifactID_ReturnsValidString()
		{
			Client client = null;
			Arrange(x => x.Create(out client));

			string result = _sut.Request(client.ArtifactID);

			Assert.IsFalse(string.IsNullOrWhiteSpace(result));
		}
	}
}
