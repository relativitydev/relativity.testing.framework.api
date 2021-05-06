using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of instance setting requirement.
	/// </summary>
	internal interface IInstanceSettingRequireByDefaultArgumentsStrategy
	{
		/// <summary>
		/// Requires the specified instance setting.
		/// Returns existing object if the <paramref name="name"/> and <paramref name="section"/> has the values to be able to get the instance setting;
		/// otherwise creates a new entity.
		/// </summary>
		/// <param name="name">The name of instance setting to require.</param>
		/// <param name="section">The section of instance setting to require.</param>
		/// <param name="value">The value of instance setting to require.</param>
		/// <returns>The instance setting required.</returns>
		InstanceSetting Require(string name, string section, string value);
	}
}
