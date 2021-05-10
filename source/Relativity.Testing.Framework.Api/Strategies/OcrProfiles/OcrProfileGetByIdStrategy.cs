using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	public class OcrProfileGetByIdStrategy : ObjectQueryGetByIdWorkspaceEntityStrategy<OcrProfile>
	{
		public OcrProfileGetByIdStrategy(IObjectService objectService)
			: base(objectService)
		{
		}
	}
}
