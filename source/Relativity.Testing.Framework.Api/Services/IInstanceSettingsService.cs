using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the instance settings API service.
	/// </summary>
	public interface IInstanceSettingsService
	{
		/// <summary>
		/// Creates the specified instance settings.
		/// </summary>
		/// <param name="entity">The entity to create.</param>
		/// <returns>The created entity.</returns>
		InstanceSetting Create(InstanceSetting entity);

		/// <summary>
		/// Requires the specified InstanceSetting.
		/// <list type="number">
		/// <item>If <see cref="Artifact.ArtifactID"/> property of <paramref name="entity"/> has positive value, gets entity by ID and returns it.</item>
		/// <item>If <see cref="NamedArtifact.Name"/> and <see cref="InstanceSetting.Section"/> properties of <paramref name="entity"/> have a value, gets entity by name and returns it if it exists.</item>
		/// <item>Otherwise creates a new entity using <see cref="ICreateStrategy{T}"/>.</item>
		/// </list>
		/// </summary>
		/// <param name="entity">The entity to require.</param>
		/// <returns>The entity required.</returns>
		InstanceSetting Require(InstanceSetting entity);

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

		/// <summary>
		/// Deletes the instance settings by ID.
		/// </summary>
		/// <param name="id">The artifact ID of the instance settings.</param>
		void Delete(int id);

		/// <summary>
		/// Gets the instance settings by the specified ID.
		/// </summary>
		/// <param name="id">The artifact ID of the instance settings.</param>
		/// <returns>The <see cref="InstanceSetting"/> entity or <see langword="null"/>.</returns>
		InstanceSetting Get(int id);

		/// <summary>
		/// Gets the instance setting by the specified name and section.
		/// </summary>
		/// <param name="name">The name of the instance setting.</param>
		/// <param name="section">The section of the instance setting.</param>
		/// <returns>The <see cref="InstanceSetting"/> entity or <see langword="null"/>.</returns>
		InstanceSetting Get(string name, string section);

		/// <summary>
		/// Updates the instance setting by specified entity.
		/// </summary>
		/// <param name="entity">The entity to update.</param>
		void Update(InstanceSetting entity);

		/// <summary>
		/// Updates value of existing instance setting.
		/// </summary>
		/// <param name="name">The name of existing instance setting.</param>
		/// <param name="section">The section of existing instance setting.</param>
		/// <param name="value">The value to set.</param>
		void UpdateValue(string name, string section, string value);

		/// <summary>
		/// Add value to existing instance setting.
		/// </summary>
		/// <param name="name">The name of existing instance setting.</param>
		/// <param name="section">The section of existing instance setting.</param>
		/// <param name="value">The value to add.</param>
		/// <param name="delimiter">The delimiter wich split old and new value.</param>
		void AddValue(string name, string section, string value, string delimiter);

		/// <summary>
		/// Ensures that the value exists in the instance setting with the passed in name and section.
		/// </summary>
		/// <param name="name">The name of existing instance setting.</param>
		/// <param name="section">The section of existing instance setting.</param>
		/// <param name="value">The value to add.</param>
		/// <param name="delimiter">The delimiter wich split old and new value.</param>
		void RequireContainsValue(string name, string section, string value, string delimiter);

		/// <summary>
		/// Ensures that the value does not exist in the instance setting with the passed in name and section.
		/// </summary>
		/// <param name="name">The name of existing instance setting.</param>
		/// <param name="section">The section of existing instance setting.</param>
		/// <param name="value">The value to add.</param>
		/// <param name="delimiter">The delimiter wich split old and new value.</param>
		void RequireNotContainsValue(string name, string section, string value, string delimiter);
	}
}
