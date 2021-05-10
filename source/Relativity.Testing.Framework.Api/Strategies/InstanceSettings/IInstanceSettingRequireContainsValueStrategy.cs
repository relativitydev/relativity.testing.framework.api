namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of requiring that a value is contained within an instance setting.
	/// </summary>
	internal interface IInstanceSettingRequireContainsValueStrategy
	{
		/// <summary>
		/// Ensures that the instance setting exists and contins the specified value.
		/// </summary>
		/// <param name="name">The name of existing instance setting.</param>
		/// <param name="section">The section of existing instance setting.</param>
		/// <param name="value">The value to ensure is contained in the instance setting.</param>
		/// <param name="delimiter">The delimiter wich split old and new value.</param>
		void RequireContainsValue(string name, string section, string value, string delimiter);
	}
}
