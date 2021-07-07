using System.Collections.Generic;
using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class KeyboardShortcutsService : IKeyboardShortcutsService
	{
		private readonly IKeyboardShortcutsGetStrategy _getStrategy;

		public KeyboardShortcutsService(IKeyboardShortcutsGetStrategy getStrategy)
		{
			_getStrategy = getStrategy;
		}

		public async Task<IEnumerable<KeyboardShortcut>> GetKeyboardShortcutsAsync(int workspaceId, KeyboardShortcutsIncludeOptions includeOptions = null)
			=> await _getStrategy.GetKeyboardShortcutsAsync(workspaceId, includeOptions).ConfigureAwait(false);
	}
}
