using System;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ICreateStrategy<User>))]
	public class UserCreateStrategyFixture : ApiTestFixture
	{
		private ICreateStrategy<User> _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = Facade.Resolve<ICreateStrategy<User>>();
		}

		[Test]
		public void Create_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				_sut.Create(null));
		}

		[Test]
		public void Create_WithEmptyEntity()
		{
			var entity = new User();

			var result = _sut.Create(entity);

			result.ArtifactID.Should().BePositive();
			result.FirstName.Should().NotBeNullOrEmpty();
			result.LastName.Should().NotBeNullOrEmpty();
			result.EmailAddress.Should().NotBeNullOrEmpty();
			result.Type.Should().Be("Internal");
			result.Client.Should().NotBeNull();
		}

		[Test]
		public void Create_WithFilledEntity()
		{
			Client client = null;

			Arrange(x => x.Create(out client));

			var entity = new User
			{
				FirstName = Randomizer.GetString(),
				LastName = Randomizer.GetString(),
				EmailAddress = Randomizer.GetEmailAddress(),
				Type = "External",
				ItemListPageLength = 99,
				Client = client,
				DefaultSelectedFileType = UserDefaultSelectedFileType.LongText,
				BetaUser = true,
				ChangeSettings = false,
				TrustedIPs = "127.0.0.1",
				RelativityAccess = false,
				AdvancedSearchPublicByDefault = false,
				NativeViewerCacheAhead = false,
				ChangePassword = false,
				MaximumPasswordAge = 90,
				ChangePasswordNextLogin = true,
				Password = "Test1234567!",
				DocumentSkip = UserDocumentSkip.Enabled,
				DataFocus = 5,
				KeyboardShortcuts = false,
				EnforceViewerCompatibility = false,
				SkipDefaultPreference = UserSkipDefaultPreference.Skip,
				CanChangeDocumentViewer = true,
				DisableOnDate = DateTime.UtcNow.Date.AddYears(1).AddHours(8).AddMinutes(45).AddSeconds(30),
				EmailPreference = UserEmailPreference.ErrorOnly,
				DocumentViewer = UserDocumentViewer.Html
			};

			var result = _sut.Create(entity.Copy());

			using (new AssertionScope())
			{
				result.ArtifactID.Should().BePositive();
				result.Groups.Should().ContainSingle().Which.ArtifactID.Should().BePositive();
				result.Should().BeEquivalentTo(entity, o => o.Excluding(x => x.ArtifactID).Excluding(x => x.Groups).Excluding(x => x.Client.Status.ArtifactID));
			}

			var gotEntity = Facade.Resolve<IUserGetByEmailStrategy>().Get(result.EmailAddress);

			gotEntity.Should().BeEquivalentTo(result, o => o.Excluding(x => x.Password).Excluding(x => x.Client).Including(x => x.Client.ArtifactID));
		}
	}
}
