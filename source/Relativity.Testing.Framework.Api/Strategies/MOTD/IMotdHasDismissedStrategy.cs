namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IMotdHasDismissedStrategy
	{
		bool HasDismissed(int? userId = null);
	}
}
