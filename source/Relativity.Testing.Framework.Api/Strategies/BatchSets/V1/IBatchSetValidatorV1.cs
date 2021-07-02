using Relativity.Testing.Framework.Api.Models;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IBatchSetValidatorV1
	{
		void ValidateWorkspaceId(int workspaceId);

		void ValidateWorkspaceIdAndExistingBatchSetId(int workspaceId, int batchSetId, UserCredentials userCredentials = null);

		void ValidateUpdateArguments(int workspaceId, BatchSet batchSet, UserCredentials userCredentials = null);
	}
}
