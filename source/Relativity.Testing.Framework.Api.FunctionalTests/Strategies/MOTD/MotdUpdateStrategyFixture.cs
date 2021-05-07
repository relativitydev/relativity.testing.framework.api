using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IUpdateStrategy<MessageOfTheDay>))]
	internal class MotdUpdateStrategyFixture : ApiServiceTestFixture<IUpdateStrategy<MessageOfTheDay>>
	{
		private MessageOfTheDay _currentMotd;
		private IMotdGetStrategy _motdGetStrategy;

		public MotdUpdateStrategyFixture()
		{
		}

		public MotdUpdateStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();
			_motdGetStrategy = Facade.Resolve<IMotdGetStrategy>();
			_currentMotd = _motdGetStrategy.Get();
		}

		protected override void OnTearDownFixture()
		{
			base.OnTearDownFixture();
			Sut.Update(_currentMotd);
		}

		[Test]
		public void Update()
		{
			var toUpdate = new MessageOfTheDay
			{
				Enabled = !_currentMotd.Enabled,
				Message = Randomizer.GetString(),
				AllowDismiss = !_currentMotd.AllowDismiss
			};

			Sut.Update(toUpdate);

			var result = _motdGetStrategy.Get();

			result.Should().BeEquivalentTo(toUpdate);
		}
	}
}
