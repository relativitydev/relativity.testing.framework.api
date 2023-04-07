using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IWaitDeleteWorkspaceStrategy))]
	internal class WaitDeleteWorkspaceStrategy : ApiServiceTestFixture<IWaitDeleteWorkspaceStrategy>
	{
		private IGetByIdStrategy<Workspace> _getById;

		public WaitDeleteWorkspaceStrategy()
		{
		}

		public WaitDeleteWorkspaceStrategy(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		protected override void OnSetUpTest()
		{
			base.OnSetUpTest();
			_getById = Facade.Resolve<IGetByIdStrategy<Workspace>>();
		}

		[Test]
		public void WaitSucceeds()
		{
			int id = 9_999_999;
			Assert.DoesNotThrow(() => Sut.Wait(id));
		}

		[Test]
		public void WaitTimesOut_ShouldThrow_InvalidOperationException()
		{
			var exception = Assert.Throws<InvalidOperationException>(() =>
				Sut.Wait(DefaultWorkspace.ArtifactID));

			exception.Message.Should().StartWith($"Workspace with id={DefaultWorkspace.ArtifactID} was not deleted within the 3 minute time limit.");
		}
	}
}
