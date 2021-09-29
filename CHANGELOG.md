# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [5.5.0] - 2021-09-29

### Added

- Support for Production Data Source Service V1 API.

## [5.4.0] - 2021-09-21

### Added

- The DataCollection enumeration (All, UsageOnly, None). 

### Changed

- The 'EnableApplicationInsights' configuration key now respects (case-insensitive) the values "All" (same as "True"), "UsageOnly" (new), and "None" (same as "False").

## [5.3.0] - 2021-09-17

### Added

- Support for LibraryApplication Service Delete V1 API

## [5.2.0] - 2021-09-17

### Added

- GetEligibleStatuses method for IClientService. 

## [5.1.0] - 2021-09-15

### Added

- Support for View Manager Service V1 API

## [5.0.0] - 2021-09-10

### Removed

- All Async specific strategies.

## [4.9.0] - 2021-09-09

### Added

- Support for Production Placeholder Manager V1 API

## [4.8.0] - 2021-09-09

### Changed

- Adding additional information to the exception that is thrown when the user is not deleted within the time limit when it occurs from inside the AccountPoolService.

## [4.7.0] - 2021-09-09

### Added

- IObjectService Update method which does not require defining a DTO model.

## [4.6.0] - 2021-09-01

### Changed

- Consumers can now use the ApiComponent against Relativity 12.3

## [4.5.2] - 2021-09-01

### Changed

- Capture additional ApplicationInsights metadata for RingSetup version and Relativity hostname.
- Include custom ApplicationInsights metadata on exceptions in addition to successful requests.

## [4.5.1] - 2021-08-27

### Fixed

- A null reference exception will no longer be thrown if application import validation messages are missing.

## [4.5.0] - 2021-08-19

### Added

- GetAvailableObjectTypes method in ITabService.
- GetEligibleParents method in ITabService.
- GetAdminLevelMetadata method in ITabService.
- GetTabsOrder method in ITabService.
- GetAllForNavigation method in ITabService.

## [4.4.1]- 2021-08-18

### Fixed

- On workspace create, use matter from specified client when matter is not provided.

## [4.4.0] - 2021-08-16

### Added

- GetEligibleStatuses, GetEligibleClients methods, withExtendedMetadata optional parameter for Get and restrictedUpdate optional parameter for Update  methods in IMatterService.

### Changed

- RTF version bumped to 5.0.0.

## [4.3.2]- 2021-08-13

### Fixed

- Creating/Retrieving a script from the admin case will no longer result in a null reference exception.

## [4.3.1] - 2021-08-12

### Fixed

- Throw a JobReportException in JobOnFatalException for the DocumentImportHelper to not swallow up stacktrace.

## [4.3.0] - 2021-08-10

### Added

- Added strategy for dismissing the message of the day by the email address of the user.

## [4.2.0] - 2021-08-06

### Added

- Strategies for Delete, Update Layouts and Get Owners that use API V1.

## [4.1.0] - 2021-08-02

### Fixed

- Return type of GetCategories method in ILayoutService.

## [4.0.0] - 2021-07-30

### Changed

- Return type, from int to long, of Run and RunAsync methods of IImagingJobService.

## [3.28.0] - 2021-07-30

### Added

- Remove inactive imaging jobs method in IImagingEnvironmentService.
- Retrieve the size of a mass imaging job method in IImagingEnvironmentService.

## [3.27.0] - 2021-07-30

### Added

- Get Imaging Document Status methods for IImagingDocumentService.

## [3.26.0] - 2021-07-29

### Added

- Retry Imaging Set Errors methods for IImagingJobService.
- Update Imaging Job Priority methods for IImagingJobService.

## [3.25.0] - 2021-07-29

### Added

- Cancel Imaging Job methods for IImagingJobService.

## [3.24.0] - 2021-07-28

### Added

- SubmitMassImagingJob methods for IImagingJobService.

## [3.23.0] - 2021-07-27

### Added

- Strategies for Create and Read Layouts that use API V1.


## [3.22.0] - 2021-07-26

### Added

- SubmitSingleImage methods for IImagingJobService.

## [3.21.0] - 2021-07-23

### Added

- IsTextOnly method in IMessageOfTheDayService.
- Support for V1 Notifications Manager (MotD) API which will be used for Relativity 12.1+.

## [3.20.0] - 2021-07-22

### Changed

- Tab Strategies will now use v1 versioned API in PriarieSmoke and later
  - Create
  - GetById
  - GetByName
  - Update
  - Delete
  - Require

## [3.19.0] - 2021-07-22

### Added

- WaitForTheJobToComplete methods for IImagingJobService.

