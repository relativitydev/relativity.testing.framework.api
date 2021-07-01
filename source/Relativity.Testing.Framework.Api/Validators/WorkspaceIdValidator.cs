using System;

namespace Relativity.Testing.Framework.Api.Validators
{
	internal class WorkspaceIdValidator : IWorkspaceIdValidator
	{
		public void Validate(int workspaceId)
		{
			if (workspaceId < -1 || workspaceId == 0)
			{
				throw new ArgumentException("Workspace ID should be greater than zero or equal to -1 for admin context.");
			}
		}
	}
}
