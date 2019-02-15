---
Title: Null DataContext
CodeTask: 30_null_datacontext.csharp.csx
---

# Null DataContext

We didn't initialize the `Profile` property and so it's `null`. The entire `<div>` and its contents won't render. We can use this feature to hide parts of the page whenever we want.

---

## Tasks

- In the `Create` method initialize the `Profile` property.
- In the `Delete` method set the `Profile` property to `null`.