## [3.18.0] - 2021-07-22

### Added

- Release methods for IImagingSetService.

## [3.17.0] - 2021-07-21

### Added

- Hide methods for IImagingSetService.

## [3.16.0] - 2021-07-20

### Added

- IImagingJobService with Run methods.

## [3.15.0] - 2021-07-15

### Added

- IApplicationFieldCodeService with CRUD methods for ApplicationFieldCode.

## [3.14.0] - 2021-07-15

### Added

- Delete and DeleteAsync methods for IImagingSetService.

## [3.13.0] - 2021-07-14

### Added

- Update and UpdateAsync methods for IImagingSetService.

## [3.12.1] - 2021-07-14

### Fixed

- InvalidOperationException when calling RelyOn<ApiComponent> on RelativityFacade with EnableApplicationInsights setting set to false.

## [3.12.0] - 2021-07-14

### Added

- GetStatus and GetStatusAsync methods for IImagingSetService.

## [3.11.0] - 2021-07-13

### Added

- IImagingSetService with Get and Create methods for ImagingSet.

## [3.10.0] - 2021-07-12

### Added

- IImagingNativeTypeService with Get method NativeType.

## [3.9.0] - 2021-07-09

### Added

- IImagingService with methods for CRUD operations on ImagingProfile.

## [3.8.0] - 2021-07-08

### Changed

- InstanceSetting Update and Create operations will now use v1 API for Relativity version 12.1 and higher.

## [3.7.0] - 2021-07-08

### Added

- IKeyboardShortcutsService with method for retrieving Keyboard Shortucts for workspace.
### Changed

- RTF version changed to v4.5.0.

## [3.6.0] - 2021-07-07

### Changed

- InstanceSetting Read and Delete operations will now use v1 API for Relativity version 12.1 and higher.

## [3.5.0] - 2021-07-05

### Added

- Async method for IPermissionService methods using IDs.

## [3.4.0] - 2021-07-05

### Added

- Async method for IHttpService.
## [3.3.0] - 2021-07-02

### Added

- Batch Set Update and Delete operations for version 12.1 and higher.

### Changed

- Batch Set requests will now use the V1 versioned API for 12.1 and higher.

## [3.2.3] - 2021-06-29

### Fixed

- Sending of metrics to ApplicationInsights.

## [3.2.2] - 2021-06-29

### Removed

- Duplicated ExpressionExtensions class.

## [3.2.1] - 2021-06-25

### Fixed

- Added NUnit to the dependencies for the project/package.

## [3.2.0] - 2021-06-24

### Changed

- Client requests will now use the V1 versioned API for 12.1 and higher.

## [3.1.0] - 2021-06-23

### Added

- Batch Checkout and Batch Checkin method in BatchService.

## [3.0.1] - 2021-06-18

### Fixed

- Choice creation now respects the parent field on the choice model.

## [3.0.0] - 2021-06-14

### Removed

- Strategies and services implementations made internal.

### Added

- Get by ID for IUserService.

## [2.3.0] - 2021-06-11

### Added

- Ability to perform action as a particular user in BatchSetService.

## [2.2.1] - 2021-06-11

### Fixed

- User.Require() with ensureNew=true will now also poll for deletion before attempting to recreate the user if the user was specifyed by email, instead of by ArtifactID.

## [2.2.0] - 2021-06-10

### Changed

- User.Require() with ensureNew=true will now poll for deletion before attempting to recreate the user.

## [2.1.1] - 2021-06-09

### Changed

- RTF version changed to v3.1.3.

## [2.1.0] - 2021-06-03

### Added

- PrairieSmoke support.

## [2.0.0] - 2021-05-28

### Changed

- Properties in ObjectType class to reflect properties of ObjectType API DTOs.

## [1.2.3] - 2021-05-27

### Fixed

- Set included assets for Relativity.DataExchange.Client.SDK to also contain build.

## [1.2.2] - 2021-05-27

### Added

-  Polling mechanism in-between the deletion and creation of user in AccountPoolService.

## [1.2.1] - 2021-05-26

### Added

- Update error message in AddWorkspaceToGroups with artifactIds.

## [1.2.0] - 2021-05-17

### Added

- EnsureEnvironmentCanRunScripts strategy.

## [1.1.1] - 2021-05-13

### Migrating to GitHub. No Changes to package.

## [1.1.0] - 2021-05-04

### Added

- IdentityFieldId setting for Image import Job.
- FileField strategies
  - DownloadFile
  - UploadFile

### Changed

- License is now checked into the repository instead of generated at build time.
- Permission Services support both ID and Names.
- IKeplerService no longer has a class constraint.

