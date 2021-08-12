using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IMatterGetByIdStrategy : IGetByIdStrategy<Matter>
	{
		Matter GetWithExtendedMetadata(int id);
	}
}
