using System.Collections.Generic;
using System.Threading.Tasks;
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

		public async Task<IEnumerable<KeyboardShortcut>> GetAsync(int workspaceId, KeyboardShortcutsIncludeOptions includeOptions = null)
			=> await _keyboardShortcutsGetStrategy.GetAsync(workspaceId, includeOptions).ConfigureAwait(false);
	}
}
