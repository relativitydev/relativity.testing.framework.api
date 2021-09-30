using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(GroupResponseMapper))]
	internal class GroupResponseMapperFixture
	{
		[Test]
		public void DoMappingFromResponse_MapsProperties()
		{
			var groupGuids = new List<Guid>
			{
				Guid.NewGuid(),
				Guid.NewGuid()
			};
			string clientName = "Test Client Name";
			int clientArtifactID = 1;
			GroupType type = GroupType.WorkspaceAdmin;
			var actions = new List<HttpAction>
			{
				new HttpAction
				{
					Name = "Get",
					Href = "https://test.com/test",
					Verb = "GET",
					IsAvailable = true
				},
				new HttpAction
				{
					Name = "Update",
					Href = "https://test.com/test",
					Verb = "UPDATE",
					IsAvailable = false,
					Reason = new List<string>
					{
						"Object is readonly"
					}
				}
			};
			var meta = new Meta
			{
				Unsupported = new List<string>
					{
						"Test",
						"Unsupported"
					},
				ReadOnly = new List<string>
					{
						"Test",
						"Read",
						"Only"
					},
			};
			string keywords = "Test Keywords";
			string notes = "Test Notes";
			NamedArtifact createdBy = new NamedArtifact
			{
				Name = "Admin",
				ArtifactID = 2
			};
			NamedArtifact modifiedBy = new NamedArtifact
			{
				Name = "Test User",
				ArtifactID = 3
			};
			DateTime modifiedOn = DateTime.Now;
			DateTime createdOn = DateTime.Parse("5/1/2019 8:30:52 AM");

			var groupResponse = new GroupResponse
			{
				Guids = groupGuids,
				Client = new Securable<NamedArtifactWithGuids>(new NamedArtifactWithGuids
				{
					Name = clientName,
					ArtifactID = clientArtifactID,
					Guids = new List<Guid>
					{
						Guid.NewGuid(),
						Guid.NewGuid()
					},
				}),
				Type = type,
				Actions = actions,
				Meta = meta,
				Keywords = keywords,
				Notes = notes,
				CreatedBy = createdBy,
				CreatedOn = createdOn,
				LastModifiedBy = modifiedBy,
				LastModifiedOn = modifiedOn
			};
			var expectedGroup = new Group
			{
				Guids = groupGuids,
				Client = new Client
				{
					Name = clientName,
					ArtifactID = clientArtifactID
				},
				Type = type,
				Actions = actions,
				Meta = meta,
				Keywords = keywords,
				Notes = notes,
				CreatedBy = createdBy,
				CreatedOn = createdOn,
				LastModifiedBy = modifiedBy,
				LastModifiedOn = modifiedOn
			};

			Group mappedGroup = groupResponse.DoMappingFromResponse();

			mappedGroup.Should().NotBeNull();
			mappedGroup.Should().BeEquivalentTo(expectedGroup);
		}
	}
}
