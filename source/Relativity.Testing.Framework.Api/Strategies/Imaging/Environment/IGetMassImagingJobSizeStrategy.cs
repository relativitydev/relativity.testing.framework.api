using System.Threading.Tasks;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IGetMassImagingJobSizeStrategy
	{
		int Get();

		Task<int> GetAsync();
	}
}
