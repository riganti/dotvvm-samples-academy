---
Title: Null DataContext
CodeTask: 30_null_datacontext.csharp.csx
---

# Null DataContext

We didn't initialize the `Profile` property and so it's `null`. Because of that, the entire `<div>` and its contents won't render. We can use this feature to implement a hiding functionality.

---

## Tasks

- In the `Create` method initialize the `Profile` property.
- In the `Delete` method set the `Profile` property to `null`.