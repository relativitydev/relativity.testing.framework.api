using System;
using System.IO;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Models.Fields;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Services
{
	[TestOf(typeof(FileFieldService))]
	[NonParallelizable]
	public class FileFieldServiceFixture : ApiTestFixture
	{
		private const string _FILENAME = "Test File Field File Name";
		private const string _FILE_FIELD_NAME = "Test File Field";

		private IFieldService _fieldService;
		private IObjectService _objectService;
		private IObjectTypeService _objectTypeService;
		private IFileFieldService _sut;

		private ObjectType _objectType = new ObjectType
		{
			Name = "Test Object",
		};

		private FileField _fileField = new FileField
		{
			Name = _FILE_FIELD_NAME
		};

		private TestObject _testRDO = new TestObject();

		public FileFieldServiceFixture()
		{
		}

		public FileFieldServiceFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			_sut = Facade.Resolve<IFileFieldService>();
			_fieldService = Facade.Resolve<IFieldService>();
			_objectService = Facade.Resolve<IObjectService>();
			_objectTypeService = Facade.Resolve<IObjectTypeService>();

			SetupRequiredObjects();
		}

		protected override void OnTearDownFixture()
		{
			_objectService.Delete(-1, _testRDO.ArtifactID);
			_objectTypeService.Delete(-1, _objectType.ArtifactID);
			_fieldService.Delete(-1, _fileField.ArtifactID);

			base.OnTearDownFixture();
		}

		private void SetupRequiredObjects()
		{
			_objectType = _objectTypeService.Create(-1, _objectType);

			_fileField.ObjectType = _objectType;
			_fileField = _fieldService.Create(-1, _fileField);

			_testRDO = _objectService.Create(-1, _testRDO);
		}

		[Test]
		public void UploadFile_ByFileFieldArtifactId_UploadsFile()
		{
			var fileFieldDto = UploadSampleFileByFileFieldArtifactId();
			Assert.That(fileFieldDto.UploadedFileGuid.HasValue);
		}

		[Test]
		public void UploadFile_ByFileFieldName_UploadsFile()
		{
			var fileFieldDto = UploadSampleFileByFileFieldName();

			Assert.That(fileFieldDto.UploadedFileGuid.HasValue);
		}

		[Test]
		public void DownloadFile_ByFileFieldArtifactId_DownloadsFile()
		{
			var fileFieldDto = UploadSampleFileByFileFieldArtifactId();

			fileFieldDto.FileStream = new MemoryStream();

			var result = _sut.DownloadFile(-1, fileFieldDto);

			using (result.FileStream)
			{
				Assert.That(result.FileStream.Length > 0);
			}
		}

		[Test]
		public void DownloadFile_ByFileFieldName_DownloadsFile()
		{
			var fileFieldDto = UploadSampleFileByFileFieldName();

			fileFieldDto.FileStream = new MemoryStream();

			var result = _sut.DownloadFile(-1, fileFieldDto);

			using (result.FileStream)
			{
				Assert.That(result.FileStream.Length > 0);
			}
		}

		private FileFieldDTO UploadSampleFileByFileFieldArtifactId()
		{
			var fileField = new FileField
			{
				ArtifactID = _fileField.ArtifactID
			};

			var fileFieldDtoAfterUpload = UploadSampleFile(fileField);
			return fileFieldDtoAfterUpload;
		}

		private FileFieldDTO UploadSampleFileByFileFieldName()
		{
			var fileField = new FileField
			{
				Name = _fileField.Name
			};

			var fileFieldDtoAfterUpload = UploadSampleFile(fileField);
			return fileFieldDtoAfterUpload;
		}

		private FileFieldDTO UploadSampleFile(FileField field)
		{
			var fileFieldDto = new FileFieldDTO
			{
				Field = field,
				ObjectRef = _testRDO,
				FileName = _FILENAME,
			};

			using (FileStream fileStream = File.OpenRead($@"{AppDomain.CurrentDomain.BaseDirectory}\files\SampleFileForFileField"))
			{
				fileFieldDto.FileStream = fileStream;

				fileFieldDto = _sut.UploadFile(-1, fileFieldDto);
			}

			return fileFieldDto;
		}

		public class TestObject : Artifact
		{
		}
	}
}
