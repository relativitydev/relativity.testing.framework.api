namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of updating value of the instance setting.
	/// </summary>
	internal interface IInstanceSettingUpdateValueStrategy
	{
		/// <summary>
		/// Updates value of existing instance setting.
		/// </summary>
		/// <param name="name">The name of existing instance setting.</param>
		/// <param name="section">The section of existing instance setting.</param>
		/// <param name="value">The value to set.</param>
		void UpdateValue(string name, string section, string value);
	}
}
