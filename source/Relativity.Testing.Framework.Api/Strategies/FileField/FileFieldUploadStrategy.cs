using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Relativity.Testing.Framework.Api.Attributes;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[DoNotRetry]
	internal class FileFieldUploadStrategy : IFileFieldUploadStrategy
	{
		private readonly IRestService _restService;

		public FileFieldUploadStrategy(IRestService restService)
		{
			_restService = restService;
		}

		public FileFieldDTO UploadFile(int workspaceId, FileFieldDTO fileFieldDto)
		{
			using (var form = new MultipartFormDataContent())
			using (StreamContent streamContent = new StreamContent(fileFieldDto.FileStream))
			{
				var jsonString = JsonConvert.SerializeObject(fileFieldDto, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
				using (var jsonContent = new StringContent(jsonString, Encoding.UTF8, "application/json"))
				{
					form.Add(jsonContent, "metadata");
					form.Add(streamContent, "file");

					var response = _restService.Post<FileFieldDTO>($"Relativity.FileField/workspace/{workspaceId}/file/upload", form);
					fileFieldDto.UploadedFileGuid = response.UploadedFileGuid;
					fileFieldDto.FileName = response.FileName;

					UpdateObjectWithFileField(fileFieldDto);

					return fileFieldDto;
				}
			}
		}

		private void UpdateObjectWithFileField(FileFieldDTO fileFieldDto)
		{
			var saveDto = new
			{
				Request = new
				{
					Object = fileFieldDto.ObjectRef,
					FieldValues = new[]
					{
						new
						{
							Field = fileFieldDto.Field,
							Value = fileFieldDto
						}
					}
				}
			};

			_restService.Post<NamedArtifact>($"Relativity.Objects/workspace/-1/object/update", saveDto);
		}
	}
}
