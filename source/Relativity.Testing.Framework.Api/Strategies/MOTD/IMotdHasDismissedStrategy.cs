using System.Threading.Tasks;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IMotdHasDismissedStrategy
	{
		bool HasDismissed(int? userId = null);

		Task<bool> HasDismissedAsync(int? userId = null);
	}
}
