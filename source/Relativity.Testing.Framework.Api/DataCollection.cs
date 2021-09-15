namespace Relativity.Testing.Framework.Api
{
	/// <summary>
	/// Represents the Application Insights data collection state of the testing framework.
	/// </summary>
	public enum DataCollection
	{
		/// <summary>
		/// All available data is collected, including
		/// potentially sensitive data (e.g. stack traces).
		/// </summary>
		All,

		/// <summary>
		/// Only non-identifying information is collected.
		/// For example, API usage metrics.
		/// </summary>
		UsageOnly,

		/// <summary>
		/// No data is collected by the framework.
		/// </summary>
		None
	}
}