## [1.0.0] - 2021-04-16

### Added

- Castle.Windsor and Castle.Core version limitation caused by RTF package.
- Osier support.

### Removed

- RetryInterceptor from LibraryApplicationService and HttpService, which was hiding original error message from InstallToLibrary method.

### Changed

- Altered WaitUserAddedToGroupStrategy to wait 30s only if lockbox is enabled.
- Update namespaces to better adhere to company standards.
- Pinned minimum version of 1.0.0 for RTF.

### Fixed

- Fixing spelling of SetLength in the Query functions.

## [0.29.0] - 2021-04-02

### Added

- Moved in TestArrangement code and related strategies.
- Layout Strategies
  - GetCategories
  - AddFields

### Changed

- Fix Agent RunInterval field value mapping.
- Updated RestService with the option to return a raw response.
- RsapiObjectExistsByStrategy renamed to ObjectQueryExistsByIdStrategy and now uses IObjectService in place of IRsapiObjectService.

### Removed

- Removed Rsapi dependencies.
- Support for versions of Relativity prior to 11.3.
- Stratiegies for Relativity 11.3 and lower.

### Fixed

- Fix for passing ObjectType when creating tab with LinkType different then Object.
- Agent:Update will now look up the current RunInterval of the agent to be update, if that property is not set on the model.
- CurrencyField model can now be used in conjunction with the FieldService.

## [0.28.0] - 2021-03-10

### Added

- ObjectManager Strategies
  - MassDelete
  - MassCreate
  - MassUpdate
- Added polling logic to UserAddToGroupStrategy.

### Changed

- Warn instead of Error when running outside of the verified Relativity version range.
- Item permission service use name instead of ID for group identifier.
- Rename RelativityApplication services to match LibraryApplication strategies.

### Fixed

- A potential null value exception when waiting for an application to be installed.

## [0.27.0] - 2021-02-12

### Added

- Script Strategies
  - EnqueueRunJob
  - ReadRunJob
  - QueryActionJobResults
  - RunStatusAction
  - RunTableAction
  - Preview
- AccountPool Strategies
  - DeleteAndAcquireStandardAccount
- SetItemPermissions.
- Contributing guide.

### Changed

- Various permissions related strategies now use ArtifactId instead of name, and also lock to help prevent stale permissions requests.
- ObjectManager requests will now use the ObjectTypeGuid Attribute to look up the object using the specified Guid.
- ObjectManager requests will now look for FieldGuid and FieldArtifactId attributes on fields and mapping them back to the fields on the model.
- Workspace:Delete now explicitly waits for the workspace to be deleted before proceeding.

## [0.26.0] - 2021-01-30

### Changed

- Made most all strategy interfaces internal and moved most of them over from the core package.
- Migrate IAccountPoolService into RTF.API.

### Fixed

- HttpClient will now only be created once when the service is instantiated, instead of for every request.

## [0.25.0] - 2021-01-15

### Added

- OcrProfile Strategies
  - Create
  - Delete
  - GetById
- AdminPermissionService.

### Changed

- Added note about respecting environment rules in workspace create xml documentation.
- Client status property is now a NamedArtifact, instead of an enum.
- RSAPI ensuring will now only run on versions lower than 11.3.
- Split PermissionService into WorkspacePermissionService, ItemPermissionService, and the new AdminPermissionService.
- LibraryApplication strategies now take advantage of the `relavity-environment/v1` APIs when testing against 12.1 and newer versions of Relativity.

### Fixed

- AgentGetById strategy will now return AgentType and AgentServer information.
- Various group related strategy now use a Lock to prevent the GroupSelector from becoming stale and causing issues.

## [0.24.0] - 2020-12-22

### Added

- ObjectType Strategies
  - GetDependencyList
  - GetAvailableParentObjectTpes
- Script Strategies
  - Create
  - Read
  - Update
  - Delete

### Changed

- Workspace create requests will now time out after 300 seconds instead of 100.

## [0.23.0] - 2020-12-07

### Added

- SearchProvider Strategies
  - Create
  - Read
  - Update
  - Delete
  - Require
  - GetDependenciesList
- Error Strategies
  - Create
  - Get
- Permissions Strategies
  - GetAdminGroupUsers
  - GetWorkspaceGroupUsers

### Changed

- API requests will retry once if the request encountered an internal server error.

## [0.22.0] - 2020-11-23

### Added

- Ninebark support.
- Layout Strategies
  - Create
  - Read
  - GetEligibleOwnersse
  - Delete
  - Require

### Removed

