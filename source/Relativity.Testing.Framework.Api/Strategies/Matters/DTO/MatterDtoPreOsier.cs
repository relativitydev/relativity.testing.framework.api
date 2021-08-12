using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class MatterDtoPreOsier
	{
		public MatterDtoPreOsier(Matter matter, int statusId)
		{
			MatterDTO = new MatterRequestPreOsier(matter, statusId);
		}

		public MatterRequestPreOsier MatterDTO { get; set; }
	}
}
