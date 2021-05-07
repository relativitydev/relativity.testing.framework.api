namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of adding the group to admin permissions.
	/// </summary>
	internal interface IAdminAddToGroupsStrategy
	{
		/// <summary>
		/// Adds the group to admin permissions.
		/// </summary>
		/// <param name="groupIds">The collection of group IDs.</param>
		void AddToGroups(params int[] groupIds);

		/// <summary>
		/// Adds the group to admin permissions.
		/// </summary>
		/// <param name="groupNames">The collection of group Names.</param>
		void AddToGroups(params string[] groupNames);
	}
}
