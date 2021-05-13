using System;
using System.Linq;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the base class for strategy that gets choice ID by choice name.
	/// </summary>
	public abstract class CachedGetChoiceIdByNameStrategyBase
	{
		private readonly Lazy<ArtifactIdNamePair[]> _lazyAllChoices;

		/// <summary>
		/// Initializes a new instance of the <see cref="CachedGetChoiceIdByNameStrategyBase"/> class.
		/// </summary>
		protected CachedGetChoiceIdByNameStrategyBase()
		{
			_lazyAllChoices = new Lazy<ArtifactIdNamePair[]>(GetAll);
		}

		/// <summary>
		/// Gets all choice objects of specific type as <see cref="NamedArtifact"/> array.
		/// </summary>
		/// <returns>All choice objects of specific type.</returns>
		protected abstract ArtifactIdNamePair[] GetAll();

		/// <summary>
		/// Gets the artifact ID of specific choice object by name.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns>The artifact ID.</returns>
		/// <exception cref="ObjectNotFoundException">The choice object is not found by name.</exception>
		public int GetId(string name)
		{
			ArtifactIdNamePair[] allChoices = _lazyAllChoices.Value;
			ArtifactIdNamePair choice = allChoices.FirstOrDefault(x => x.Name == name);

			if (choice is null)
			{
				string availableChoiceNames = string.Join(", ", allChoices.Select(x => $"\"{x.Name}\""));
				throw new ObjectNotFoundException($"The choice object is not found by \"{name}\" name. There are choices with the names available: {availableChoiceNames}.");
			}
			else
			{
				return choice.ArtifactID;
			}
		}
	}
}
