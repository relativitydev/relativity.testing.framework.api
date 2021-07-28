using System.Collections.Generic;
using Relativity.Testing.Framework.Api.Strategies;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the toggle API service.
	/// </summary>
	public interface IToggleService
	{
		/// <summary>
		/// Gets all toggles.
		/// </summary>
		/// <returns>The collection of <see cref="Toggle"/> entities.</returns>
		public Toggle[] GetAll();

		/// <summary>
		/// Gets the toggle by the specified name.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns>The <see cref="Toggle"/> entity or <see langword="null"/>.</returns>
		public Toggle Get(string name);

		/// <summary>
		/// Requires the specified toggle.
		/// <list type="number">
		/// <item>If <see cref="Toggle.Name"/> property of <paramref name="toggle"/> have a value, gets entity by name and updates it if it exists.</item>
		/// <item>Otherwise creates a new entity using <see cref="ICreateWorkspaceEntityStrategy{T}"/>.</item>
		/// </list>
		/// </summary>
		/// <param name="toggle">The entity to require.</param>
		public void Require(Toggle toggle);
	}
}
