# Constructors and List Properties

## Dependency Injection

ViewModels gain access to other services using [Dependency Injection][di]. In practice you have to do these three things to consume a service:

1. Register your services (e.g. facades) in an [IoC Container][di]
2. In your ViewModel create a constructor which takes the services it needs as parameters
3. Use the services

For the sake of simplicity, let's pretend out `ToDoFacade` is already registered.

## Collections in ViewModel

DotVVM needs to be able to deserialize the ViewModel from a piece of [JSON]. Your collection properties must therefore have constructible types like [`List<string>`][list] or `string[]`. `IEnumerable<string>` cannot be deserialized and will generate errors.

---

## Your Task

Create a new class in the `DotvvmAcademy.Course.ToDo` namespace named `ToDoViewModel` that fulfils these requirements:

- Has a private readonly field called `facade` of type `ToDoFacade`
- Has a public constructor which takes a `ToDoFacade` as its single parameter and assigns it to the `facade` field
- Has a public property `Items` of type [`List<ToDoItem>`][list]

[di]: https://www.dotvvm.com/docs/tutorials/advanced-ioc-di-container
[json]: https://json.org/
[list]: https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1

[CodeTask](/resources/collections/viewmodel_stub.csharp.csx)
