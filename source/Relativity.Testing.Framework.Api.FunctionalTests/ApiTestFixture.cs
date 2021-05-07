using NUnit.Framework;
using Relativity.Testing.Framework.Api.Arrangement;
using Relativity.Testing.Framework.Configuration;

namespace Relativity.Testing.Framework.Api.FunctionalTests
{
	/// <summary>
	/// Represents the base fixture class for API test fixtures.
	/// </summary>
	[TestFixtureSource(typeof(Config), nameof(Config.GetTestRelativityInstanceAliases))]
	public abstract class ApiTestFixture : SessionBasedFixture
	{
		protected static readonly object Locker = new object();

		/// <summary>
		/// Initializes a new instance of the <see cref="ApiTestFixture"/> class using default Relativity instance configuration.
		/// </summary>
		protected ApiTestFixture()
			: this(null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ApiTestFixture"/> class using the Relativity instance configuration by <paramref name="relativityInstanceAlias"/>.
		/// </summary>
		/// <param name="relativityInstanceAlias">The Relativity instance alias.</param>
		protected ApiTestFixture(string relativityInstanceAlias)
		{
			FacadeHost = new RelativityFacadeHost(relativityInstanceAlias);
		}

		/// <summary>
		/// Gets the <see cref="RelativityFacadeHost"/> instance.
		/// </summary>
		protected RelativityFacadeHost FacadeHost { get; }

		/// <inheritdoc>
		protected override IRelativityFacade Facade => FacadeHost.Facade;

		/// <summary>
		/// Gets current Relativity instance configuration.
		/// </summary>
		protected RelativityInstanceConfiguration RelativityInstanceConfiguration => FacadeHost.RelativityInstanceConfiguration;

		/// <inheritdoc>
		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			FacadeHost.SetUpFacadeWithCoreAndApi();
			CheckVersionRangeForFixture();
		}

		/// <inheritdoc>
		protected override void OnSetUpTest()
		{
			base.OnSetUpTest();
			CheckVersionRangeForTest();
		}

		/// <inheritdoc>
		protected override void OnTearDownFixture()
		{
			base.OnTearDownFixture();

			FacadeHost.TearDownFacade();
		}
	}
}
