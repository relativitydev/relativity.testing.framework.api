﻿using System;
using System.Linq;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Arrangement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Arrangement
{
	/// <summary>
	/// Provides a set of extension methods for <see cref="TestArrangementDomain{TEntity}"/> of <see cref="Workspace"/>.
	/// </summary>
	public static class WorkspaceTestArrangementDomainExtensions
	{
		/// <summary>
		/// Adds the specified groups to the current workspace.
		/// </summary>
		/// <param name="domain">The domain.</param>
		/// <param name="groups">The groups.</param>
		/// <returns>The same domain.</returns>
		public static TestArrangementDomain<Workspace> Add(this TestArrangementDomain<Workspace> domain, params Group[] groups)
		{
			var service = domain.Context.Facade.Resolve<IWorkspacePermissionService>();

			service.AddWorkspaceToGroups(domain.Entity.ArtifactID, groups.Select(x => x.ArtifactID).ToArray());

			return domain;
		}

		/// <summary>
		/// Sets the specified group permissions to the current workspace.
		/// </summary>
		/// <param name="domain">The domain.</param>
		/// <param name="group">The group.</param>
		/// <param name="changeset">The permissions changeset.</param>
		/// <returns>The same domain.</returns>
		public static TestArrangementDomain<Workspace> SetGroupPermissions(this TestArrangementDomain<Workspace> domain, Group group, GroupPermissionsChangeset changeset)
		{
			var service = domain.Context.Facade.Resolve<IWorkspacePermissionService>();
			service.SetWorkspaceGroupPermissions(domain.Entity.ArtifactID, group.ArtifactID, changeset);

			return domain;
		}

		/// <summary>
		/// Executes the specified workspace arrange action using <see cref="TestArrangementDomain{TEntity}"/> for the current workspace.
		/// </summary>
		/// <param name="domain">The domain.</param>
		/// <param name="arrangementAction">The workspace domain arrange action.</param>
		/// <returns>The same domain.</returns>
		public static TestArrangementDomain<Workspace> ArrangeWorkspace(this TestArrangementDomain<Workspace> domain, Action<TestArrangementDomain<Workspace>> arrangementAction)
		{
			var entityCreator = new WorkspaceLevelEntityCreator(domain.Context.Facade, domain.Entity.ArtifactID);

			TestArrangementContext context = new TestArrangementContext(domain.Context.Facade, domain.Context.Session, entityCreator);

			var workspaceDomain = new TestArrangementDomain<Workspace>(domain.Entity, context);

			arrangementAction?.Invoke(workspaceDomain);

			return domain;
		}
	}
}
