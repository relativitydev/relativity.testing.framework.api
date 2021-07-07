using System.Collections.Generic;
using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IKeyboardShortcutsGetStrategy
	{
		Task<IEnumerable<KeyboardShortcut>> GetKeyboardShortcutsAsync(int workspaceId, KeyboardShortcutsIncludeOptions includeOptions = null);
	}
}
