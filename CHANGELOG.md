# Changelog

## [v1.0.0-beta01]

Version 0.x of this library used the helper library [CodeGeneration.Roslyn](https://github.com/AArnott/CodeGeneration.Roslyn) by [AArnott](https://github.com/AArnott), for build-time source generation. In version 1.x this approach has been completely replaced in favour of source generators, as these are explicitly supported in .NET 5+. As part of this change, there were a number of additional features added and breaking changes made.

Breaking Changes

* `StronglyTypedIds` namespace is required. In version 0.x of the library, the `[StronglyTypedId]` attribute was in the global namespace. In version 1.x, the attribute is in the `StronglyTypedIds` namespace, so you must add `namespace StronglyTypedIds;`.
* The properties exposed by `StronglyTypedIds` have changed: there is no longer a `generateJsonConverter` property. Instead, this is infered based on the `StronglyTypedIdConverters` flags provided.
* The `String` backing typed ID will throw if you call the constructor with a `null` value

New Features

* The attributes can now auto-generate additional converter types such as EF Core `ValueConverter` and Dapper `TypeHandler`, as described in [my blog posts](https://andrewlock.net/series/using-strongly-typed-entity-ids-to-avoid-primitive-obsession/). These are optional flags on the `converters` property.
* Made interface implementations (`IEquatable<T>` and `IComparable<T>` currently) optional. This is to potentially support additional interfaces in future versions.
* Added a `NullableString` backing type. Due to the behaviour of `struct`s in c#, the `String` backing type ID _may_ still be null, but you can't explicitly call the constructor with a null value. In contrast, you can do this with the `NullableString` backing type.
* Added a `[StronglyTypedIdDefaults]` attribute to set default values for all `[StronglyTypedId]` attributes in your project. This is useful if you want to customise all the attributes, for example, if you want to generate additional converters by default. You can still override all the properties of a `[StronglyTypedId]` instance.

Bug Fixes

* Some converters had incorrect implementations, such as in ([#26](https://github.com/andrewlock/StronglyTypedId/issues/24)). These have been addressed in version 1.x.
* Better null handling has been added for the `String` backing type, handling issues such as [#32](https://github.com/andrewlock/StronglyTypedId/issues/32).
* The code is marked as auto generated, to avoid errors such as #CS1591 as described in [#27](https://github.com/andrewlock/StronglyTypedId/issues/27)

## [v0.2.1]

Features:

* Fix Package description

## [v0.2.0]

Features:

* Added support for .NET Core 3.1, and converted to using CodeGeneration.Roslyn.Tool instead of dotnet-codegen (thanks [Bartłomiej Oryszak
](https://github.com/vebbo2))
* Added support for generating System.Text.Json `JsonConverters` (thanks [Bartłomiej Oryszak
](https://github.com/vebbo2))
* Added support for long backing type (thanks [Bartłomiej Oryszak
](https://github.com/vebbo2))

## [v0.1.0]

Initial release

[v0.2.0]: https://github.com/andrewlock/StronglyTypedId/compare/v0.1.0...v0.2.0