using System.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Session;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests
{
	[TestOf(typeof(TestSession))]
	[NonParallelizable] // These tests can run into issues if run at the same time as other tests that modify the session/account pool.
	public class TestSessionFixture : ApiTestFixture
	{
		public TestSessionFixture()
		{
		}

		public TestSessionFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[Test]
		public void EntityIsAddedToSessionWhenCreated()
		{
			Client client = null;

			Arrange(x => x.Create(out client));

			TestSession.Current.Entities.Should().BeEquivalentTo(new[] { client });
		}

		[Test]
		public void EntityIsRemovedFromSessionWhenDeleted()
		{
			Client client = null;

			Arrange(x => x.Create(out client));

			Facade.Resolve<IDeleteByIdStrategy<Client>>().Delete(client.ArtifactID);

			TestSession.Current.Entities.Should().BeEmpty();
		}

		[Test]
		public void Dispose_DeletesEntity()
		{
			Client client = null;

			Arrange(x => x.Create(out client));

			TestSession.Current.Dispose();

			Facade.Resolve<IGetByIdStrategy<Client>>().Get(client.ArtifactID).
				Should().BeNull();
		}

		[Test]
		public void Dispose_DeletesWorkspaceCompletely()
		{
			Workspace workspace = null;

			Arrange(x => x.Create(out workspace));

			ObjectType objectType = Facade.Resolve<ICreateWorkspaceEntityStrategy<ObjectType>>().
				Create(workspace.ArtifactID, new ObjectType());

			TestSession.Current.EntityItems.First(x => x.Entity == objectType).
				WorkspaceId.Should().Be(workspace.ArtifactID);

			TestSession.Current.Dispose();

			Facade.Resolve<IGetByIdStrategy<Workspace>>().Get(workspace.ArtifactID).
				Should().BeNull();
		}

		[Test]
		public void Dispose_DeletesWorkspaceEntities_WhenWorkspaceIsOutOfSession()
		{
			Workspace workspace = null;

			Arrange(x => x.Create(out workspace));
			TestSession.Current.Remove(workspace.ArtifactID);

			ObjectType objectType = Facade.Resolve<ICreateWorkspaceEntityStrategy<ObjectType>>().
				Create(workspace.ArtifactID, new ObjectType());

			TestSession.Current.Dispose();

			using (new AssertionScope())
			{
				Facade.Resolve<IGetByIdStrategy<Workspace>>().Get(workspace.ArtifactID).
					Should().NotBeNull();

				Facade.Resolve<IGetWorkspaceEntityByIdStrategy<ObjectType>>().Get(workspace.ArtifactID, objectType.ArtifactID).
					Should().BeNull();
			}
		}

		[Test]
		public void Dispose_DeletesWorkspaceEntities_WhenCleanUpIsFalse()
		{
			Workspace workspace = null;

			Arrange(x => x.Create(out workspace).CleanUp(false));

			ObjectType objectType = Facade.Resolve<ICreateWorkspaceEntityStrategy<ObjectType>>().
				Create(workspace.ArtifactID, new ObjectType());

			TestSession.Current.Dispose();

			using (new AssertionScope())
			{
				Facade.Resolve<IGetByIdStrategy<Workspace>>().Get(workspace.ArtifactID).
					Should().NotBeNull();

				Facade.Resolve<IGetWorkspaceEntityByIdStrategy<ObjectType>>().Get(workspace.ArtifactID, objectType.ArtifactID).
					Should().BeNull();
			}
		}

		[Test]
		public void Dispose_DoesNotDeleteEntity_WhenCleanUpIsFalse()
		{
			Client client = null;

			Arrange(x => x.Create(out client).CleanUp(false));

			TestSession.Current.Dispose();

			Facade.Resolve<IGetByIdStrategy<Client>>().Get(client.ArtifactID).
				Should().NotBeNull();

			// Clean up:
			Facade.Resolve<IDeleteByIdStrategy<Client>>().
				Delete(client.ArtifactID);
		}

		[Test]
		public void Dispose_DoesNotDeleteEntities_WhenCleanUpIsFalse()
		{
			Client[] clients = null;

			Arrange(x => x.Create(2, out clients).CleanUp(false));

			TestSession.Current.Dispose();

			var getStrategy = Facade.Resolve<IGetByIdStrategy<Client>>();

			using (new AssertionScope())
			{
				getStrategy.Get(clients[0].ArtifactID).Should().NotBeNull();
				getStrategy.Get(clients[1].ArtifactID).Should().NotBeNull();
			}

			// Clean up:
			var deleteStrategy = Facade.Resolve<IDeleteByIdStrategy<Client>>();

			deleteStrategy.Delete(clients[0].ArtifactID);
			deleteStrategy.Delete(clients[1].ArtifactID);
		}

		[Test]
		public void Dispose_DeletesDependentEntities()
		{
			Client client = null;
			Matter matter = null;

			Arrange(x => x
				.Create(out client)
				.Create(new Matter { Client = client })
					.Pick(out matter));

			TestSession.Current.Dispose();

			using (new AssertionScope())
			{
				Facade.Resolve<IGetByIdStrategy<Client>>().Get(client.ArtifactID).
					Should().BeNull();

				Facade.Resolve<IGetByIdStrategy<Matter>>().Get(matter.ArtifactID).
					Should().BeNull();
			}
		}

		[Test]
		public void StartChildSession_InjectsIntoSessionHierarchy()
		{
			TestSession parent = TestSession.Current;

			TestSession child = parent.StartChildSession();

			using (new AssertionScope())
			{
				child.Parent.Should().Be(parent);
				parent.Children.Should().BeEquivalentTo(new[] { child });
			}
		}

		[Test]
		public void Dispose_RemovesFromParentChildren()
		{
			TestSession parent = TestSession.Current;
			TestSession child = parent.StartChildSession();

			child.Dispose();

			parent.Children.Should().BeEmpty();
		}

		[Test]
		public void All()
		{
			Client[] clients = null;

			Arrange(x => x.Create(2, out clients));

			TestSession.Current.All<Client>().
				Should().BeEquivalentTo(clients);
		}

		[Test]
		public void First()
		{
			Client[] clients = null;

			Arrange(x => x.Create(2, out clients));

			TestSession.Current.First<Client>().
				Should().Be(clients.First());
		}

		[Test]
		public void Last()
		{
			Client[] clients = null;

			Arrange(x => x.Create(2, out clients));

			TestSession.Current.Last<Client>().
				Should().Be(clients.Last());
		}
	}
}
