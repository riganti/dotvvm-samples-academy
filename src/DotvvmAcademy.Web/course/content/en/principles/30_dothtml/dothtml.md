# Dothtml

DotVVM uses a special html-like syntax, [Dothtml], to define Views. It's html with features that make it easier to use the [MVVM] pattern.

## Directives

Directives contain data about the View such as:

- what ViewModel to use
- what [MasterPage] to consider a template
- what namespaces to import
- etc.

They are always at the beginning of the file and they look like this:

```dothtml
    @directiveOne argument
    @directiveTwo Namespace.Sample.Type
```

The `@viewModel` directive is mandatory; it tells DotVVM what ViewModel to instantiate.

---

## Your Task

Create a dothtml stub that fulfils the following requirements:

- Starts with a `@viewModel` directive where our ViewModel, `DotvvmAcademy.Course.Calculator.CalculatorViewModel`, is the argument
- Has the [`<!DOCTYPE html>`][doctype] declaration
- Contains the [`<html>`][html] element
- Contains the [`<head>`][head] element with [`<meta charset="utf-8">`][meta]
- Contains the [`<body>`][body] element 

[dothtml]: https://www.dotvvm.com/docs/tutorials/basics-first-page
[masterpage]: https://www.dotvvm.com/docs/tutorials/basics-master-pages
[mvvm]: https://en.wikipedia.org/wiki/Model%E2%80%93view%E2%80%93viewmodel
[extension]: https://www.dotvvm.com/landing/dotvvm-for-visual-studio-extension
[html]: https://developer.mozilla.org/en-US/docs/Web/HTML/Element/html
[body]: https://developer.mozilla.org/en-US/docs/Web/HTML/Element/body
[head]: https://developer.mozilla.org/en-US/docs/Web/HTML/Element/head
[meta]: https://developer.mozilla.org/en-US/docs/Web/HTML/Element/meta
[doctype]: https://developer.mozilla.org/en-US/docs/Glossary/Doctype

[CodeTask](/resources/principles/view_stub.dothtml.csx)