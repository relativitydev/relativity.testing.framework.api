using System;
using System.Linq;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using Relativity.Testing.Framework.Arrangement;
using Relativity.Testing.Framework.Logging;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Session;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Arrangement
{
	/// <summary>
	/// Represents the base fixture that manages [TestSession](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Session.TestSession.html) for fixture and test levels.
	/// </summary>
	[TestFixture]
	public abstract class SessionBasedFixture
	{
		private TestSession FixtureSession { get; set; }

		private TestSession TestSession { get; set; }

		/// <summary>
		/// Gets the current session.
		/// During fixture set up or tear down returns session of fixture;
		/// otherwise returns session of test.
		/// </summary>
		protected static TestSession Session => TestSession.Current;

		/// <summary>
		/// Gets the current [IRelativityFacade](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.IRelativityFacade.html) instance.
		/// </summary>
		protected virtual IRelativityFacade Facade => RelativityFacade.Instance;

		/// <summary>
		/// Gets the [ILogService](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Logging.ILogService.html).
		/// </summary>
		protected ILogService Log => Facade?.Log;

		/// <summary>
		/// Gets the name of the current test/fixture.
		/// </summary>
		protected static string CurrentTestName => TestContext.CurrentContext?.Test?.Name;

		/// <summary>
		/// Sets up fixture.
		/// </summary>
		[OneTimeSetUp]
		public void SetUpFixture()
		{
			TestSession.Current = FixtureSession = TestSession.Global.StartChildSession();

			Log?.Info($"Starting test fixture: {CurrentTestName}");
			Log?.Info($"Starting test fixture setup");

			OnSetUpFixture();

			Log?.Info($"Finished test fixture setup");
		}

		/// <summary>
		/// Called when set up of fixture executes.
		/// </summary>
		protected virtual void OnSetUpFixture()
		{
		}

		/// <summary>
		/// Tears down fixture.
		/// </summary>
		[OneTimeTearDown]
		public void TearDownFixture()
		{
			TestSession.Current = FixtureSession;

			Log?.Info($"Starting test fixture tear down");

			try
			{
				OnTearDownFixture();
			}
			catch (Exception exception)
			{
				Log?.Error("Test fixture tear down failed", exception);
			}
			finally
			{
				FixtureSession?.Dispose();
				FixtureSession = null;

				Log?.Info($"Finished test fixture tear down");
				Log?.Info($"Finished test fixture: {CurrentTestName}");
			}
		}

		/// <summary>
		/// Called when tear down of fixture executes.
		/// </summary>
		protected virtual void OnTearDownFixture()
		{
		}

		/// <summary>
		/// Sets up test.
		/// </summary>
		[SetUp]
		public void SetUpTest()
		{
			TestSession.Current = TestSession = FixtureSession.StartChildSession();

			Log?.Info($"Starting test: {CurrentTestName}");

			Log?.Info($"Starting test setup");

			OnSetUpTest();

			Log?.Info($"Finished test setup");
		}

		/// <summary>
		/// Called when set up of test executes.
		/// </summary>
		protected virtual void OnSetUpTest()
		{
		}

		/// <summary>
		/// Tears down test.
		/// </summary>
		[TearDown]
		public void TearDownTest()
		{
			TestSession.Current = TestSession;

			Log?.Info($"Starting test tear down");

			try
			{
				OnTearDownTest();
			}
			catch (Exception exception)
			{
				Log?.Error("Test tear down failed", exception);
			}
			finally
			{
				TestSession?.Dispose();
				TestSession = null;

				Log?.Info($"Finished test tear down");
				Log?.Info($"Finished test: {CurrentTestName}");
			}
		}

		/// <summary>
		/// Called when tear down of test executes.
		/// </summary>
		protected virtual void OnTearDownTest()
		{
			var testResult = TestContext.CurrentContext.Result;

			if (testResult.Outcome.Status == TestStatus.Failed)
			{
				StringBuilder builder = new StringBuilder(testResult.Message)
					.AppendLine()
					.Append(testResult.StackTrace);

				Log?.Error(builder.ToString());
			}
		}

		/// <summary>
		/// Executes the specified arrange action.
		/// If action fails with an exception, wraps the exception with <see cref="TestArrangeException"/>.
		/// </summary>
		/// <param name="action">The arrange action.</param>
		/// <exception cref="TestArrangeException">Test arrange failed.</exception>
		protected void Arrange(Action action)
		{
			try
			{
				Log?.Info("Starting arrange");
				action?.Invoke();
				Log?.Info("Finished arrange");
			}
			catch (Exception exception) when (!(exception is TestArrangeException))
			{
				throw new TestArrangeException("Test arrange failed.", exception);
			}
		}

		/// <summary>
		/// Executes the specified arrange action using <see cref="TestArrangementDomain"/>.
		/// If action fails with an exception, wraps the exception with <see cref="TestArrangeException"/>.
		/// </summary>
		/// <param name="action">The domain arrange action.</param>
		/// <exception cref="TestArrangeException">Test arrange failed.</exception>
		protected void Arrange(Action<TestArrangementDomain> action)
		{
			Arrange(() => action?.Invoke(CreateTestArrangementDomain()));
		}

		/// <summary>
		/// Executes the specified workspace arrange action using <see cref="TestArrangementDomain{TEntity}"/> for working workspace.
		/// If action fails with an exception, wraps the exception with <see cref="TestArrangeException"/>.
		/// </summary>
		/// <param name="action">The domain arrange action.</param>
		/// <exception cref="TestArrangeException">Test arrange failed.</exception>
		protected void ArrangeWorkingWorkspace(Action<TestArrangementDomain<Workspace>> action)
		{
			Arrange(() => CreateTestArrangementDomain().ForWorkingWorkspace().ArrangeWorkspace(action));
		}

		/// <summary>
		/// Executes the specified workspace arrange action using <see cref="TestArrangementDomain{TEntity}"/> for the specified workspace.
		/// If action fails with an exception, wraps the exception with <see cref="TestArrangeException"/>.
		/// </summary>
		/// <param name="workspace">The workspace.</param>
		/// <param name="action">The domain arrange action.</param>
		/// <exception cref="TestArrangeException">Test arrange failed.</exception>
		protected void ArrangeWorkspace(Workspace workspace, Action<TestArrangementDomain<Workspace>> action)
		{
			Arrange(() => CreateTestArrangementDomain().For(workspace).ArrangeWorkspace(action));
		}

		/// <summary>
		/// Creates the <see cref="TestArrangementDomain"/> using current <see cref="Session"/>.
		/// </summary>
		/// <returns>The created <see cref="TestArrangementDomain"/>.</returns>
		private TestArrangementDomain CreateTestArrangementDomain()
		{
			return new TestArrangementDomain(
				new TestArrangementContext(Facade, Session, new AdminLevelEntityCreator(Facade)));
		}

		protected bool IsVersionComparable(string version)
		{
			return Facade.Resolve<IVersionRangeMatchService>()
				.IsVersionInRange(Facade.RelativityInstanceVersion, version);
		}

		protected static string GetVersionRange(MemberInfo member)
		{
			return member.GetCustomAttributes<VersionRangeAttribute>().
				FirstOrDefault()?.
				VersionRange;
		}

		protected void CheckVersionRangeForFixture()
		{
			var version = GetVersionRange(GetType());

			if (version != null && !IsVersionComparable(version))
			{
				Assert.Inconclusive();
			}
		}

		protected void CheckVersionRangeForTest()
		{
			var version = GetVersionRange(TestExecutionContext.CurrentContext.CurrentTest.Method.MethodInfo);
			if (version != null && !IsVersionComparable(version))
			{
				Assert.Inconclusive();
			}
		}
	}
}
