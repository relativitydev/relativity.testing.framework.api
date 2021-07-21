using System.Threading.Tasks;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IMotdIsTextOnlyStrategy
	{
		bool IsTextOnly();

		Task<bool> IsTextOnlyAsync();
	}
}
