using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class DocumentViewerPropertiesDtoV1
	{
		public bool AllowDocumentViewerChange { get; set; }

		public bool AllowKeyboardShortcuts { get; set; }

		public bool AllowDocumentSkipPreferenceChange { get; set; }

		public UserDefaultSelectedFileType DefaultSelectedFileType { get; set; }

		public UserDocumentViewer DocumentViewer { get; set; }

		public bool SkipDefaultPreference { get; set; }
	}
}
