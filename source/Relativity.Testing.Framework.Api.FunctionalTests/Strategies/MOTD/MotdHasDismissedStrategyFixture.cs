using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IMotdHasDismissedStrategy))]
	[NonParallelizable]
	internal class MotdHasDismissedStrategyFixture : ApiServiceTestFixture<IMotdHasDismissedStrategy>
	{
		private MessageOfTheDay _currentMotd;
		private IUpdateStrategy<MessageOfTheDay> _updateStrategy;

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();
			var tempMotd = Facade.Resolve<IMotdGetStrategy>().Get();
			_updateStrategy = Facade.Resolve<IUpdateStrategy<MessageOfTheDay>>();

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
		public void HasDismiss_False()
		{
			User testUser = null;
			Arrange(x => x.Create(out testUser));

			Sut.HasDismissed(testUser.ArtifactID).Should().BeFalse();
		}

		[Test]
		public void HasDismiss_True()
		{
			User testUser = null;
			Arrange(x =>
			{
				x.Create(out testUser);
				Facade.Resolve<IMotdDismissStrategy>().Dismiss(testUser.ArtifactID);
			});

			Sut.HasDismissed(testUser.ArtifactID).Should().BeTrue();
		}
	}
}
