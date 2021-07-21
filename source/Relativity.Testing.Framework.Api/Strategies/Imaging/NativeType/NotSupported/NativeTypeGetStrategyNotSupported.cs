using System;
using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class NativeTypeGetStrategyNotSupported : INativeTypeGetStrategy
	{
		public NativeType Get(int workspaceId, int nativeTypeID)
		{
			throw new ArgumentException("The method Get NativeType does not support version of Relativity lower than 12.1.");
		}

		public Task<NativeType> GetAsync(int workspaceId, int nativeTypeID)
		{
			throw new ArgumentException("The method Get NativeType does not support version of Relativity lower than 12.1.");
		}
	}
}
