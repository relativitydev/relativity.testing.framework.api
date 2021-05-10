using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class FieldUpdateStrategy<TFieldModel> : IUpdateWorkspaceEntityStrategy<TFieldModel>
		where TFieldModel : Field
	{
		private readonly IRestService _restService;

		public FieldUpdateStrategy(IRestService restService)
		{
			_restService = restService;
		}

		public void Update(int workspaceId, TFieldModel entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			string url = $"Relativity.Fields/workspace/{workspaceId}/{entity.FieldType.ToString().ToLower()}fields/{entity.ArtifactID}";

			var dto = new
			{
				fieldRequest = entity
			};

			var settings = new JsonSerializerSettings
			{
				ContractResolver = FieldJsonResolver.For<TFieldModel>(),
				NullValueHandling = NullValueHandling.Ignore
			};

			var dtoAsString = JsonConvert.SerializeObject(dto, settings);

			if (entity.PropagateTo != null)
			{
				dtoAsString = ConvertPoplateToObject(dtoAsString);
			}

			dtoAsString = RemoveArtifactIdProperty(dtoAsString);

			_restService.Put(url, dtoAsString);
		}

		private string ConvertPoplateToObject(string dtoAsString)
		{
			var jobject = JObject.Parse(dtoAsString);

			if (jobject == null)
				throw new System.Exception();

			var viewableItems = jobject["fieldRequest"]["PropagateTo"]["ViewableItems"];

			jobject["fieldRequest"]["PropagateTo"] = viewableItems.Any() ? viewableItems : null;

			return jobject.RemoveNullAndEmptyProperties().ToString();
		}

		private string RemoveArtifactIdProperty(string dtoAsString)
		{
			var jobject = JObject.Parse(dtoAsString);

			if (jobject == null)
				throw new System.Exception();

			jobject["fieldRequest"]["ArtifactID"] = null;

			return jobject.RemoveNullAndEmptyProperties().ToString();
		}
	}
}
