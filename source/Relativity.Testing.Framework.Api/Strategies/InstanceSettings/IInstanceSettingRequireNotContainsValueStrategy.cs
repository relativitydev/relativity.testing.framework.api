namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of requiring that a value is not contained within an instance setting.
	/// </summary>
	internal interface IInstanceSettingRequireNotContainsValueStrategy
	{
		/// <summary>
		/// Ensures that the instance setting does not contain the specified value.
		/// Will delete the instance setting if this removes the last value in the string.
		/// </summary>
		/// <param name="name">The name of existing instance setting.</param>
		/// <param name="section">The section of existing instance setting.</param>
		/// <param name="value">The value to ensure is not contained in the instance setting.</param>
		/// <param name="delimiter">The delimiter wich split old and new value.</param>
		void RequireNotContainsValue(string name, string section, string value, string delimiter);
	}
}
