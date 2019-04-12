# Remove Button

Finally, we need a "Remove" button for every item.

Since the `Remove` method doesn't belong to `ToDoItem`, we need to access the `_root` Binding Context. We'll also need to pass the current item as a parameter to the command. We can use `_this` to reference the current item.

---

## Tasks

- Add a `Button` to the `Repeater`'s item template.
- This button needs to call `Remove` on `_root` with `_this` as the parameter.
