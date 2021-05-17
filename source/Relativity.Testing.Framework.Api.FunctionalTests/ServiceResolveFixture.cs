using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests
{
	public class ServiceResolveFixture : ApiTestFixture
	{
		public ServiceResolveFixture()
		{
		}

		public ServiceResolveFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[Test]
		[TestOf(typeof(IMatterService))]
		public void Resolve_IMatterService()
		{
			TestResolve<IMatterService>();
		}

		[Test]
		[TestOf(typeof(IClientService))]
		public void Resolve_IClientService()
		{
			TestResolve<IClientService>();
		}

		[Test]
		[TestOf(typeof(IUserService))]
		public void Resolve_IUserService()
		{
			TestResolve<IUserService>();
		}

		[Test]
		[TestOf(typeof(IGroupService))]
		public void Resolve_IGroupService()
		{
			TestResolve<IGroupService>();
		}

		[Test]
		[TestOf(typeof(IChoiceService))]
		public void Resolve_IChoiceService()
		{
			TestResolve<IChoiceService>();
		}

		[Test]
		[TestOf(typeof(ILibraryApplicationService))]
		public void Resolve_IRelativityApplicationService()
		{
			TestResolve<ILibraryApplicationService>();
		}

		[Test]
		[TestOf(typeof(IWorkspaceService))]
		public void Resolve_IWorkspaceService()
		{
			TestResolve<IWorkspaceService>();
		}

		[Test]
		[TestOf(typeof(IInstanceSettingsService))]
		public void Resolve_IInstanceSettingsService()
		{
			TestResolve<IInstanceSettingsService>();
		}

		[Test]
		[TestOf(typeof(IPermissionService))]
		public void Resolve_IPermissionService()
		{
			TestResolve<IPermissionService>();
		}

		[Test]
		[TestOf(typeof(IObjectTypeService))]
		public void Resolve_IObjectTypeService()
		{
			TestResolve<IObjectTypeService>();
		}

		[Test]
		[TestOf(typeof(IAgentService))]
		public void Resolve_IAgentService()
		{
			TestResolve<IAgentService>();
		}

		[Test]
		[TestOf(typeof(IFieldService))]
		public void Resolve_IFieldService()
		{
			TestResolve<IFieldService>();
		}

		[Test]
		[TestOf(typeof(IMessageOfTheDayService))]
		public void Resolve_IMessageOfTheDayService()
		{
			TestResolve<IMessageOfTheDayService>();
		}

		[Test]
		[TestOf(typeof(IDocumentService))]
		public void Resolve_IDocumentService()
		{
			TestResolve<IDocumentService>();
		}

		[Test]
		[TestOf(typeof(IKeywordSearchService))]
		public void Resolve_IKeywordSearchService()
		{
			TestResolve<IKeywordSearchService>();
		}

		[Test]
		[TestOf(typeof(IResourcePoolService))]
		public void Resolve_IResourcePoolService()
		{
			TestResolve<IResourcePoolService>();
		}

		[Test]
		[TestOf(typeof(IResourceServerService))]
		public void Resolve_IResourceServerService()
		{
			TestResolve<IResourceServerService>();
		}

		[Test]
		[TestOf(typeof(IProductionDataSourceService))]
		public void Resolve_IProductionDataSourceService()
		{
			TestResolve<IProductionDataSourceService>();
		}

		[Test]
		[TestOf(typeof(IProductionPlaceholderService))]
		public void Resolve_IProductionPlaceholderService()
		{
			TestResolve<IProductionPlaceholderService>();
		}

		[Test]
		[TestOf(typeof(IEntityService))]
		public void Resolve_IEntityService()
		{
			TestResolve<IEntityService>();
		}

		[Test]
		[TestOf(typeof(IBatchSetService))]
		public void Resolve_IBatchSetService()
		{
			TestResolve<IBatchSetService>();
		}

		[Test]
		[TestOf(typeof(IBatchService))]
		public void Resolve_IBatchService()
		{
			TestResolve<IBatchService>();
		}

		[Test]
		[TestOf(typeof(ITabService))]
		public void Resolve_ITabService()
		{
			TestResolve<ITabService>();
		}

		[Test]
		[VersionRange(">=12")]
		[TestOf(typeof(ILayoutService))]
		public void Resolve_ILayoutService()
		{
			TestResolve<ILayoutService>();
		}

		[Test]
		[TestOf(typeof(ISearchProviderService))]
		public void Resolve_ISearchProviderService()
		{
			TestResolve<ISearchProviderService>();
		}

		[Test]
		[TestOf(typeof(IErrorService))]
		public void Resolve_IErrorService()
		{
			TestResolve<IErrorService>();
		}

		[Test]
		[VersionRange(">=12.1")]
		[TestOf(typeof(IScriptService))]
		public void Resolve_IScriptService()
		{
			TestResolve<IScriptService>();
		}

		[Test]
		[TestOf(typeof(IOcrProfileService))]
		public void Resolve_IOcrProfileService()
		{
			TestResolve<IOcrProfileService>();
		}

		private void TestResolve<TService>()
		{
			Facade.Resolve<TService>().Should().NotBeNull();
		}
	}
}
