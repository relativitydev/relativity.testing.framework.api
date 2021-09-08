using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.ObjectManagement
{
	internal class ObjectRequest
	{
		public ObjectRequest(int objectArtifactID, IList<FieldRefValuePair> fieldValues)
		{
			Object = new Artifact(objectArtifactID);
			FieldValues = fieldValues;
		}

		public Artifact Object { get; set; }

		public IList<FieldRefValuePair> FieldValues { get; set; }
	}
}
