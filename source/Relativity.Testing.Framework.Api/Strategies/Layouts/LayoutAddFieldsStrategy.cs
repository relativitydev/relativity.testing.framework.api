using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.0")]
	internal class LayoutAddFieldsStrategy : ILayoutAddFieldsStrategy
	{
		private readonly IRestService _restService;
		private readonly IGetWorkspaceEntityByNameStrategy<Layout> _layoutGetByNameStrategy;
		private readonly ILayoutGetCategoriesStrategy _layoutGetCategoriesStrategy;

		public LayoutAddFieldsStrategy(IRestService restService, IGetWorkspaceEntityByNameStrategy<Layout> layoutGetByNameStrategy, ILayoutGetCategoriesStrategy layoutGetCategoriesStrategy)
		{
			_restService = restService;
			_layoutGetByNameStrategy = layoutGetByNameStrategy;
			_layoutGetCategoriesStrategy = layoutGetCategoriesStrategy;
		}

		public void AddFields(int workspaceId, Layout entity, List<CategoryField> categoryFields)
		{
			if (workspaceId == 0)
			{
				throw new ArgumentException("WorkspaceId must be -1 or a valid workspace artifact id.");
			}

			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			if (entity.ArtifactID == 0)
			{
				if (entity.Name != null)
				{
					entity = _layoutGetByNameStrategy.Get(workspaceId, entity.Name);
				}
				else
				{
					throw new ArgumentException($"{typeof(Layout)} model must have a valid ArtifactId or Name set.");
				}
			}

			List<Category> categories = _layoutGetCategoriesStrategy.GetCategories(workspaceId, entity);

			RemoveExistingFields(categories, categoryFields);

			if (categoryFields.Count == 0)
			{
				throw new ArgumentNullException($"{typeof(List<CategoryField>)} No fields present after removing existing fields from the layout.");
			}

			Category defaultCategory = categories.First();
			FillInLayoutProperties(entity.ArtifactID, defaultCategory, categoryFields);

			SaveFieldsAndCustomTextRequest request = new SaveFieldsAndCustomTextRequest
			{
				AppId = workspaceId,
				LayoutId = entity.ArtifactID,
				FieldsToTrack = categoryFields
			};

			DoSaveFieldsAndCustomTextRequest(request);
		}

		internal static void RemoveExistingFields(List<Category> categories, List<CategoryField> categoryFields)
		{
			List<Element> existingElements = new List<Element>();
			categories.ForEach(x => x.Elements.ForEach(y => existingElements.AddRange(y.Elements)));

			categoryFields.RemoveAll(x => existingElements.Exists(y => y.FieldId == x.FieldArtifactID));
		}

		internal static void FillInLayoutProperties(int layoutArtifactId, Category category, List<CategoryField> categoryFields)
		{
			int largestRow = category.Elements.First().Elements.Last().Row;

			foreach (CategoryField categoryField in categoryFields)
			{
				largestRow += 1;

				categoryField.LayoutArtifactID = layoutArtifactId;
				categoryField.Row = largestRow;
				categoryField.CategoryID = category.Elements.First().CategoryID;
			}
		}

		private static string BuildLayoutBuildExceptionMessage(int workspaceId, int layoutId, int categoryId, string responseMessage)
		{
			string message = $@"Failed to add fields to Layout Category (Workspace ID: {workspaceId}, Layout ID: {layoutId}, Category ID: {categoryId}).
				Make sure that the ObjectType used for the Field and Layout are the same.
				REST call responded with '{responseMessage}'.
				Check the errors tab in Relativity for more information.";

			return message;
		}

		internal void DoSaveFieldsAndCustomTextRequest(SaveFieldsAndCustomTextRequest request)
		{
			SaveFieldsAndCustomTextResponse response = _restService.Post<SaveFieldsAndCustomTextResponse>("Relativity.Services.Layout.Interfaces.ILayoutModule/LayoutBuilderService/SaveFieldsAndCustomText", request);

			if (!response.Success)
			{
				// ToDo: We know that this is a single category right now, but this could in theory be any number on the layout,
				// and I'm not sure that we can even know which combination failed it without grabbing errors from Relativity.
				int categoryId = request.FieldsToTrack.First().CategoryID;
				string message = BuildLayoutBuildExceptionMessage(request.AppId, request.LayoutId, categoryId, response.Message);
				throw new HttpRequestException(message);
			}
		}
	}
}
