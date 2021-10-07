using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Dto;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class UserGetByIdStrategyPreOsier : IGetByIdStrategy<User>
	{
		private readonly IObjectService _objectService;

		public UserGetByIdStrategyPreOsier(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public User Get(int id)
		{
			var user = _objectService.Query<UserDto>().Where(x => x.ArtifactID, id).FirstOrDefault();

			if (user != null)
			{
				var fullName = user.FullName.Split(',');
				user.FirstName = fullName.Last().Trim();
				user.LastName = fullName.First().Trim();
			}

			return user;
		}
	}
}
