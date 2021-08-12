using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class MatterDtoV1
	{
		public MatterDtoV1(Matter matter, int statusId)
		{
			MatterRequest = new MatterRequestV1(matter, statusId);
		}

		public MatterRequestV1 MatterRequest { get; set; }
	}
}
