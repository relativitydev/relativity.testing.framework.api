using System.Collections.Generic;
using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the Keyboard Shortcuts API service.
	/// </summary>
	/// <example>
	/// <code>
	/// _keyboardShortcutsService = relativityFacade.Resolve&lt;IKeyboardShortcutsService&gt;();
	/// </code>
	/// </example>
	public interface IKeyboardShortcutsService
	{
		/// <summary>
		/// Asynchronously gets the list of <see cref="KeyboardShortcut"/> for the workspace.
		/// </summary>
		/// <param name="workspaceId">The workspace Artifact ID. Don't use admin level context specified by -1.</param>
		/// <param name="includeOptions">Optional parameters indicating wheter to include specified types of shortcuts.</param>
		/// <returns>The task with collection of <see cref="KeyboardShortcut"/> entities.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var includeOptions = new KeyboardShortcutsIncludeOptions
		/// {
		/// 	IncludeSystemShortcuts = false,
		/// 	IncludeFieldShortcuts = false
		/// };
		/// var keyboardShortcuts = await _keyboardShortcutsService.GetKeyboardShortcutsAsync(workspaceId, includeOptions).ConfigureAwait(false);
		/// </code>
		/// </example>
		Task<IEnumerable<KeyboardShortcut>> GetKeyboardShortcutsAsync(int workspaceId, KeyboardShortcutsIncludeOptions includeOptions = null);
	}
}
