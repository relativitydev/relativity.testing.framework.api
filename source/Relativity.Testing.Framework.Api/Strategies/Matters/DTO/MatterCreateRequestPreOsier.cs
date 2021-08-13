using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class MatterCreateRequestPreOsier
	{
		public MatterCreateRequestPreOsier(Matter matter, int statusId)
		{
			MatterDTO = new MatterRequestPreOsier(matter, statusId);
		}

		public MatterRequestPreOsier MatterDTO { get; set; }
	}
}
