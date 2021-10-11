using System.Collections.Generic;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Extensions;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Tests.Extensions
{
	[TestOf(typeof(UserDtoV1Extensions))]
	public class UserDtoV1ExtensionsFixture
	{
		private readonly UserDtoV1 _userDtoToMap = new UserDtoV1
		{
			DocumentViewerProperties = new DocumentViewerPropertiesDtoV1(),
			Type = new NamedArtifact(),
			FirstName = "Martin",
			LastName = "Smith"
		};

		[Test]
		public void MapToUser_WithGroupsAsNull_ShouldAddGroupsAsEmptyList()
		{
			Client client = null;
			IList<NamedArtifact> groups = null;

			var user = _userDtoToMap.MapToUser(client, groups);

			Assert.NotNull(user.Groups);
			Assert.IsEmpty(user.Groups);
		}

		[Test]
		public void MapToUser_WithNames_ShouldCreateFullName()
		{
			var expectedFullName = "Martin, Smith";

			Client client = null;
			IList<NamedArtifact> groups = null;

			var user = _userDtoToMap.MapToUser(client, groups);

			Assert.AreEqual(expectedFullName, user.FullName);
		}
	}
}
