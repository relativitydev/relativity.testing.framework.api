using System.Threading.Tasks;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IMotdDismissStrategy
	{
		void Dismiss(int? userId = null);

		Task DismissAsync(int? userId = null);
	}
}
