using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class FieldCreateStrategy<TFieldModel> : CreateWorkspaceEntityStrategy<TFieldModel>
		where TFieldModel : Field
	{
		private readonly IRestService _restService;
		private readonly IGetWorkspaceEntityByIdStrategy<TFieldModel> _getFieldByIdStrategy;
		private readonly IGetWorkspaceEntityByNameStrategy<ObjectType> _getObjectTypeByNameStrategy;

		public FieldCreateStrategy(
			IRestService restService,
			IGetWorkspaceEntityByIdStrategy<TFieldModel> getFieldByIdStrategy,
			IGetWorkspaceEntityByNameStrategy<ObjectType> getObjectTypeByNameStrategy)
		{
			_restService = restService;
			_getFieldByIdStrategy = getFieldByIdStrategy;
			_getObjectTypeByNameStrategy = getObjectTypeByNameStrategy;
		}

		protected override TFieldModel DoCreate(int workspaceId, TFieldModel entity)
		{
			var entityToCreate = RequireFieldFields(workspaceId, entity);

			string url = $"Relativity.Fields/workspace/{workspaceId}/{entityToCreate.FieldType.ToString().ToLower()}fields";

			var dto = new
			{
				fieldRequest = entityToCreate
			};

			var settings = new JsonSerializerSettings
			{
				ContractResolver = FieldJsonResolver.For<TFieldModel>(),
				NullValueHandling = NullValueHandling.Ignore
			};

			var dtoAsString = JsonConvert.SerializeObject(dto, settings);

			if (entity.PropagateTo != null)
			{
				dtoAsString = ConvertPopulateToObject(dtoAsString);
			}

			int artifactId = _restService.Post<int>(url, dtoAsString);

			return _getFieldByIdStrategy.Get(workspaceId, artifactId);
		}

		private TFieldModel RequireFieldFields(int workspaceId, TFieldModel entity)
		{
			var entityToCreate = entity.Copy();

			if (entityToCreate.Name == null)
			{
				entityToCreate.Name = Randomizer.GetString($"RTF {0}");
			}

			if (entityToCreate.ObjectType == null)
			{
				entityToCreate.ObjectType = workspaceId != -1
					? _getObjectTypeByNameStrategy.Get(workspaceId, "Document")
					: _getObjectTypeByNameStrategy.Get(workspaceId, "Scope");
			}

			return entityToCreate;
		}

		private string ConvertPopulateToObject(string dtoAsString)
		{
			var jobject = JObject.Parse(dtoAsString);

			if (jobject == null)
				throw new System.Exception();

			var viewableItems = jobject["fieldRequest"]["PropagateTo"]["ViewableItems"];

			jobject["fieldRequest"]["PropagateTo"] = viewableItems.Any() ? viewableItems : null;

			return jobject.RemoveNullAndEmptyProperties().ToString();
		}
	}
}
