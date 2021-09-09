using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of getting the users in a group with admin permissions.
	/// </summary>
	internal interface IGetAdminGroupUsersStrategy
	{
		/// <summary>
		/// Gets the users in a group with admin permissions.
		/// </summary>
		/// <param name="groupId">The group ID.</param>
		/// <returns>A List of <see cref="User"/>s or <see langword="null"/> if the admin is not added to the group.</returns>
		List<NamedArtifact> Get(int groupId);

		/// <summary>
		/// Gets the users in a group with admin permissions.
		/// </summary>
		/// <param name="name">The group name.</param>
		/// <returns>A List of <see cref="User"/>s or <see langword="null"/> if the admin is not added to the group.</returns>
		List<NamedArtifact> Get(string name);
	}
}
