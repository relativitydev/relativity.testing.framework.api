using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the instance settings API service.
	/// </summary>
	/// <example>
	/// <code>
	/// private readonly IInstanceSettingsService _instanceSettingsService;
	/// ...
	/// _instanceSettingsService = relativityFacade.Resolve&#60;IInstanceSettingsService&#62;();
	/// </code>
	/// </example>
	public interface IInstanceSettingsService
	{
		/// <summary>
		/// Creates the specified <see cref="InstanceSetting"/>.
		/// </summary>
		/// <param name="entity">The entity to create.</param>
		/// <returns>The created entity.</returns>
		/// <example>
		/// <code>
		/// InstanceSetting instanceSetting = new InstanceSetting
		/// {
		///     Name = "MySpecialInstanceSetting",
		///     Value = "4",
		///     Description = "This instance setting does something.",
		///     Section = "Relativity.Example",
		///     Machine = "Bert Kreischer",
		///     InitialValue = "An initial value.",
		///     ValueType = InstanceSettingValueType.PositiveInt32
		/// };
		///
		/// InstanceSetting result = _instanceSettingsService.Create(instanceSetting);
		/// </code>
		/// </example>
		InstanceSetting Create(InstanceSetting entity);

		/// <summary>
		/// <para>Creates the specified <see cref="InstanceSetting"/> if it does not exist.</para>
		/// <para>If it does exist, it will be updated instead.</para>
		/// <list type="number">
		/// <item>If [Artifact.ArtifactID](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Artifact.html#Relativity_Testing_Framework_Models_Artifact_ArtifactID) property of <paramref name="entity"/> has positive value, gets entity by ID and returns it.</item>
		/// <item>If [NamedArtifact.Name](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.NamedArtifact.html#Relativity_Testing_Framework_Models_NamedArtifact_Name) and <see cref="InstanceSetting.Section"/> properties of <paramref name="entity"/> have a value, gets entity by name and returns it if it exists.</item>
		/// <item>Otherwise creates a new entity using <see cref="ICreateStrategy{T}"/>.</item>
		/// </list>
		/// </summary>
		/// <param name="entity">The entity to require.</param>
		/// <returns>The entity required.</returns>
		/// <example>
		/// In this example, the instance setting does not exist, so it will be created.
		/// <code>
		/// InstanceSetting instanceSetting = new InstanceSetting
		/// {
		///     Name = "MySpecialInstanceSetting",
		///     Value = "4",
		///     Description = "This instance setting does something.",
		///     Section = "Relativity.Example",
		///     Machine = "SomeMachine",
		///     InitialValue = "An initial value.",
		///     ValueType = InstanceSettingValueType.PositiveInt32
		/// };
		///
		/// InstanceSetting result = _instanceSettingsService.Require(instanceSetting);
		/// </code>
		/// </example>
		/// <example>
		/// In this example, the newly created instance setting will be updated to have the new value property that was passed in.
		/// <code>
		/// InstanceSetting instanceSetting = new InstanceSetting
		/// {
		///     Name = "MySpecialInstanceSetting",
		///     Value = "4",
		///     Description = "This instance setting does something.",
		///     Section = "Relativity.Example",
		///     Machine = "SomeMachine",
		///     InitialValue = "An initial value.",
		///     ValueType = InstanceSettingValueType.PositiveInt32
		/// };
		///
		/// InstanceSetting result = _instanceSettingsService.Create(entity);
		///
		/// result.Value = 111;
		///
		/// InstanceSetting newResult = _instanceSettingsService.Require(entity);
		/// </code>
		/// </example>
		InstanceSetting Require(InstanceSetting entity);

		/// <summary>
		/// <para>Creates the specified <see cref="InstanceSetting"/> if it does not exist.</para>
		/// <para>The instance setting will be looked up by <paramref name="name"/> and <paramref name="section"/>. If it does exist, it will be updated instead.</para>
		/// </summary>
		/// <param name="name">The name of the instance setting.</param>
		/// <param name="section">The section of the instance setting.</param>
		/// <param name="value">The value of the instance setting.</param>
		/// <returns>The instance setting required.</returns>
		/// <example>
		/// <code>
		/// InstanceSetting result = _instanceSettingsService.Require("MyNewInstanceSetting", "TheSectionOfMyNewInstanceSetting", "TheValueOfMyNewInstanceSetting");
		/// </code>
		/// </example>
		InstanceSetting Require(string name, string section, string value);

		/// <summary>
		/// Deletes the <see cref="InstanceSetting"/> by ArtifactId.
		/// </summary>
		/// <param name="id">The artifact ID of the instance settings.</param>
		/// <example>
		/// <code>
		/// _instanceSettingsService.Delete(1234567);
		/// </code>
		/// </example>
		void Delete(int id);

		/// <summary>
		/// Gets the instance settings by the specified ID.
		/// </summary>
		/// <param name="id">The artifact ID of the instance settings.</param>
		/// <returns>The <see cref="InstanceSetting"/> entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// InstanceSetting instancesetting = _instanceSettingsService.Get(1234567);
		/// </code>
		/// </example>
		InstanceSetting Get(int id);

		/// <summary>
		/// Gets the instance setting by the specified name and section.
		/// </summary>
		/// <param name="name">The name of the instance setting.</param>
		/// <param name="section">The section of the instance setting.</param>
		/// <returns>The <see cref="InstanceSetting"/> entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// InstanceSetting instancesetting = _instanceSettingsService.Get("MyNewInstanceSetting", "MyNewSection");
		/// </code>
		/// </example>
		InstanceSetting Get(string name, string section);

		/// <summary>
		/// <para>Updates the <see cref="InstanceSetting"/> by specified entity.</para>
		/// <para>If specified, the ArtifactId on the model will be used to locate the InstanceSetting.</para>
		/// <para>Otherwise, the InstanceSetting will be looked up by Name and Section.</para>
		/// </summary>
		/// <param name="entity">The entity to update.</param>
		/// <example>
		/// <code>
		/// InstanceSetting instanceSetting = new InstanceSetting
		/// {
		///     ArtifactID = 1234567,
		///     Value = "4"
		/// };
		///
		/// InstanceSetting result = _instanceSettingsService.Update(instanceSetting);
		/// </code>
		/// </example>
		/// <example>
		/// <code>
		/// InstanceSetting instanceSetting = new InstanceSetting
		/// {
		///     Name = "InstanceSettingName",
		///     Section = "InstanceSettingSection",
		///     Value = "4"
		/// };
		///
		/// InstanceSetting result = _instanceSettingsService.Update(instanceSetting);
		/// </code>
		/// </example>
		void Update(InstanceSetting entity);

		/// <summary>
		/// Updates value of an existing <see cref="InstanceSetting"/>.
		/// </summary>
		/// <param name="name">The name of the instance setting.</param>
		/// <param name="section">The section of theinstance setting.</param>
		/// <param name="value">The value to set.</param>
		/// <example>
		/// <code>
		/// InstanceSetting instancesetting = _instanceSettingsService.UpdateValue("MyNewInstanceSetting", "MyNewSection", "ThisIsIt");
		/// </code>
		/// </example>
		void UpdateValue(string name, string section, string value);

		/// <summary>
		/// Adds a value to an existing <see cref="InstanceSetting"/>.
		/// </summary>
		/// <param name="name">The name of existing instance setting.</param>
		/// <param name="section">The section of existing instance setting.</param>
		/// <param name="value">The value to add.</param>
		/// <param name="delimiter">The delimiter wich split old and new value.</param>
		/// <example>
		/// In this example, ";ThisIsIt" is added to the value that is in the MyNewSection:MyNewInstanceSetting instance setting.
		/// <code>
		/// InstanceSetting instancesetting = _instanceSettingsService.AddValue("MyNewInstanceSetting", "MyNewSection", "ThisIsIt", ";");
		/// </code>
		/// </example>
		void AddValue(string name, string section, string value, string delimiter);

		/// <summary>
		/// Ensures that the value exists in the <see cref="InstanceSetting"/> with the passed in name and section.
		/// </summary>
		/// <param name="name">The name of existing instance setting.</param>
		/// <param name="section">The section of existing instance setting.</param>
		/// <param name="value">The value to add.</param>
		/// <param name="delimiter">The delimiter wich split old and new value.</param>
		/// <example>
		/// <code>
		/// InstanceSetting instanceSetting = new InstanceSetting
		/// {
		///     Name = "MySpecialInstanceSetting",
		///     Value = "4",
		///     Description = "This instance setting does something.",
		///     Section = "Relativity.Example",
		///     InitialValue = "1;2;3",
		///     ValueType = InstanceSettingValueType.PositiveInt32
		/// };
		///
		/// InstanceSetting result = _instanceSettingsService.Create(instanceSetting);
		///
		/// // After running this line, Relativity.Example:MySpecialInstanceSetting will have the value "1;2;3;4"
		/// _instanceSettingsService.AddValue("MySpecialInstanceSetting", "Relativity.Example", "4", ";");
		/// </code>
		/// </example>
		void RequireContainsValue(string name, string section, string value, string delimiter);

		/// <summary>
		/// <para>Ensures that the value does not exist in the <see cref="InstanceSetting"/> with the passed in name and section.</para>
		/// <para>If the value of the instance setting is an exact match, the entire instance setting will be deleted.</para>
		/// <para>Otherwise, the value will just be removed from the delimited string.</para>
		/// </summary>
		/// <param name="name">The name of existing instance setting.</param>
		/// <param name="section">The section of existing instance setting.</param>
		/// <param name="value">The value to add.</param>
		/// <param name="delimiter">The delimiter wich split old and new value.</param>
		void RequireNotContainsValue(string name, string section, string value, string delimiter);
	}
}
