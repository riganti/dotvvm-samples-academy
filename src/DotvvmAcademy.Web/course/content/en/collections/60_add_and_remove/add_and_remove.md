# Add and Remove

To enable the user to add and remove to-do items, we need a few more ViewModel members.

---

- Add a `string` property named `NewItemText`

- Add two methods:
    - `public Task AddItem();` has to asynchronously call `facade`'s `AddItem` with `NewItemText` as argument
    - `public Task RemoveItem(int id);` has to wrap `facade`'s `RemoveItem`