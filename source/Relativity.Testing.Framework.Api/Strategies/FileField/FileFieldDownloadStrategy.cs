using System.Net.Http;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class FileFieldDownloadStrategy : IFileFieldDownloadStrategy
	{
		private readonly IRestService _restService;

		public FileFieldDownloadStrategy(IRestService restService)
		{
			_restService = restService;
		}

		public FileFieldDTO DownloadFile(int workspaceId, FileFieldDTO fileFieldDto)
		{
			using (var response = _restService.Post<HttpResponseMessage>($"Relativity.FileField/workspace/{workspaceId}/file/download", fileFieldDto))
			{
				response.Content.CopyToAsync(fileFieldDto.FileStream).Wait();
				return fileFieldDto;
			}
		}
	}
}
