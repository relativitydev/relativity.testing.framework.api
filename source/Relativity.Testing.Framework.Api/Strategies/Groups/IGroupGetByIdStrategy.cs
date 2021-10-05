using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IGroupGetByIdStrategy
	{
		Group Get(int id, bool includeMetadata = false, bool includeActions = false);
	}
}
