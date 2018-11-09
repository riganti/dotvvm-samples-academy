---
Title: DataContext
CodeTask: /resources/020_profile_detail/20_datacontext.dothtml.csx
---

# DataContext

Every Control has a `DataContext` property. DataContext is the object you bind your bindings to. Child controls inherit DataContext from their Parent. By default it's set to the ViewModel itself. Sometimes it's convenient, however, to change it.

Value-bind the DataContext of the outermost `<div>` element to the `Profile` property. All elements inside the `<div>` now have their DataContext bound to `Profile`; you have to therefore fix the Value Bindings. Remove the `Profile.` prefix.
