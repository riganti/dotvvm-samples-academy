# Constructors and List Properties

## Dependency Injection

ViewModels gain access to the business layer using [Dependency Injection][di]. In practice you have to do three things:

1. Register your services (e.g. facades) in an [IoC Container][di]
2. In your ViewModel create a constructor which takes the services it needs as parameters
3. Use the services


## Collections in ViewModel

DotVVM needs to be able to deserialize the ViewModel from a piece of [JSON]. Your collection properties must therefore
have concrete types like `List<string>` or `string[]`. `IEnumerable<string>` cannot be deserialized and will
generate errors.

---

Create a new class in the `DotvvmAcademy.Course` namespace named `ToDoViewModel` that fulfils these requirements:

- Has a private readonly field called `facade` of type `ToDoFacade`
- Has a public constructor which takes a `ToDoFacade` as its single parameter and assigns it to the `facade` field
- Has a public property `Items` of type `List<ToDoItem>`

[di]: https://www.dotvvm.com/docs/tutorials/advanced-ioc-di-container
[json]: https://json.org/

[CodeTask](/resources/collections/viewmodel_stub.csharp.csx)
