namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of adding value to the instance setting.
	/// </summary>
	internal interface IInstanceSettingAddValueStrategy
	{
		/// <summary>
		/// Add value to existing instance setting.
		/// </summary>
		/// <param name="name">The name of existing instance setting.</param>
		/// <param name="section">The section of existing instance setting.</param>
		/// <param name="value">The value to add.</param>
		/// <param name="delimiter">The delimiter wich split old and new value.</param>
		void AddValue(string name, string section, string value, string delimiter);
	}
}
