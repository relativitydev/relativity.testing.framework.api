using System;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class MatterUpdateRequest
	{
		public MatterUpdateRequest(Matter matter, int statusID, bool restrictedUpdate = false)
		{
			MatterRequest = new MatterRequestV1(matter, statusID);

			if (restrictedUpdate)
			{
				LastModifiedOn = matter.LastModifiedOn;
			}
		}

		public MatterRequestV1 MatterRequest { get; set; }

		public DateTime? LastModifiedOn { get; set; }
	}
}
