using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class OcrProfileGetByIdStrategy : ObjectQueryGetByIdWorkspaceEntityStrategy<OcrProfile>
	{
		public OcrProfileGetByIdStrategy(IObjectService objectService)
			: base(objectService)
		{
		}
	}
}
