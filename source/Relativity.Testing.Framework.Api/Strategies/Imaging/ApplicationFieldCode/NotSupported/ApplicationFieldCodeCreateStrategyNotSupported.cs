using System;
using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class ApplicationFieldCodeCreateStrategyNotSupported : IApplicationFieldCodeCreateStrategy
	{
		public ApplicationFieldCode Create(int workspaceId, ApplicationFieldCode applicationFieldCode)
		{
			throw new ArgumentException("The method Create ApplicationFieldCode does not support version of Relativity lower than 12.1.");
		}

		public Task<ApplicationFieldCode> CreateAsync(int workspaceId, ApplicationFieldCode applicationFieldCode)
		{
			throw new ArgumentException("The method Create ApplicationFieldCode does not support version of Relativity lower than 12.1.");
		}
	}
}