- Goatsbeard support.

## [0.21.0] - 2020-11-09

### Added

- User Strategies
  - Require
- ResourcePool Strategies
  - QueryEligibleResources
  - QueryClients
  - AddResources
  - RemoveResources
- Tab Strategies
  - Create
  - Read
  - Delete
  - Update
  - Require

## [0.20.0] - 2020-10-26

### Added

- ResourcePool Strategies
  - QueryResources
- View Strategies
  - GetAll

### Changed

- ApiComponent will now retry a few times if the Ensure method fails to talk to REST or RSAPI.

## [0.19.0] - 2020-10-12

### Added

- Entity Strategies
  - Create
  - Get
  - GetAll
  - Update
  - Require
  - Delete
- BatchSet Strategies
  - Create
  - Get
  - Create Batches
  - Purge Batches
- Batch Strategies
  - Get
  - GetAll
  - AssignToUser
  - Query
- View Strategies
  - Create
  - Read
  - Update
  - Require
  - AccessStatus
  - ViewOwner
- ResourcePool Strategies
  - Create
  - Read
  - Update
  - Delete

### Fixed

- Catching a potential exception when a production is deleted halfway though the GetProductionsById method.

### Removed

- Support for 10.2 versions of Realtivity.

## [0.18.0] - 2020-09-28

### Added

- ObjectManager Strategies
  - Create
  - Update
  - Delete
- ProductionSet Strategies
  - Stage
  - Run
  - WaitForStatus
- Document Strategies
  - Get
  - Delete
- Markup Set Strategies
  - Create
  - Read
  - Delete
  - Require
- Field Strategies
  - Require

## [0.17.0] - 2020-09-14

### Added

- Strategies for running production sets
  - Get Production Status Strategy
- Production Data Source Strategies
  - Read
  - Update
- User Group Email Notifications to the User model.

- Production Placeholder Strategies
  - Create
  - Read
  - Update
  - Delete
- Implement Await function for Production.
- Implement Await function for Production Data Source.

### Changed

- Update User model and properties due to changes for 11.3.

### Fixed

- RSAPI over REST calls are now correctly using the REST URL, instead of the RSAPI URL.

## [0.16.0] - 2020-08-31

### Added

- Production Data Source Strategies
  - Create
  - Delete

## [0.15.0] - 2020-08-17

### Added

- Resource pool Strategies
  - GetAll
- Resource server Strategies
  - GetAll
- Production Set Strategies
  - Create
  - Read
  - Delete

### Changed

- Workspace methods now use REST for Relativity versions 11.3.0 and higher
  - Create

### Fixed

- User requests to Relativity 11.3 now correctly map returned `FirstName`, `LastName`, `EmailAddress`, and `Password` values.

## [0.14.0] - 2020-08-03

### Added

- Group Strategies
  - Update
  - Require
- Matter Strategies
  - Update
  - Require
- Agent Strategies
  - Update
  - Require

### Changed

- Group methods now use REST for Relativity versions 11.3.0 and higher
  - DeleteGroup
  - CreateGroup
- Field Strategies
  - Add Field Propagation to base Field functionality
  - Add Relational Fields to FixedLengthTextField model

## [0.13.0] - 2020-07-20

### Added

- KeywordSearch Strategies
  - Require
  - Query
- Add AccountPool functionality for multiple users during UI tests.

### Changed

- User methods now use REST for Relativity versions 11.3.0 and higher
  - CreateUser
  - UserExistsByEmail
  - DeleteUser
  - GetUserById
  - GetUserByEmail

## [0.12.0] - 2020-07-06

### Added

- Workspace Strategies
  - CreateWithDocs
- KeywordSearch Strategies
  - Get
  - Create
  - Delete
  - Update

### Fixed

- Declare dependencies targeting a specific build number.
- Use passed in WorkspaceAdminGroup when requesting workspace creation.

## [0.11.0] - 2020-06-22

### Added

- Document Strategies
  - GetAll
  - Import

### Fixed

- Temporary fix for `Yes/No`, `Fixed-Length Text`, and `ObjectType` field mapping for IObjectService requests.

## [0.10.0] - 2020-06-08

### Added

- Lanceleaf support.
- User Strategies
  - RemoveFromGroup

### Fixed

- Using PerformRelativityVersionCheck will no longer cause an issue with lazy loading the RelativityFacade.
- Multiversion support.

## [0.9.0] - 2020-05-25

### Added

- Choice Strategies
  - Get
  - Create
  - Update
  - Require
  - Delete
- MOTD Strategies
  - Get
  - Update
  - Dismiss
  - HadDismissed
