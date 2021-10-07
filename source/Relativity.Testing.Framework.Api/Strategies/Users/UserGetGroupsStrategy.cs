using System;
using System.Collections.Generic;
using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Api.Querying;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Mapping;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class UserGetGroupsStrategy : QueryStrategy<NamedArtifactQueryRequest>, IUserGetGroupsStrategy
	{
		public UserGetGroupsStrategy(IRestService restService, IObjectMappingService objectMappingService)
			: base(restService, objectMappingService)
		{
		}

		public IList<NamedArtifact> GetGroups(int userId)
		{
			var request = new NamedArtifactQueryRequest(userId);

			IQueryExecutor<NamedArtifact> executor = new QueryExecutor<NamedArtifact, NamedArtifactQueryRequest>(QuerySlimAndMap<NamedArtifact>);

			return new NamedArtifactQuery<NamedArtifact>(request, executor).ToList();
		}

		protected override QuerySlimResult QuerySlim(NamedArtifactQueryRequest request)
		{
			if (request == null)
			{
				throw new ArgumentNullException(nameof(request));
			}

			var wrapper = new
			{
				Request = request,
				request.Start,
				request.Length
			};

			return RestService.Post<QuerySlimResult>($"Relativity.groups/workspace/-1/groups/query-by-user/{request.UserArtifactId}", wrapper);
		}
	}
}
