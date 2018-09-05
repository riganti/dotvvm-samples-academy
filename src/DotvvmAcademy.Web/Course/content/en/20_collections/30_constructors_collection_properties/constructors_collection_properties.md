# Constructors and Collection Properties

## Dependency Injection

ViewModels gain access to other services using [Dependency Injection][di]. In practice you have to do these three things to consume a service:

1. Register your services (e.g. facades) in an [IoC Container][di]
2. In your ViewModel create a constructor taht takes the services, it needs, as parameters
3. Use the services

For the sake of simplicity, let's pretend out `ToDoFacade` is already registered.

## Collections in ViewModel

DotVVM needs to be able to deserialize the ViewModel from a piece of [JSON]. Your collection properties must therefore be of constructible types like [`List<string>`][list] or `string[]`. `IEnumerable<string>` cannot be deserialized and will generate errors.

---

## Your Task

Edit `ToDoViewModel` to contain:

- A public constructor that initializes the `facade` field
- A public property `Items` of type [`List<ToDoItem>`][list]
  - With a public getter and a setter

[di]: https://www.dotvvm.com/docs/tutorials/advanced-ioc-di-container
[json]: https://json.org/
[list]: https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1

[CodeTask](/resources/collections/viewmodel_stub.csharp.csx)
