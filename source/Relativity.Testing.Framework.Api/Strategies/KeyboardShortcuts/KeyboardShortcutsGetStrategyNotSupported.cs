using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies.KeyboardShortcuts
{
	[VersionRange("<12.1")]
	internal class KeyboardShortcutsGetStrategyNotSupported : IKeyboardShortcutsGetStrategy
	{
		public Task<IEnumerable<KeyboardShortcut>> GetKeyboardShortcutsAsync(int workspaceId, KeyboardShortcutsIncludeOptions includeOptions = null)
		{
			throw new ArgumentException("The method GetKeyboardShortcuts does not support version of Relativity lower than 12.1.");
		}
	}
}
