using Relativity.Testing.Framework.Api.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IDeleteBatchSetStrategy
	{
		void Delete(int workspaceId, int entityId, UserCredentials userCredentials = null);
	}
}
