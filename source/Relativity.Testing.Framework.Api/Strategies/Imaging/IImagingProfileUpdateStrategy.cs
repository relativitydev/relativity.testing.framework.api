using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IImagingProfileUpdateStrategy
	{
		void Update(int workspaceId, ImagingProfile imagingProfile);

		Task UpdateAsync(int workspaceId, ImagingProfile imagingProfile);
	}
}
