namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Creates and/or enables a ScriptRunManager Agent to make sure that the environment can run scripts.
	/// </summary>
	internal interface IEnsureEnvironmentCanRunScriptsStrategy
	{
		/// <summary>
		/// Creates and/or enables a ScriptRunManager Agent to make sure that the environment can run scripts.
		/// </summary>
		void EnsureEnvironmentCanRunScripts();
	}
}
