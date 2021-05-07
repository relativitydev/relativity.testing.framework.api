using System;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ScriptUpdateStrategy : IUpdateWorkspaceEntityStrategy<Script>
	{
		private readonly IRestService _restService;

		public ScriptUpdateStrategy(IRestService restService)
		{
			_restService = restService;
		}

		public void Update(int workspaceId, Script entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			if (entity.ScriptBody == null)
			{
				throw new ArgumentException("This entity should have an ScriptBody.", nameof(entity));
			}

			var nameFromScript = new Regex(@"<name>(.*)</name>").Match(entity.ScriptBody)?.Groups[1]?.Value;
			var descriptionFromScript = new Regex(@"<description>(.*)</description>").Match(entity.ScriptBody)?.Groups[1]?.Value;
			var categoryFromScript = new Regex(@"<category>(.*)</category>").Match(entity.ScriptBody)?.Groups[1]?.Value;

			var entityForRequest = entity.Copy();

			if (entity.Name != null && nameFromScript != entity.Name)
			{
				entityForRequest.ScriptBody = entityForRequest.ScriptBody.Replace($"<name>{nameFromScript}</name>", $"<name>{entity.Name}</name>");
			}

			if (entity.Description != null && descriptionFromScript != entity.Description)
			{
				entityForRequest.ScriptBody = entityForRequest.ScriptBody.Replace($"<description>{descriptionFromScript}</description>", $"<description>{entity.Description}</description>");
			}

			if (entity.Category != null && categoryFromScript != entity.Category)
			{
				entityForRequest.ScriptBody = entityForRequest.ScriptBody.Replace($"<category>{categoryFromScript}</category>", $"<category>{entity.Category}</category>");
			}

			var jobject = JObject.FromObject(entityForRequest);
			jobject.Properties()
				.Where(x => !x.Name.Contains("ScriptBody") && !x.Name.Contains("RelativityApplications"))
				.ToList()
				.ForEach(x => x.Remove());

			var dto = new
			{
				ScriptRequest = jobject
			};

			_restService.Put($"Relativity.Scripts/workspace/{workspaceId}/Scripts/{entity.ArtifactID}", dto);
		}
	}
}
