using System.Net.Http;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Validators;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class GroupGetByIdStrategyV1 : IGroupGetByIdStrategy
	{
		private readonly IArtifactIdValidator _artifactIdValidator;
		private readonly IRestService _restService;

		public GroupGetByIdStrategyV1(IRestService restService, IArtifactIdValidator artifactIdValidator)
		{
			_restService = restService;
			_artifactIdValidator = artifactIdValidator;
		}

		public Group Get(int id, bool includeMetadata = false, bool includeActions = false)
		{
			_artifactIdValidator.Validate(id, "Group");
			string includeMetadataString = includeMetadata.ToString();
			string includeActionsString = includeActions.ToString();

			try
			{
				GroupResponse response = _restService.Get<GroupResponse>($"Relativity-Identity/v1/groups/{id}?includeMetadata={includeMetadataString}&includeActions={includeActionsString}");

				Group mappedGroup = response.MapToGroup();
				return mappedGroup;
			}
			catch (HttpRequestException exception)
			{
				if (exception.Message.Contains("The object does not exist or you do not have permission to access it."))
				{
					return null;
				}

				throw;
			}
		}
	}
}
