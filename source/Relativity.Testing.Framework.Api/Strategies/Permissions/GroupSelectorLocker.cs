using Nito.AsyncEx;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal static class GroupSelectorLocker
	{
		public static readonly AsyncLock Locker = new AsyncLock();
	}
}
