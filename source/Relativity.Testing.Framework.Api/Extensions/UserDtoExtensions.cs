using Newtonsoft.Json;
using Relativity.Testing.Framework.Dto;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Extensions
{
	internal static class UserDtoExtensions
	{
		public static User MapToUser(this UserDto dto)
		{
			return JsonConvert.DeserializeObject<User>(JsonConvert.SerializeObject(dto));
		}
	}
}
