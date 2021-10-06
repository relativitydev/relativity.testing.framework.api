using System.Collections.Generic;
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
		/// Gets the list of [KeyboardShortcut](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.KeyboardShortcut.html) for the workspace.
		/// </summary>
		/// <param name="workspaceId">The workspace Artifact ID. Don't use admin level context specified by -1.</param>
		/// <param name="includeOptions">Optional parameters indicating wheter to include specified types of shortcuts.</param>
		/// <returns>The collection of [KeyboardShortcut](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.KeyboardShortcut.html) entities.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var includeOptions = new KeyboardShortcutsIncludeOptions
		/// {
		///     IncludeSystemShortcuts = false,
		///     IncludeFieldShortcuts = false
		/// };
		/// var keyboardShortcuts = _keyboardShortcutsService.Get(workspaceId, includeOptions);
		/// </code>
		/// </example>
		IEnumerable<KeyboardShortcut> Get(int workspaceId, KeyboardShortcutsIncludeOptions includeOptions = null);
	}
}
