Relativity.Testing.Framework.Api is an abstraction that handles making API requests to Relativity instances.
You can use this package to do CRUD operations in Relativity, and supports most operations that are available through the public REST APIs.

It depends on the [Relativity.Testing.Framework](https://probable-happiness-2926a3e8.pages.github.io) package for configuration management and DTOs to model the objects.

Relativity.Testing.Framework.Api is version aware, so it handles the differences in the API requests for you behind the scenes.
This allows you to define your test code once, and run it on any Relativity environment.