﻿using System.Collections.Generic;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class ClientGetEligibleStatusesStrategyPreOsier : IClientGetEligibleStatusesStrategy
	{
		private readonly IRestService _restService;

		public ClientGetEligibleStatusesStrategyPreOsier(IRestService restService)
		{
			_restService = restService;
		}

		public IEnumerable<NamedArtifact> Get()
		{
			return _restService.Get<IEnumerable<NamedArtifact>>("/Relativity.Services.Client.IClientModule/Client%20Manager/GetStatusChoicesForClientAsync");
		}
	}
}
