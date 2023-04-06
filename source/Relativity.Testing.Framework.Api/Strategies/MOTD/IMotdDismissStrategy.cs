namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IMotdDismissStrategy
	{
		void Dismiss(int? userId = null);

		void Dismiss(string emailAddress);
	}
}
