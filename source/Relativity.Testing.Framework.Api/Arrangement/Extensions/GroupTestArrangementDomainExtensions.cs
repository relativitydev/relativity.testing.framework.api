using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Arrangement
{
	/// <summary>
	/// Provides a set of extension methods for <see cref="TestArrangementDomain{TEntity}"/> of <see cref="Group"/>.
	/// </summary>
	public static class GroupTestArrangementDomainExtensions
	{
		/// <summary>
		/// Adds the specified users to the current group.
		/// </summary>
		/// <param name="domain">The domain.</param>
		/// <param name="users">The users.</param>
		/// <returns>The same domain.</returns>
		public static TestArrangementDomain<Group> Add(this TestArrangementDomain<Group> domain, params User[] users)
		{
			var strategy = domain.Context.Facade.Resolve<IUserAddToGroupStrategy>();

			foreach (User user in users)
			{
				strategy.AddToGroup(user.ArtifactID, domain.Entity.ArtifactID);
			}

			return domain;
		}

		/// <summary>
		/// Adds current group to the specified workspace.
		/// </summary>
		/// <param name="domain">The domain.</param>
		/// <param name="workspace">The workspace.</param>
		/// <returns>The same domain.</returns>
		public static TestArrangementDomain<Group> AddTo(this TestArrangementDomain<Group> domain, Workspace workspace)
		{
			var strategy = domain.Context.Facade.Resolve<IWorkspaceAddToGroupsStrategy>();

			strategy.AddWorkspaceToGroups(workspace.ArtifactID, domain.Entity.ArtifactID);

			return domain;
		}
	}
}
