using System.Linq;
using Relativity.Testing.Framework.Api.Extensions;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Dto;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class UserGetByIdStrategy : IGetByIdStrategy<User>
	{
		private readonly IObjectService _objectService;

		public UserGetByIdStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public User Get(int id)
		{
			var userDto = _objectService.Query<UserDto>().Where(x => x.ArtifactID, id).FirstOrDefault();

			if (userDto != null)
			{
				var fullName = userDto.FullName.Split(',');
				userDto.FirstName = fullName.Last().Trim();
				userDto.LastName = fullName.First().Trim();
			}

			// We need to return User class object and not UserDto class casted as User (as calling typeof will return UserDto) due to reflection logic in TestSession.
			return userDto.MapToUser();
		}
	}
}
