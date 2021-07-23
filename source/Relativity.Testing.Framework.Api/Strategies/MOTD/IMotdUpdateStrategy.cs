using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IMotdUpdateStrategy
	{
		MessageOfTheDay Update(MessageOfTheDay entity);

		Task<MessageOfTheDay> UpdateAsync(MessageOfTheDay entity);
	}
}
