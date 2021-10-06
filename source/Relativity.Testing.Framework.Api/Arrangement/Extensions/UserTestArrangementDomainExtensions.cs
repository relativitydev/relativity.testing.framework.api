using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Arrangement
{
	/// <summary>
	/// Provides a set of extension methods for <see cref="TestArrangementDomain{TEntity}"/> of [User](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.User.html).
	/// </summary>
	public static class UserTestArrangementDomainExtensions
	{
		/// <summary>
		/// Adds current user to the specified group.
		/// </summary>
		/// <param name="domain">The domain.</param>
		/// <param name="group">The group.</param>
		/// <returns>The same domain.</returns>
		public static TestArrangementDomain<User> AddTo(this TestArrangementDomain<User> domain, Group group)
		{
			var strategy = domain.Context.Facade.Resolve<IUserAddToGroupStrategy>();

			strategy.AddToGroup(domain.Entity.ArtifactID, group.ArtifactID);

			return domain;
		}
	}
}
