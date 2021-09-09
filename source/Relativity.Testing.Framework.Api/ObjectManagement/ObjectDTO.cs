using System.Collections.Generic;

namespace Relativity.Testing.Framework.Api.ObjectManagement
{
	internal class ObjectDTO
	{
		public ObjectDTO(int objectArtifactID, IList<FieldRefValuePair> fieldValues)
		{
			Request = new ObjectRequest(objectArtifactID, fieldValues);
		}

		public ObjectRequest Request { get; set; }
	}
}
