using System.Threading.Tasks;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IRemoveInactiveImagingJobsStrategy
	{
		void Remove();

		Task RemoveAsync();
	}
}
