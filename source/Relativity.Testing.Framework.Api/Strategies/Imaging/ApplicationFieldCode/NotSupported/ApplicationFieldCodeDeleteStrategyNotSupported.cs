﻿using System;
using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class ApplicationFieldCodeDeleteStrategyNotSupported : IApplicationFieldCodeDeleteStrategy
	{
		public void Delete(int workspaceId, int applicationFieldCodeId)
		{
			throw new ArgumentException("The method Delete ApplicationFieldCode does not support version of Relativity lower than 12.1.");
		}

		public Task DeleteAsync(int workspaceId, int applicationFieldCodeId)
		{
			throw new ArgumentException("The method Delete ApplicationFieldCode does not support version of Relativity lower than 12.1.");
		}
	}
}
