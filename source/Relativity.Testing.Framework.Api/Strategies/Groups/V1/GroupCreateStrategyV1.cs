﻿using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class GroupCreateStrategyV1 : CreateStrategy<Group>
	{
		private readonly IRestService _restService;

		private readonly IRequireStrategy<Client> _clientRequireStrategy;

		public GroupCreateStrategyV1(
			IRestService restService,
			IRequireStrategy<Client> clientRequireStrategy)
		{
			_restService = restService;
			_clientRequireStrategy = clientRequireStrategy;
		}

		protected override Group DoCreate(Group entity)
		{
			entity.Client = _clientRequireStrategy.Require(entity.Client);
			var dto = new GroupDTO(entity);

			GroupResponse response = _restService.Post<GroupResponse>("Relativity-Identity/v1/groups/", dto);
			Group mappedGroup = response.DoMappingFromResponse();

			return mappedGroup;
		}
	}
}
