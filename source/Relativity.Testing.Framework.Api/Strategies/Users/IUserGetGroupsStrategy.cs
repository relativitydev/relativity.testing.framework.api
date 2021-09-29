using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IUserGetGroupsStrategy
	{
		List<NamedArtifact> GetGroups(int userId);
	}
}
