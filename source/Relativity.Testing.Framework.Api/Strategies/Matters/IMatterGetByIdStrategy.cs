using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IMatterGetByIdStrategy
	{
		Matter Get(int id, bool withExtendedMetadata = false);
	}
}
