using System;
using kCura.Relativity.DataReaderClient;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal static class NativeImportConfigurations
	{
		public static void ConfigureImportJobSettingsForNativeImport(ImportBulkArtifactJob importBulkArtifactJob, int workspaceArtifactId, NativeImportOptions options = null)
		{
			if (options == null)
			{
				options = new NativeImportOptions();
			}

			ConfigureCommonImportJobSettings(importBulkArtifactJob, workspaceArtifactId, options);

			importBulkArtifactJob.Settings.NativeFilePathSourceFieldName = options.NativeFilePathColumnName;
			importBulkArtifactJob.Settings.CopyFilesToDocumentRepository = true;
			importBulkArtifactJob.Settings.NativeFileCopyMode = (NativeFileCopyModeEnum)Enum.Parse(typeof(NativeFileCopyModeEnum), options.NativeFileCopyMode.ToString());
			importBulkArtifactJob.Settings.DisableNativeLocationValidation = false;
			importBulkArtifactJob.Settings.DisableNativeValidation = false;
			importBulkArtifactJob.Settings.BulkLoadFileFieldDelimiter = options.BulkLoadFileFieldDelimiter;
		}

		public static void ConfigureCommonImportJobSettings(
			ImportBulkArtifactJob importBulkArtifactJob,
			int workspaceArtifactId,
			DocumentImportOptionsBase options = null)
		{
			importBulkArtifactJob.Settings.CaseArtifactId = workspaceArtifactId;
			importBulkArtifactJob.Settings.ArtifactTypeId = 10;

			importBulkArtifactJob.Settings.SelectedIdentifierFieldName = options.DocumentIdentifierField;

			importBulkArtifactJob.Settings.OverwriteMode = (OverwriteModeEnum)Enum.Parse(typeof(OverwriteModeEnum), options.OverwriteMode.ToString());
			importBulkArtifactJob.Settings.DisableExtractedTextEncodingCheck = false;
			importBulkArtifactJob.Settings.DisableUserSecurityCheck = true;
			importBulkArtifactJob.Settings.ExtractedTextFieldContainsFilePath = options.ExtractedTextFieldContainsFilePath;
			importBulkArtifactJob.Settings.ExtractedTextEncoding = options.ExtractedTextEncoding;
			importBulkArtifactJob.Settings.MaximumErrorCount = int.MaxValue - 1;
			importBulkArtifactJob.Settings.StartRecordNumber = 0;
			importBulkArtifactJob.Settings.Billable = false;
			importBulkArtifactJob.Settings.LoadImportedFullTextFromServer = false;
			importBulkArtifactJob.Settings.MoveDocumentsInAppendOverlayMode = false;
			importBulkArtifactJob.Settings.DisableControlNumberCompatibilityMode = true;
			importBulkArtifactJob.Settings.DisableExtractedTextFileLocationValidation = true;
		}
	}
}
