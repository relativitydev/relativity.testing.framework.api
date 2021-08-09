using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IMatterGetEligibleClientsStrategy
	{
		Task<ArtifactIdNamePair[]> GetAllAsync();
	}
}
