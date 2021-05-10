namespace Relativity.Testing.Framework.Api.ObjectManagement
{
	/// <summary>
	/// Represents the field of object query result.
	/// </summary>
	internal class QuerySlimResult
	{
		public int CurrentStartIndex { get; set; }

		public int ResultCount { get; set; }

		public int TotalCount { get; set; }

		public QueryResultField[] Fields { get; set; }

		public QuerySlimObject[] Objects { get; set; }
	}
}
