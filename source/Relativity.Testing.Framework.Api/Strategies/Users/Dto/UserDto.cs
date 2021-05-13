using Relativity.Testing.Framework.Attributes;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Dto
{
	[ObjectTypeName("User")]
	internal class UserDto : User
	{
		[FieldName("E-mail Address")]
		public override string EmailAddress { get; set; }

		[FieldName("User Type")]
		public override string Type { get; set; }
	}
}
