using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ScriptCreateStrategy : CreateWorkspaceEntityStrategy<Script>
	{
		private readonly IRestService _restService;
		private readonly IGetWorkspaceEntityByIdStrategy<Script> _getWorkspaceEntityByIdStrategy;

		public ScriptCreateStrategy(
			IRestService restService,
			IGetWorkspaceEntityByIdStrategy<Script> getWorkspaceEntityByIdStrategy)
		{
			_restService = restService;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
		}

		protected override Script DoCreate(int workspaceId, Script entity)
		{
			if (entity.ScriptBody == null)
			{
				throw new ArgumentException("This entity should have an ScriptBody.", nameof(entity));
			}

			var entityForRequest = entity.Copy();

			if (entity.Name != null && !entity.ScriptBody.Contains("<name>"))
			{
				entityForRequest = UpdateScriptWithTag(entity, "name", entity.Name);
			}

			if (entity.Description != null && !entity.ScriptBody.Contains("<description>"))
			{
				entityForRequest = UpdateScriptWithTag(entity, "description", entity.Description);
			}

			if (entity.Category != null && !entity.ScriptBody.Contains("<category>"))
			{
				entityForRequest = UpdateScriptWithTag(entity, "category", entity.Category);
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

			entity.ArtifactID = _restService.Post<int>($"Relativity.Scripts/workspace/{workspaceId}/Scripts", dto);

			return _getWorkspaceEntityByIdStrategy.Get(workspaceId, entity.ArtifactID);
		}

		private Script UpdateScriptWithTag(Script entity, string tagName, string value)
		{
			const string scriptTag = "<script>";

			var index = entity.ScriptBody.IndexOf(scriptTag);
			if (index >= 0)
			{
				entity.ScriptBody = entity.ScriptBody.Insert(index + scriptTag.Length, $"<{tagName}>{value}</{tagName}>");
			}

			return entity;
		}
	}
}
