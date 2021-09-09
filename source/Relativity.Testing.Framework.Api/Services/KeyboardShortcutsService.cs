using System.Collections.Generic;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class KeyboardShortcutsService : IKeyboardShortcutsService
	{
		private readonly IKeyboardShortcutsGetStrategy _keyboardShortcutsGetStrategy;

		public KeyboardShortcutsService(IKeyboardShortcutsGetStrategy getStrategy)
		{
			_keyboardShortcutsGetStrategy = getStrategy;
		}

		public IEnumerable<KeyboardShortcut> Get(int workspaceId, KeyboardShortcutsIncludeOptions includeOptions = null)
			=> _keyboardShortcutsGetStrategy.Get(workspaceId, includeOptions);
	}
}
