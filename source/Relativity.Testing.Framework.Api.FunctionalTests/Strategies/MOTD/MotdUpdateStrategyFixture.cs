using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IMotdUpdateStrategy))]
	internal class MotdUpdateStrategyFixture : ApiServiceTestFixture<IMotdUpdateStrategy>
	{
		private MessageOfTheDay _currentMotd;
		private IMotdGetStrategy _motdGetStrategy;

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

			var result = Sut.Update(toUpdate);

			result.Should().BeEquivalentTo(toUpdate);
		}

		[Test]
		public async Task UpdateAsync()
		{
			var toUpdate = new MessageOfTheDay
			{
				Enabled = !_currentMotd.Enabled,
				Message = Randomizer.GetString(),
				AllowDismiss = !_currentMotd.AllowDismiss
			};

			var result = await Sut.UpdateAsync(toUpdate).ConfigureAwait(false);

			result.Should().BeEquivalentTo(toUpdate);
		}
	}
}
