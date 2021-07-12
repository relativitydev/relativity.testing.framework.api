# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [3.10.0] - 2021-07-12

### Added

- IImagingNativeTypeService with Get method NativeType. - [RTF-1251](https://jira.kcura.com/browse/RTF-1251)

## [3.9.0] - 2021-07-09

### Added

- IImagingService with methods for CRUD operations on ImagingProfile. - [RTF-1249](https://jira.kcura.com/browse/RTF-1249)

## [3.8.0] - 2021-07-08

### Changed

- InstanceSetting Update and Create operations will now use v1 API for Relativity version 12.1 and higher. - [RTF-1256](https://jira.kcura.com/browse/RTF-1256)

## [3.7.0] - 2021-07-08

### Added

- IKeyboardShortcutsService with method for retrieving Keyboard Shortucts for workspace. - [RTF-1257](https://jira.kcura.com/browse/RTF-1257)

### Changed

- RTF version changed to v4.5.0. - [RTF-1257](https://jira.kcura.com/browse/RTF-1257)

## [3.6.0] - 2021-07-07

### Changed

- InstanceSetting Read and Delete operations will now use v1 API for Relativity version 12.1 and higher. - [RTF-1256](https://jira.kcura.com/browse/RTF-1256)

## [3.5.0] - 2021-07-05

### Added

- Async method for IPermissionService methods using IDs. - [TESTENG-1276](https://jira.kcura.com/browse/TESTENG-1276)

## [3.4.0] - 2021-07-05

### Added

- Async method for IHttpService. - [TESTENG-1275](https://jira.kcura.com/browse/TESTENG-1275)

## [3.3.0] - 2021-07-02

### Added

- Batch Set Update and Delete operations for version 12.1 and higher. - [RTF-1245](https://jira.kcura.com/browse/RTF-1245)

### Changed

- Batch Set requests will now use the V1 versioned API for 12.1 and higher. - [RTF-1245](https://jira.kcura.com/browse/RTF-1245)

## [3.2.3] - 2021-06-29

### Fixed

- Sending of metrics to ApplicationInsights. - https://github.com/relativitydev/relativity.testing.framework.api/issues/46

## [3.2.2] - 2021-06-29

### Removed

- Duplicated ExpressionExtensions class. - [TESTENG-1273](https://jira.kcura.com/browse/TESTENG-1273)

## [3.2.1] - 2021-06-25

### Fixed

- Added NUnit to the dependencies for the project/package. - [TESTENG-477](https://jira.kcura.com/browse/TESTENG-477)

## [3.2.0] - 2021-06-24

### Changed

- Client requests will now use the V1 versioned API for 12.1 and higher. - [RTF-1247](https://jira.kcura.com/browse/RTF-1247)

## [3.1.0] - 2021-06-23

### Added

- Batch Checkout and Batch Checkin method in BatchService - [RTF-1246](https://jira.kcura.com/browse/RTF-1246)

## [3.0.1] - 2021-06-18

### Fixed

- Choice creation now respects the parent field on the choice model. - [TESTENG-1218](https://jira.kcura.com/browse/TESTENG-1218)

## [3.0.0] - 2021-06-14

### Removed

- Strategies and services implementations made internal - [RTF-1229](https://jira.kcura.com/browse/RTF-1229)

### Added

- Get by ID for IUserService. - [RTF-1229](https://jira.kcura.com/browse/RTF-1229)

## [2.3.0] - 2021-06-11

### Added

- Ability to perform action as a particular user in BatchSetService. - [RTF-1100](https://jira.kcura.com/browse/RTF-1100)

## [2.2.1] - 2021-06-11

### Fixed

- User.Require() with ensureNew=true will now also poll for deletion before attempting to recreate the user if the user was specifyed by email, instead of by ArtifactID. - [TESTENG-1128](https://jira.kcura.com/browse/TESTENG-1128)

## [2.2.0] - 2021-06-10

### Changed

- User.Require() with ensureNew=true will now poll for deletion before attempting to recreate the user. - [TESTENG-1128](https://jira.kcura.com/browse/TESTENG-1128)

## [2.1.1] - 2021-06-09

### Changed

- RTF version changed to v3.1.3. - [TESTENG-908](https://jira.kcura.com/browse/TESTENG-908)

## [2.1.0] - 2021-06-03

### Added

- PrairieSmoke support. - [RTF-1225](https://jira.kcura.com/browse/RTF-1225)

## [2.0.0] - 2021-05-28

### Changed

- Properties in ObjectType class to reflect properties of ObjectType API DTOs. - [TESTENG-898](https://jira.kcura.com/browse/TESTENG-898)

## [1.2.3] - 2021-05-27

### Fixed

- Set included assets for Relativity.DataExchange.Client.SDK to also contain build. - [TESTENG-1143](https://jira.kcura.com/browse/TESTENG-1143)

## [1.2.2] - 2021-05-27

### Added

-  Polling mechanism in-between the deletion and creation of user in AccountPoolService. - [TESTENG-1128](https://jira.kcura.com/browse/TESTENG-1128)

## [1.2.1] - 2021-05-26

### Added

- Update error message in AddWorkspaceToGroups with artifactIds. - [TESTENG-922](https://jira.kcura.com/browse/TESTENG-922)

## [1.2.0] - 2021-05-17

### Added

- EnsureEnvironmentCanRunScripts strategy. - [TESTENG-1089](https://jira.kcura.com/browse/TESTENG-1089)

## [1.1.1] - 2021-05-13

### Migrating to GitHub. No Changes to package.

## [1.1.0] - 2021-05-04

### Added

- IdentityFieldId setting for Image import Job. - [TESTENG-1045](https://jira.kcura.com/browse/TESTENG-1045)
- FileField strategies - [RTF-1129](https://jira.kcura.com/browse/RTF-1129)
  - DownloadFile
  - UploadFile

### Changed

- License is now checked into the repository instead of generated at build time. - [RTF-1012](https://jira.kcura.com/browse/RTF-1012)
- Permission Services support both ID and Names. - [RTF-1080](https://jira.kcura.com/browse/RTF-1080)
- IKeplerService no longer has a class constraint. - [RTF-1191](https://jira.kcura.com/browse/RTF-1191)

## [1.0.0] - 2021-04-16

### Added

- Castle.Windsor and Castle.Core version limitation caused by RTF package. - [RTF-1121](https://jira.kcura.com/browse/RTF-1121)
- Osier support - [RTF-1078](https://jira.kcura.com/browse/RTF-1078)

### Removed

- RetryInterceptor from LibraryApplicationService and HttpService, which was hiding original error message from InstallToLibrary method. - [RTF-1048]](https://jira.kcura.com/browse/RTF-1048)

### Changed

- Altered WaitUserAddedToGroupStrategy to wait 30s only if lockbox is enabled. - [RTF-1122](https://jira.kcura.com/browse/RTF-1122)
- Update namespaces to better adhere to company standards. - [RTF-1107](https://jira.kcura.com/browse/RTF-1107)
- Pinned minimum version of 1.0.0 for RTF [RTF-1083](https://jira.kcura.com/browse/RTF-1083)

### Fixed

- Fixing spelling of SetLength in the Query functions - [TESTENG-978](https://jira.kcura.com/browse/TESTENG-978)

## [0.29.0] - 2021-04-02

### Added

- Moved in TestArrangement code and related strategies [RTF-963](https://jira.kcura.com/browse/RTF-963)
- Layout Strategies [RTF-981](https://jira.kcura.com/browse/RTF-981)
  - GetCategories
  - AddFields

### Changed

- Fix Agent RunInterval field value mapping - [RTF-1036](https://jira.kcura.com/browse/RTF-1036)
- Updated RestService with the option to return a raw response - [TESTENG-860](https://jira.kcura.com/browse/TESTENG-860)
- RsapiObjectExistsByStrategy renamed to ObjectQueryExistsByIdStrategy and now uses IObjectService in place of IRsapiObjectService - [RTF-1074](https://jira.kcura.com/browse/RTF-1074)

### Removed

- Removed Rsapi dependencies - [RTF-953](https://jira.kcura.com/browse/RTF-953)
- Support for versions of Relativity prior to 11.3 - [RTF-1120](https://jira.kcura.com/browse/RTF-1120)
- Stratiegies for Relativity 11.3 and lower - [RTF-1124](https://jira.kcura.com/browse/RTF-1124)

### Fixed

- Fix for passing ObjectType when creating tab with LinkType different then Object - [RTF-1061](https://jira.kcura.com/browse/RTF-1061)
- Agent:Update will now look up the current RunInterval of the agent to be update, if that property is not set on the model- [RTF-1039](https://jira.kcura.com/browse/RTF-1039)
- CurrencyField model can now be used in conjunction with the FieldService. - [TESTENG-906](https://jira.kcura.com/browse/TESTENG-906)

## [0.28.0] - 2021-03-10

### Added

- ObjectManager Strategies
  - MassDelete - [RTF-1026](https://jira.kcura.com/browse/RTF-1026)
  - MassCreate - [RTF-1027](https://jira.kcura.com/browse/RTF-1027)
  - MassUpdate - [RTF-1028](https://jira.kcura.com/browse/RTF-1028)
- Added polling logic to UserAddToGroupStrategy  [TESTENG-720](https://jira.kcura.com/browse/TESTENG-720)

### Changed

- Warn instead of Error when running outside of the verified Relativity version range - [RTF-1011](https://jira.kcura.com/browse/RTF-1011)
- Item permission service use name instead of ID for group identifier - [RTF-1050](https://jira.kcura.com/browse/RTF-1050)
- Rename RelativityApplication services to match LibraryApplication strategies - [RTF-1029](https://jira.kcura.com/browse/RTF-1029)

### Fixed

- A potential null value exception when waiting for an application to be installed. - [RTF-1043](https://jira.kcura.com/browse/RTF-1043)

## [0.27.0] - 2021-02-12

### Added

- Script Strategies
  - EnqueueRunJob [RTF-998](https://jira.kcura.com/browse/RTF-998)
  - ReadRunJob [RTF-998](https://jira.kcura.com/browse/RTF-998)
  - QueryActionJobResults [RTF-998](https://jira.kcura.com/browse/RTF-998)
  - RunStatusAction [RTF-998](https://jira.kcura.com/browse/RTF-998)
  - RunTableAction [RTF-998](https://jira.kcura.com/browse/RTF-998)
  - Preview [RTF-999](https://jira.kcura.com/browse/RTF-999)
- AccountPool Strategies
  - DeleteAndAcquireStandardAccount - [TESTENG-742](https://jira.kcura.com/browse/TESTENG-742)
- SetItemPermissions - [RTF-985](https://jira.kcura.com/browse/RTF-985)
- Contributing guide - [RTF-92](https://jira.kcura.com/browse/RTF-92)

### Changed

- Various permissions related strategies now use ArtifactId instead of name, and also lock to help prevent stale permissions requests. - [RTF-985](https://jira.kcura.com/browse/RTF-985)
- ObjectManager requests will now use the ObjectTypeGuid Attribute to look up the object using the specified Guid. - [RTF-1020](https://jira.kcura.com/browse/RTF-1020)
- ObjectManager requests will now look for FieldGuid and FieldArtifactId attributes on fields and mapping them back to the fields on the model. - [RTF-1025](https://jira.kcura.com/browse/RTF-1025)
- Workspace:Delete now explicitly waits for the workspace to be deleted before proceeding. - [RTF-992](https://jira.kcura.com/browse/RTF-992)

## [0.26.0] - 2021-01-30

### Changed

- Made most all strategy interfaces internal and moved most of them over from the core package. - [RTF-754](https://jira.kcura.com/browse/RTF-754)
- Migrate IAccountPoolService into RTF.API - [RTF-997](https://jira.kcura.com/browse/RTF-997)

### Fixed

- HttpClient will now only be created once when the service is instantiated, instead of for every request. - [RTF-994](https://jira.kcura.com/browse/RTF-994)

## [0.25.0] - 2021-01-15

### Added

- OcrProfile Strategies
  - Create [RTF-898](https://jira.kcura.com/browse/RTF-898)
  - Delete [RTF-899](https://jira.kcura.com/browse/RTF-899)
  - GetById [RTF-872](https://jira.kcura.com/browse/RTF-872)
- AdminPermissionService [RTF-946](https://jira.kcura.com/browse/RTF-946)

### Changed

- Added note about respecting environment rules in workspace create xml documentation. - [RTF-904](https://jira.kcura.com/browse/RTF-904)
- Client status property is now a NamedArtifact, instead of an enum. - [RTF-947](https://jira.kcura.com/browse/RTF-947)
- RSAPI ensuring will now only run on versions lower than 11.3. - [RTF-920](https://jira.kcura.com/browse/RTF-920)
- Split PermissionService into WorkspacePermissionService, ItemPermissionService, and the new AdminPermissionService. - [RTF-946](https://jira.kcura.com/browse/RTF-946)
- LibraryApplication strategies now take advantage of the `relavity-environment/v1` APIs when testing against 12.1 and newer versions of Relativity. - [RTF-814](https://jira.kcura.com/browse/RTF-814)

### Fixed

- AgentGetById strategy will now return AgentType and AgentServer information. - [RTF-889](https://jira.kcura.com/browse/RTF-889)
- Various group related strategy now use a Lock to prevent the GroupSelector from becoming stale and causing issues. - [RTF-860](https://jira.kcura.com/browse/RTF-860)

## [0.24.0] - 2020-12-22

### Added

- ObjectType Strategies
  - GetDependencyList - [RTF-880](https://jira.kcura.com/browse/RTF-880)
  - GetAvailableParentObjectTypes - [RTF-879](https://jira.kcura.com/browse/RTF-879)
- Script Strategies
  - Create - [RTF-874](https://jira.kcura.com/browse/RTF-874)
  - Read - [RTF-875](https://jira.kcura.com/browse/RTF-875)
  - Update - [RTF-876](https://jira.kcura.com/browse/RTF-876)
  - Delete - [RTF-877](https://jira.kcura.com/browse/RTF-877)

### Changed

- Workspace create requests will now time out after 300 seconds instead of 100. - [RTF-903](https://jira.kcura.com/browse/RTF-903)

## [0.23.0] - 2020-12-07

### Added

- SearchProvider Strategies
  - Create - [RTF-712](https://jira.kcura.com/browse/RTF-712)
  - Read - [RTF-713](https://jira.kcura.com/browse/RTF-713)
  - Update - [RTF-714](https://jira.kcura.com/browse/RTF-714)
  - Delete - [RTF-715](https://jira.kcura.com/browse/RTF-715)
  - Require - [RTF-716](https://jira.kcura.com/browse/RTF-716)
  - GetDependenciesList - [RTF-717](https://jira.kcura.com/browse/RTF-717)
- Error Strategies
  - Create - [RTF-846](https://jira.kcura.com/browse/RTF-846)
  - Get - [RTF-847](https://jira.kcura.com/browse/RTF-847)
- Permissions Strategies
  - GetAdminGroupUsers [RTF-858](https://jira.kcura.com/browse/RTF-858)
  - GetWorkspaceGroupUsers [RTF-857](https://jira.kcura.com/browse/RTF-857)

### Changed

- API requests will retry once if the request encountered an internal server error. [RTF-771](https://jira.kcura.com/browse/RTF-771)

## [0.22.0] - 2020-11-23

### Added

- Ninebark support - [RTF-833](https://jira.kcura.com/browse/RTF-833)
- Layout Strategies
  - Create - [RTF-840](https://jira.kcura.com/browse/RTF-840)
  - Read - [RTF-841](https://jira.kcura.com/browse/RTF-841)
  - GetEligibleOwners - [RTF-842](https://jira.kcura.com/browse/RTF-842)
  - Delete - [RTF-843](https://jira.kcura.com/browse/RTF-843)
  - Require -[RTF-844](https://jira.kcura.com/browse/RTF-844)

### Removed

- Goatsbeard support

## [0.21.0] - 2020-11-09

### Added

- User Strategies
  - Require - [RTF-508](https://jira.kcura.com/browse/RTF-508)
- ResourcePool Strategies
  - QueryEligibleResources - [RTF-741](https://jira.kcura.com/browse/RTF-741)
  - QueryClients - [RTF-742](https://jira.kcura.com/browse/RTF-742)
  - AddResources - [RTF-743](https://jira.kcura.com/browse/RTF-743)
  - RemoveResources - [RTF-744](https://jira.kcura.com/browse/RTF-744)
- Tab Strategies
  - Create - [RTF-763](https://jira.kcura.com/browse/RTF-763)
  - Read - [RTF-764](https://jira.kcura.com/browse/RTF-764)
  - Delete - [RTF-766](https://jira.kcura.com/browse/RTF-766)
  - Update - [RTF-765](https://jira.kcura.com/browse/RTF-765)
  - Require - [RTF-767](https://jira.kcura.com/browse/RTF-767)

## [0.20.0] - 2020-10-26

### Added

- ResourcePool Strategies
  - QueryResources - [RTF-740](https://jira.kcura.com/browse/RTF-740)
- View Strategies
  - GetAll - [RTF-746](https://jira.kcura.com/browse/RTF-746)

### Changed

- ApiComponent will now retry a few times if the Ensure method fails to talk to REST or RSAPI. - [RTF-769](https://jira.kcura.com/browse/RTF-769)

## [0.19.0] - 2020-10-12

### Added

- Entity Strategies
  - Create - [RTF-693](https://jira.kcura.com/browse/RTF-693)
  - Get - [RTF-694](https://jira.kcura.com/browse/RTF-694)
  - GetAll - [RTF-694](https://jira.kcura.com/browse/RTF-694)
  - Update - [RTF-695](https://jira.kcura.com/browse/RTF-695)
  - Require - [RTF-697](https://jira.kcura.com/browse/RTF-697)
  - Delete - [RTF-696](https://jira.kcura.com/browse/RTF-696)
- BatchSet Strategies
  - Create - [RTF-706](https://jira.kcura.com/browse/RTF-706)
  - Get - [RTF-736](https://jira.kcura.com/browse/RTF-736)
  - Create Batches - [RTF-732](https://jira.kcura.com/browse/RTF-732)
  - Purge Batches - [RTF-738](https://jira.kcura.com/browse/RTF-738)
- Batch Strategies
  - Get - [RTF-707](https://jira.kcura.com/browse/RTF-707)
  - GetAll - [RTF-707](https://jira.kcura.com/browse/RTF-707)
  - AssignToUser - [RTF-708](https://jira.kcura.com/browse/RTF-708)
  - Query - [RTF-711](https://jira.kcura.com/browse/RTF-711)
- View Strategies
  - Create - [RTF-701](https://jira.kcura.com/browse/RTF-701)
  - Read - [RTF-702](https://jira.kcura.com/browse/RTF-702)
  - Update - [RTF-703](https://jira.kcura.com/browse/RTF-703)
  - Require - [RTF-705](https://jira.kcura.com/browse/RTF-705)
  - AccessStatus - [RTF-733](https://jira.kcura.com/browse/RTF-733)
  - ViewOwner - [RTF-734](https://jira.kcura.com/browse/RTF-734)
- ResourcePool Strategies
  - Create - [RTF-749](https://jira.kcura.com/browse/RTF-749)
  - Read - [RTF-698](https://jira.kcura.com/browse/RTF-698)
  - Update - [RTF-699](https://jira.kcura.com/browse/RTF-699)
  - Delete - [RTF-700](https://jira.kcura.com/browse/RTF-700)

### Fixed

- Catching a potential exception when a production is deleted halfway though the GetProductionsById method. - [RTF-721](https://jira.kcura.com/browse/RTF-721)

### Removed

- Support for 10.2 versions of Realtivity. [RTF-658](https://jira.kcura.com/browse/RTF-658)

## [0.18.0] - 2020-09-28

### Added

- ObjectManager Strategies
  - Create - [RTF-657](https://jira.kcura.com/browse/RTF-657)
  - Update - [RTF-652](https://jira.kcura.com/browse/RTF-652)
  - Delete - [RTF-651](https://jira.kcura.com/browse/RTF-651)
- ProductionSet Strategies
  - Stage [RTF-567](https://jira.kcura.com/browse/RTF-567)
  - Run [RTF-568](https://jira.kcura.com/browse/RTF-568)
  - WaitForStatus [RTF-584](https://jira.kcura.com/browse/RTF-584)
- Document Strategies
  - Get - [RTF-649](https://jira.kcura.com/browse/RTF-649)
  - Delete - [RTF-663](https://jira.kcura.com/browse/RTF-663)
- Markup Set Strategies
  - Create - [RTF-642](https://jira.kcura.com/browse/RTF-642)
  - Read - [RTF-643](https://jira.kcura.com/browse/RTF-643)
  - Delete - [RTF-645](https://jira.kcura.com/browse/RTF-645)
  - Require - [RTF-646](https://jira.kcura.com/browse/RTF-646)
- Field Strategies
  - Require - [RTF-406](https://jira.kcura.com/browse/RTF-406)

## [0.17.0] - 2020-09-14

### Added

- Strategies for running production sets
  - Get Production Status Strategy [RTF-569](https://jira.kcura.com/browse/RTF-569)
- Production Data Source Strategies
  - Read - [RTF-571](https://jira.kcura.com/browse/RTF-571)
  - Update - [RTF-572](https://jira.kcura.com/browse/RTF-572)
- User Group Email Notifications to the User model  - [RTF-507](https://jira.kcura.com/browse/RTF-507)

- Production Placeholder Strategies
  - Create - [RTF-625](https://jira.kcura.com/browse/RTF-625)
  - Read - [RTF-626](https://jira.kcura.com/browse/RTF-626)
  - Update - [RTF-627](https://jira.kcura.com/browse/RTF-627)
  - Delete - [RTF-628](https://jira.kcura.com/browse/RTF-628)
- Implement Await function for Production - [RTF-623](https://jira.kcura.com/browse/RTF-623)
- Implement Await function for Production Data Source - [RTF-624](https://jira.kcura.com/browse/RTF-624)

### Changed

- Update User model and properties due to changes for 11.3 - [RTF-590](https://jira.kcura.com/browse/RTF-590)

### Fixed

- RSAPI over REST calls are now correctly using the REST URL, instead of the RSAPI URL. - [RTF-631](https://jira.kcura.com/browse/RTF-631)

## [0.16.0] - 2020-08-31

### Added

- Production Data Source Strategies
  - Create - [RTF-570](https://jira.kcura.com/browse/RTF-570)
  - Delete - [RTF-573](https://jira.kcura.com/browse/RTF-573)

## [0.15.0] - 2020-08-17

### Added

- Resource pool Strategies
  - GetAll - [RTF-557](https://jira.kcura.com/browse/RTF-557)
- Resource server Strategies
  - GetAll - [RTF-557](https://jira.kcura.com/browse/RTF-557)
- Production Set Strategies
  - Create - [RTF-564](https://jira.kcura.com/browse/RTF-564)
  - Read - [RTF-565](https://jira.kcura.com/browse/RTF-565)
  - Delete - [RTF-566](https://jira.kcura.com/browse/RTF-566)

### Changed

- Workspace methods now use REST for Relativity versions 11.3.0 and higher
  - Create - [RTF-557](https://jira.kcura.com/browse/RTF-557)

### Fixed

- User requests to Relativity 11.3 now correctly map returned `FirstName`, `LastName`, `EmailAddress`, and `Password` values. [RTF-575](https://jira.kcura.com/browse/RTF-575)

## [0.14.0] - 2020-08-03

### Added

- Group Strategies
  - Update - [RTF-552](https://jira.kcura.com/browse/RTF-552)
  - Require - [RTF-553](https://jira.kcura.com/browse/RTF-553)
- Matter Strategies
  - Update - [RTF-550](https://jira.kcura.com/browse/RTF-550)
  - Require - [RTF-551](https://jira.kcura.com/browse/RTF-551)
- Agent Strategies
  - Update - [RTF-544](https://jira.kcura.com/browse/RTF-544)
  - Require - [RTF-544](https://jira.kcura.com/browse/RTF-544)

### Changed

- Group methods now use REST for Relativity versions 11.3.0 and higher
  - DeleteGroup - [RTF-540](https://jira.kcura.com/browse/RTF-540)
  - CreateGroup - [RTF-539](https://jira.kcura.com/browse/RTF-539)
- Field Strategies
  - Add Field Propagation to base Field functionality - [REL-450762](https://jira.kcura.com/browse/REL-450762)
  - Add Relational Fields to FixedLengthTextField model - [RTF-558](https://jira.kcura.com/browse/RTF-558)

## [0.13.0] - 2020-07-20

### Added

- KeywordSearch Strategies
  - Require - [RTF-518](https://jira.kcura.com/browse/RTF-518)
  - Query - [RTF-520](https://jira.kcura.com/browse/RTF-520)
- Add AccountPool functionality for multiple users during UI tests - [RTF-489](https://jira.kcura.com/browse/RTF-489)

### Changed

- User methods now use REST for Relativity versions 11.3.0 and higher
  - CreateUser - [RTF-533](https://jira.kcura.com/browse/RTF-533)
  - UserExistsByEmail - [RTF-535](https://jira.kcura.com/browse/RTF-535)
  - DeleteUser - [RTF-534](https://jira.kcura.com/browse/RTF-534)
  - GetUserById - [RTF-559](https://jira.kcura.com/browse/RTF-559)
  - GetUserByEmail - [RTF-536](https://jira.kcura.com/browse/RTF-536)

## [0.12.0] - 2020-07-06

### Added

- Workspace Strategies
  - CreateWithDocs - [RTF-335](https://jira.kcura.com/browse/RTF-335)
- KeywordSearch Strategies
  - Get - [RTF-514](https://jira.kcura.com/browse/RTF-514)
  - Create - [RTF-515](https://jira.kcura.com/browse/RTF-515)
  - Delete - [RTF-516](https://jira.kcura.com/browse/RTF-516)
  - Update - [RTF-517](https://jira.kcura.com/browse/RTF-517)

### Fixed

- Declare dependencies targeting a specific build number. [RTF-499](https://jira.kcura.com/browse/RTF-499)
- Use passed in WorkspaceAdminGroup when requesting workspace creation. [RTF-519](https://jira.kcura.com/browse/RTF-519)

## [0.11.0] - 2020-06-22

### Added

- Document Strategies
  - GetAll - [RTF-461](https://jira.kcura.com/browse/RTF-461)
  - Import - [RTF-456](https://jira.kcura.com/browse/RTF-456)

### Fixed

- Temporary fix for `Yes/No`, `Fixed-Length Text`, and `ObjectType` field mapping for IObjectService requests. [RTF-486](https://jira.kcura.com/browse/RTF-486)

## [0.10.0] - 2020-06-08

### Added

- Lanceleaf support - [RTF-430](https://jira.kcura.com/browse/RTF-430)
- User Strategies
  - RemoveFromGroup - [RTF-448](https://jira.kcura.com/browse/RTF-448)

### Fixed

- Using PerformRelativityVersionCheck will no longer cause an issue with lazy loading the RelativityFacade. - [RTF-447](https://jira.kcura.com/browse/RTF-447)
- Multiversion support. - [RTF-433](https://jira.kcura.com/browse/RTF-433)

## [0.9.0] - 2020-05-25

### Added

- Choice Strategies
  - Get - [RTF-419](https://jira.kcura.com/browse/RTF-419)
  - Create - [RTF-418](https://jira.kcura.com/browse/RTF-418)
  - Update - [RTF-420](https://jira.kcura.com/browse/RTF-420)
  - Require - [RTF-422](https://jira.kcura.com/browse/RTF-422)
  - Delete - [RTF-421](https://jira.kcura.com/browse/RTF-421)
- MOTD Strategies - [RTF-399](https://jira.kcura.com/browse/RTF-399)
  - Get
  - Update
  - Dismiss
  - HadDismissed
