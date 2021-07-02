using Relativity.Testing.Framework.Api.Models;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IUpdateBatchSetStrategy
	{
		BatchSet Update(int workspaceId, BatchSet entity, UserCredentials userCredentials = null);
	}
}
