using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Services
{
	[TestOf(typeof(IWorkspacePermissionService))]
	internal class WorkspaceGroupSelectorGetByWorkspaceIdStrategyFixture : ApiServiceTestFixture<IWorkspacePermissionService>
	{
		[Test]
		public void Get_Missing()
		{
			int id = 9_999_999;

			var exception = Assert.Throws<HttpRequestException>(() =>
				Sut.GetWorkspaceGroupSelector(id));

			exception.Message.Should().StartWith($"Workspace {id} is invalid.");
		}

		[Test]
		public void Get_Existing()
		{
			Workspace entity = null;
			List<string> defaultDisabledGroupNames = new List<string>
			{
				"Domain Users",
				"First Level Group",
				"Second Level Group",
				"Third Level Group",
				"Level 1",
				"Level 2",
				"Level 3"
			};

			Arrange(x => x.Create(out entity));

			var result = Sut.GetWorkspaceGroupSelector(entity.ArtifactID);

			result.Should().NotBeNull();
			result.DisabledGroups.Select(x => x.Name).Should().Contain(defaultDisabledGroupNames);
			result.DisabledGroups[0].Name.Should().NotBeNullOrEmpty();
			result.DisabledGroups[0].ArtifactID.Should().BePositive();
			result.LastModified.Should().NotBe(DateTime.MinValue);
		}
	}
}
