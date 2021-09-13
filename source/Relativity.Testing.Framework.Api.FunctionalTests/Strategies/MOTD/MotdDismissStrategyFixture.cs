using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IMotdDismissStrategy))]
	[NonParallelizable]
	internal class MotdDismissStrategyFixture : ApiServiceTestFixture<IMotdDismissStrategy>
	{
		private MessageOfTheDay _currentMotd;
		private IMotdUpdateStrategy _updateStrategy;
		private IMotdHasDismissedStrategy _hasDismissedStrategy;

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();
			var tempMotd = Facade.Resolve<IMotdGetStrategy>().Get();
			_updateStrategy = Facade.Resolve<IMotdUpdateStrategy>();
			_hasDismissedStrategy = Facade.Resolve<IMotdHasDismissedStrategy>();

			if (!tempMotd.Enabled)
			{
				_currentMotd = tempMotd.Copy();
				tempMotd.Enabled = true;
				tempMotd.AllowDismiss = true;

				_updateStrategy.Update(tempMotd);
			}
		}

		protected override void OnTearDownFixture()
		{
			base.OnTearDownFixture();

			if (_currentMotd != null)
			{
				_updateStrategy.Update(_currentMotd);
			}
		}

		[Test]
		public void Dismiss_ByArtifactID()
		{
			User testUser = null;
			Arrange(x => x.Create(out testUser));

			Sut.Dismiss(testUser.ArtifactID);
			_hasDismissedStrategy.HasDismissed(testUser.ArtifactID).Should().BeTrue();
		}

		[Test]
		public void Dismiss_ByEmailAddress()
		{
			User testUser = null;
			Arrange(x => x.Create(out testUser));

			Sut.Dismiss(testUser.EmailAddress);
			_hasDismissedStrategy.HasDismissed(testUser.ArtifactID).Should().BeTrue();
		}
	}
}
