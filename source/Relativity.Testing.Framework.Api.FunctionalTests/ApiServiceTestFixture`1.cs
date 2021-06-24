using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests
{
	/// <summary>
	/// Represents the base fixture class for API service test fixtures.
	/// </summary>
	/// <typeparam name="TService">Service which will be resolve from Facde as SUT object.</typeparam>
	internal abstract class ApiServiceTestFixture<TService> : ApiTestFixture
	{
		protected const string DefaultWorkspaceName = "RTF api default test workspace.";

		/// <summary>
		/// Initializes a new instance of the <see cref="ApiServiceTestFixture{TService}"/> class.
		/// </summary>
		protected ApiServiceTestFixture()
		{
		}

		/// <summary>
		/// Gets SUT object.
		/// </summary>
		protected TService Sut { get; private set; }

		/// <summary>
		/// Gets default workspace.
		/// </summary>
		protected Workspace DefaultWorkspace { get; private set; }

		/// <inheritdoc>
		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			InitDefaultWorkspace();

			Facade.Resolve<IInstanceSettingsService>().Require("AllowAddOrEditScripts", "kCura.EDDS.Web", "true");
		}

		/// <inheritdoc>
		protected override void OnSetUpTest()
		{
			base.OnSetUpTest();
			Sut = Facade.Resolve<TService>();
		}

		private void InitDefaultWorkspace()
		{
			var workspaceService = Facade.Resolve<IWorkspaceService>();
			var documentService = Facade.Resolve<IDocumentService>();
			var objectService = Facade.Resolve<IObjectService>();
			var installApplicationToWorkspace = Facade.Resolve<ILibraryApplicationInstallToWorkspaceStrategy>();

			lock (Locker)
			{
				DefaultWorkspace = workspaceService.Get(DefaultWorkspaceName);

				if (DefaultWorkspace == null)
				{
					DefaultWorkspace = workspaceService.Create(new Workspace { Name = DefaultWorkspaceName });

					documentService.ImportGeneratedDocuments(DefaultWorkspace.ArtifactID);
					var processingAppId = objectService.Query<LibraryApplication>().Where(x => x.Name, "Processing").FirstOrDefault().ArtifactID;
					installApplicationToWorkspace.InstallToWorkspace(DefaultWorkspace.ArtifactID, processingAppId);
				}

				Session.SetCleanUp(DefaultWorkspace, false);
				Session.SetWorking(DefaultWorkspace);
			}
		}
	}
}
