# Architecture

This document describes the components that are provided by the Relativity.Testing.Framework.Api package.
This is a good first place to look to get an understanding of the logical groupings of this codebase.

## Table of Contents

- [Architecture](#architecture)
  - [Table of Contents](#table-of-contents)
  - [Code Map](#code-map)
    - [Inversion of Control](#inversion-of-control)
      - [ApiComponent](#apicomponent)
    - [Making API Requests](#making-api-requests)
      - [HttpService](#httpservice)
        - [RestService](#restservice)
      - [Services](#services)
      - [Strategies](#strategies)
      - [Object Manager](#object-manager)
        - [ObjectManagement](#objectmanagement)
        - [Querying](#querying)
      - [Kepler](#kepler)

## Code Map

### Inversion of Control
  
Relativity.Testing.Framework uses [Inversion of Control](https://en.wikipedia.org/wiki/Inversion_of_control) to provide an extensible framework for end users while only requiring them to take on the dependencies that they are actually using.
[Castle.Windsor](http://www.castleproject.org/projects/windsor/) is used to create a container that is then used to register and retrieve components.

#### ApiComponent

The ApiComponent is the main piece of the Relativity.Testing.Framework.Api repository, and implements the IRelativityComponent interface.
As described in [IRelativityComponent](https://github.com/relativitydev/relativity.testing.framework/blob/master/docs/dev/architecture.md#irelativitycomponent), the ApiComponent houses the rest of the functionality described in this document, and registers them with the container.

Additionally, this component will ensure that the SUT is active by pinging the "Relativity.Services.Environmental.IEnvironmentModule/Ping Service/Ping" endpoint.

[source](https://github.com/relativitydev/relativity.testing.framework.api/blob/master/source/Relativity.Testing.Framework.Api/ApiComponent.cs), [docs](https://glowing-spork-1e23a31b.pages.github.io/api/Relativity.Testing.Framework.Api.ApiComponent.html)

### Making API Requests

#### HttpService

The HttpService is responsible for interacting with the HTTP APIs in Relativity.
It is a wrapper around the C# HttpClient, with built in serialization, deserialization, and response checking.

[source](https://github.com/relativitydev/relativity.testing.framework.api/blob/master/source/Relativity.Testing.Framework.Api/Services/HttpService.cs), [docs](https://glowing-spork-1e23a31b.pages.github.io/api/Relativity.Testing.Framework.Api.Services.HttpService.html)

##### RestService

The RestService is a further abstraction built on top of the HttpService, built specifically to work with the Relativity.Rest endpoints.

[source](https://github.com/relativitydev/relativity.testing.framework.api/blob/master/source/Relativity.Testing.Framework.Api/Services/RestService.cs), [docs](https://glowing-spork-1e23a31b.pages.github.io/api/Relativity.Testing.Framework.Api.Services.RestService.html)

#### Services

Relativity.Testing.Framework.Api uses services to group common functionality into logical units.
This should line up with the different [REST service managers for the APIs](https://platform.relativity.com/RelativityOne/Content/Relativity_Platform/Platform_APIs.htm).
Services can also be thought of as a collection of strategies for a specific object type in Relativity.

Since this is the public entry point to using the strategies, this is where the end user documentation should be focused.

[source](https://github.com/relativitydev/relativity.testing.framework.api/tree/master/source/Relativity.Testing.Framework.Api/Services), [docs](https://glowing-spork-1e23a31b.pages.github.io/api/Relativity.Testing.Framework.Api.Services.html)

#### Strategies

Relativity.Testing.Framework.Api defines the actions that can be called into as strategies.
This mostly translates as individual requests to the API endpoints (e.g. [Getting a client](https://platform.relativity.com/RelativityOne/Content/BD_Identity/Client_Manager_service.htm#_Retrieve_a_client)), but can also be more complex actions, like requiring an object (i.e. check to see if it exists, create it if it doesn't or if it does, run an update on it to make sure that it has the correct configuration).

[source](https://github.com/relativitydev/relativity.testing.framework.api/tree/master/source/Relativity.Testing.Framework.Api/Strategies), [docs](https://glowing-spork-1e23a31b.pages.github.io/api/Relativity.Testing.Framework.Api.Strategies.html)

#### Object Manager

Similar to the services and strategies, Relativity.Testing.Framework.Api has an abstraction around the [Object Manager](https://platform.relativity.com/RelativityOne/Content/BD_Object_Manager/Object_Manager_service.htm) APIs.
These use a builder pattern instead of normal strategies, but otherwise function the same by making a request to the desired endpoint.

##### ObjectManagement

This is the service that interacts with the Object Manager APIs.

[source](https://github.com/relativitydev/relativity.testing.framework.api/tree/master/source/Relativity.Testing.Framework.Api/ObjectManagement), [docs](https://glowing-spork-1e23a31b.pages.github.io/api/Relativity.Testing.Framework.Api.ObjectManagement.html)

##### Querying

This code defines the objects used during the queries to Object Manager.

[source](https://github.com/relativitydev/relativity.testing.framework.api/tree/master/source/Relativity.Testing.Framework.Api/Querying), [docs](https://glowing-spork-1e23a31b.pages.github.io/api/Relativity.Testing.Framework.Api.Querying.html)

#### Kepler

The KeplerServiceFactory is used to create a service proxy and communicate with the Kepler services for passed in interfaces.

[source](https://github.com/relativitydev/relativity.testing.framework.api/tree/master/source/Relativity.Testing.Framework.Api/Kepler), [docs](https://glowing-spork-1e23a31b.pages.github.io/api/Relativity.Testing.Framework.Api.Kepler.IKeplerServiceFactory.html)
