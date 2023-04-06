namespace Relativity.Testing.Framework.Api.Validators
{
	/// <summary>
	/// Validator for artifact ID (not including workspaceId).
	/// </summary>
	internal interface IArtifactIdValidator
	{
		void Validate(int artifactId, string artifactName);
	}
}
