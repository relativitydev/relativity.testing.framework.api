using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IMatterUpdateStrategy
	{
		Matter Update(Matter entity, bool restrictedUpdate = false);
	}
}
