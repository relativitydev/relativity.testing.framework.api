namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ArrayResponse<TResult>
	where TResult : class
	{
		public int TotalResultCount { get; set; }

		public int ResultCount { get; set; }

		public TResult[] Results { get; set; }
	}
}
