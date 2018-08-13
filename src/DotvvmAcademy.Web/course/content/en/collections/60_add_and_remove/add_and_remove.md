# Add and Remove

To enable the user to add and remove to-do items, we need add a few more declarations to the ViewModel.

---

## Your Task

- Add a `string` property named `NewItemText`
- Add two methods:
    - `public Task AddItem();`; it has to asynchronously call `facade`'s `AddItem` with `NewItemText` as argument
    - `public Task RemoveItem(int id);`; it has to wrap around `facade`'s `RemoveItem`

[CodeTask](/resources/collections/viewmodel_operations.csharp.csx)