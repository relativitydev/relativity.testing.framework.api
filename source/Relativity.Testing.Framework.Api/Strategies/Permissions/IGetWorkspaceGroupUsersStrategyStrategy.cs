using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of getting the users in a group with workspace permissions.
	/// </summary>
	internal interface IGetWorkspaceGroupUsersStrategy
	{
		/// <summary>
		/// Gets the users in a group with workspace permissions.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="groupId">The group ID.</param>
		/// <returns>A List of <see cref="User"/>s or <see langword="null"/> if the workspace is not added to the group.</returns>
		List<NamedArtifact> Get(int workspaceId, int groupId);

		/// <summary>
		/// Gets the users in a group with workspace permissions.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="groupName">The group Name.</param>
		/// <returns>A List of <see cref="User"/>s or <see langword="null"/> if the workspace is not added to the group.</returns>
		List<NamedArtifact> Get(int workspaceId, string groupName);
	}
}
