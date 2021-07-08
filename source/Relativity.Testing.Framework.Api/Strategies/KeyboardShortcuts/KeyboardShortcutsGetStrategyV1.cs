using System.Collections.Generic;
using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Validators;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class KeyboardShortcutsGetStrategyV1 : IKeyboardShortcutsGetStrategy
	{
		private const string _BASE_GET_URL = "relativity-review/v1/workspaces/";
		private readonly IRestService _restService;
		private readonly IArtifactIdValidator _artifactIdValidator;

		public KeyboardShortcutsGetStrategyV1(IRestService restService, IArtifactIdValidator artifactIdValidator)
		{
			_restService = restService;
			_artifactIdValidator = artifactIdValidator;
		}

		public IEnumerable<KeyboardShortcut> Get(int workspaceId, KeyboardShortcutsIncludeOptions includeOptions = null)
		{
			_artifactIdValidator.Validate(workspaceId, "Workspace");

			var url = BuildUrlWithIncludeOptionsAsQueryParamaters(workspaceId, includeOptions);

			var result = _restService.Get<IEnumerable<KeyboardShortcut>>(url);

			return result;
		}

		public async Task<IEnumerable<KeyboardShortcut>> GetAsync(int workspaceId, KeyboardShortcutsIncludeOptions includeOptions = null)
		{
			_artifactIdValidator.Validate(workspaceId, "Workspace");

			var url = BuildUrlWithIncludeOptionsAsQueryParamaters(workspaceId, includeOptions);

			var result = await _restService.GetAsync<IEnumerable<KeyboardShortcut>>(url).ConfigureAwait(false);

			return result;
		}

		private static string BuildUrlWithIncludeOptionsAsQueryParamaters(int workspaceId, KeyboardShortcutsIncludeOptions includeOptions)
		{
			return includeOptions == null
				? $"{_BASE_GET_URL}{workspaceId}/keyboard-shortcuts"
				: $"{_BASE_GET_URL}{workspaceId}/keyboard-shortcuts?includeSystemShortcuts={includeOptions.IncludeSystemShortcuts}&includeChoiceShortcuts={includeOptions.IncludeChoiceShortcuts}&includeFieldShortcuts={includeOptions.IncludeFieldShortcuts}";
		}
	}
}
