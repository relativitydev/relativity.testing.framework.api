﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class PermissionService : IPermissionService
	{
		private readonly IGetWorkspaceGroupUsersStrategy _getWorkspaceGroupUsers;

		private readonly IGetAdminGroupUsersStrategy _getAdminGroupUsers;

		public PermissionService(
			IAdminPermissionService adminPermissionService,
			IWorkspacePermissionService workspacePermissionService,
			IItemPermissionService itemPermissionService,
			IGetWorkspaceGroupUsersStrategy getWorkspaceGroupUsers,
			IGetAdminGroupUsersStrategy getAdminGroupUsers)
		{
			AdminPermissionService = adminPermissionService;
			WorkspacePermissionService = workspacePermissionService;
			ItemPermissionService = itemPermissionService;
			_getWorkspaceGroupUsers = getWorkspaceGroupUsers;
			_getAdminGroupUsers = getAdminGroupUsers;
		}

		public IAdminPermissionService AdminPermissionService { get; }

		public IWorkspacePermissionService WorkspacePermissionService { get; }

		public IItemPermissionService ItemPermissionService { get; }

		public List<NamedArtifact> GetWorkspaceGroupUsers(int workspaceId, int groupId)
			=> _getWorkspaceGroupUsers.Get(workspaceId, groupId);

		public List<NamedArtifact> GetWorkspaceGroupUsers(int workspaceId, string groupName)
			=> _getWorkspaceGroupUsers.Get(workspaceId, groupName);

		public async Task<List<NamedArtifact>> GetWorkspaceGroupUsersAsync(int workspaceId, int groupId)
			=> await _getWorkspaceGroupUsers.GetAsync(workspaceId, groupId).ConfigureAwait(false);

		public List<NamedArtifact> GetAdminGroupUsers(int groupId)
			=> _getAdminGroupUsers.Get(groupId);

		public List<NamedArtifact> GetAdminGroupUsers(string groupName)
			=> _getAdminGroupUsers.Get(groupName);

		public async Task<List<NamedArtifact>> GetAdminGroupUsersAsync(int groupId)
			=> await _getAdminGroupUsers.GetAsync(groupId).ConfigureAwait(false);
	}
}
