﻿using System;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class TabUpdateStrategyPreOsier : IUpdateWorkspaceEntityStrategy<Tab>
	{
		private readonly IRestService _restService;

		public TabUpdateStrategyPreOsier(IRestService restService)
		{
			_restService = restService;
		}

		public void Update(int workspaceId, Tab entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			TabDtoPreOsier tabDto = new TabDtoPreOsier
			{
				Tab = TabRequestPreOsier.FromTab(workspaceId, entity)
			};

			_restService.Put($"Relativity.Tabs/workspace/{workspaceId}/tabs", tabDto);
		}
	}
}
