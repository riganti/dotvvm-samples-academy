# 

These facts are somewhat important:

- ViewModel has to inherit from `DotvvmViewModelBase` in order to overload the
lifecycle methods (`Init()`, `Load()`, and `PreRender()`).
- ViewModels can have constructors with parameters as long as DotVVM is able to resolve them using [Dependency Injection][di].

You don't necessarily need to know how Dependency Injection works; just that the parameters get provided automatically.

Now it's time for some actual coding.

---

Create a `ToDoViewModel` class in the `DotvvmAcademy.Course` namespace that:

- inherits from `DotvvmViewModelBase`
- has a private readonly field of type `ToDoFacade` called `facade`
- has a constructor with `ToDoFacade` as its single argument that gets assigned to the `facade` field

[di]: https://www.dotvvm.com/docs/tutorials/advanced-ioc-di-container

