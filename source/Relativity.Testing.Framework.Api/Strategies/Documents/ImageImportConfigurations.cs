using System;
using kCura.EDDS.WebAPI.BulkImportManagerBase;
using kCura.Relativity.DataReaderClient;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal static class ImageImportConfigurations
	{
		public static void ConfigureImportJobSettingsForImageImport(ImageImportBulkArtifactJob importBulkArtifactJob, int workspaceArtifactId, ImageImportOptions options = null)
		{
			if (options == null)
			{
				options = new ImageImportOptions();
			}

			ConfigureCommonImportJobSettings(importBulkArtifactJob, workspaceArtifactId, options);

			importBulkArtifactJob.Settings.BatesNumberField = options.BatesNumberField;
			importBulkArtifactJob.Settings.DocumentIdentifierField = options.DocumentIdentifierField;
			importBulkArtifactJob.Settings.FileLocationField = options.FileLocationField;
			importBulkArtifactJob.Settings.IdentityFieldId = options.IdentityFieldId;
			importBulkArtifactJob.Settings.CopyFilesToDocumentRepository = true;
			importBulkArtifactJob.Settings.DisableImageTypeValidation = true;
		}

		public static void ConfigureCommonImportJobSettings(
			ImageImportBulkArtifactJob importBulkArtifactJob,
			int workspaceArtifactId,
			DocumentImportOptionsBase options = null)
		{
			importBulkArtifactJob.Settings.CaseArtifactId = workspaceArtifactId;
			importBulkArtifactJob.Settings.ArtifactTypeId = 10;

			importBulkArtifactJob.Settings.SelectedIdentifierFieldName = options.DocumentIdentifierField;
			importBulkArtifactJob.Settings.OverwriteMode = (OverwriteModeEnum)Enum.Parse(typeof(OverwriteModeEnum), options.OverwriteMode.ToString());
			importBulkArtifactJob.Settings.OverlayBehavior = (OverlayBehavior)Enum.Parse(typeof(OverlayBehavior), options.OverlayBehavior.ToString());
			importBulkArtifactJob.Settings.DisableExtractedTextEncodingCheck = false;
			importBulkArtifactJob.Settings.DisableUserSecurityCheck = true;
			importBulkArtifactJob.Settings.ExtractedTextFieldContainsFilePath = options.ExtractedTextFieldContainsFilePath;
			importBulkArtifactJob.Settings.ExtractedTextEncoding = options.ExtractedTextEncoding;
			importBulkArtifactJob.Settings.MaximumErrorCount = int.MaxValue - 1;
			importBulkArtifactJob.Settings.StartRecordNumber = 0;
			importBulkArtifactJob.Settings.Billable = false;
			importBulkArtifactJob.Settings.LoadImportedFullTextFromServer = false;
			importBulkArtifactJob.Settings.MoveDocumentsInAppendOverlayMode = false;
		}
	}
}
