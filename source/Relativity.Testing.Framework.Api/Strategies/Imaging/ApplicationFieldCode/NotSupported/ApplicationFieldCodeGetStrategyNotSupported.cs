using System;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class ApplicationFieldCodeGetStrategyNotSupported : IApplicationFieldCodeGetStrategy
	{
		public ApplicationFieldCode Get(int workspaceId, int applicationFieldCodeId)
		{
			throw new ArgumentException("The method Get ApplicationFieldCode does not support version of Relativity lower than 12.1.");
		}
	}
}
