using System.Threading.Tasks;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IImagingJobSubmitSingleDocumentStrategy
	{
		long SubmitSingleDocument(int workspaceId, int documentArtifactId, ImagingJobSubmitSingleDocumentRequest imagingJobSubmitSingleDocumentRequest);

		Task<long> SubmitSingleDocumentAsync(int workspaceId, int documentArtifactId, ImagingJobSubmitSingleDocumentRequest imagingJobSubmitSingleDocumentRequest);
	}
}
