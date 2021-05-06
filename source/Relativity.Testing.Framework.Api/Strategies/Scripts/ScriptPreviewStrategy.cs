using System.Collections.Generic;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ScriptPreviewStrategy : IScriptPreviewStrategy
	{
		private readonly IRestService _restService;

		public ScriptPreviewStrategy(IRestService restService)
		{
			_restService = restService;
		}

		public string PreviewScript(int workspaceId, int scriptId, List<ScriptInput> inputs = null, double timeZoneOffset = 0)
		{
			if (inputs == null)
			{
				inputs = new List<ScriptInput>();
			}

			var dto = new
			{
				inputs,
				timeZoneOffset
			};

			return _restService.Post<string>($"Relativity.Scripts/workspace/{workspaceId}/Scripts/{scriptId}/preview", dto);
		}
	}
}
