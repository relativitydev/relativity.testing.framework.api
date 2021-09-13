using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface INativeTypeGetStrategy
	{
		NativeType Get(int workspaceId, int nativeTypeID);
	}
}
