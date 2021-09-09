using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IKeyboardShortcutsGetStrategy
	{
		IEnumerable<KeyboardShortcut> Get(int workspaceId, KeyboardShortcutsIncludeOptions includeOptions = null);
	}
}
