# Add and Remove

To enable the user to add and remove to-do items, we need a few more declarations in the ViewModel.

---

## Your Task

- Add a `string` property named `NewItemText`
- Add two methods:
    - `public Task AddItem();`
      - It has to asynchronously call `ToDoFacade`'s `AddItem` with `NewItemText` as argument.
    - `public Task RemoveItem(int id);`
      - It has to wrap around `ToDoFacade`'s `RemoveItem`.

[CodeTask](/resources/collections/viewmodel_operations.csharp.csx)