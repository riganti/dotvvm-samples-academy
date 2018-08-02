# The View

As said before, DotVVM uses a special html-like syntax, called [dothtml]. It's essentially the same as html except for a
few features that make it easier to use the MVVM pattern.

One of which is the concept of directives. Directives are always at the very beginning of the file and are denoted
by the `@` prefix as in this example:

```dothtml
    @sampleDirective argument
    @sampleTypeDirective Namespace.Sample.Type
```

---

Create a dothtml View stub.

> Usually you don't have to do that manually; our [Visual Studio Extension][extension] does it for you, 
but it's important to know how to do it yourself; for example in case you don't have access to an instance of VS.

- Start with the `@viewModel` directive. It tells the framework that in order to use this page, it has to create an
instance of a ViewModel class you denote as the argument, `DotvvmAcademy.Course.CalculatorViewModel` in our case.
- Follow with the standard `<!DOCTYPE html>` declaration.
- And finally add the `<html>` element and the `<body>` element as its child.

[dothtml]: https://www.dotvvm.com/docs/tutorials/basics-first-page
[extension]: https://www.dotvvm.com/landing/dotvvm-for-visual-studio-extension

[CodeTask]("/resources/principles/view_stub.dothtml.csx")