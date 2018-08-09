# The View

DotVVM uses a special html-like syntax, [Dothtml], to define Views. It's essentially html with features that
make it easier to use the MVVM pattern.

## Directives

Directives are one of those features. They contain information about the View such as: what ViewModel to use, what
[MasterPage] to consider a template, what namespaces to import, and so on. Located at the beginning of the file,
they look like this:

```dothtml
    @directiveNameOne argument
    @directiveNameTwo Namespace.Sample.Type
```

The `@viewModel` directive is mandatory; it tells DotVVM what ViewModel to instantiate.

---

Create a dothtml stub that fulfils the following requirements:

- Starts with a `@viewModel` directive where our ViewModel, `DotvvmAcademy.Course.CalculatorViewModel`,
is the argument.
- Has the `<!DOCTYPE html>` declaration
- Contains the `<body>` element inside the root `<html>` element

[masterpage]: https://www.dotvvm.com/docs/tutorials/basics-master-pages
[dothtml]: https://www.dotvvm.com/docs/tutorials/basics-first-page
[extension]: https://www.dotvvm.com/landing/dotvvm-for-visual-studio-extension

[CodeTask](/resources/principles/view_stub.dothtml.csx)