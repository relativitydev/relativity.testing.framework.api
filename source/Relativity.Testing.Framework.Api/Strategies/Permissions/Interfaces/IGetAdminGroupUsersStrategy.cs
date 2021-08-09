using System.Collections.Generic;
using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IGetAdminGroupUsersStrategy
	{
		Task<List<NamedArtifact>> GetAsync(string name);

		Task<List<NamedArtifact>> GetAsync(int groupId);
	}
}
