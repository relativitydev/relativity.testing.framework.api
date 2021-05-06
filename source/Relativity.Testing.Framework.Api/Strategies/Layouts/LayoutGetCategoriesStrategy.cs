using System;
using System.Collections.Generic;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.0")]
	internal class LayoutGetCategoriesStrategy : ILayoutGetCategoriesStrategy
	{
		private readonly IRestService _restService;
		private readonly IGetWorkspaceEntityByNameStrategy<Layout> _layoutGetByNameStrategy;

		public LayoutGetCategoriesStrategy(IRestService restService, IGetWorkspaceEntityByNameStrategy<Layout> layoutGetByNameStrategy)
		{
			_restService = restService;
			_layoutGetByNameStrategy = layoutGetByNameStrategy;
		}

		public List<Category> GetCategories(int workspaceId, Layout entity)
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

			ReadSingleAsyncRequest body = new ReadSingleAsyncRequest
			{
				WorkspaceID = workspaceId,
				LayoutID = entity.ArtifactID
			};

			ReadSingleAsyncResult result = _restService.Post<ReadSingleAsyncResult>("Relativity.Services.Layout.Interfaces.ILayoutModule/LayoutRenderService/ReadSingleAsync", body);
			return result.Groups;
		}
	}
}
