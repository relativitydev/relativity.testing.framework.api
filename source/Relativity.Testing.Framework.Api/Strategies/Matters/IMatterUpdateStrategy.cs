using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IMatterUpdateStrategy
	{
		void Update(Matter entity, bool restrictedUpdate = false);
	}
}
