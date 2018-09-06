---
Title: A Form
CodeTask: /resources/collections/view_form.dothtml.csx
---

# A Form

Finally, we add some item-adding and item-removing controls.

---

## Your Task

- Add a `<dot:Button>` to the Repeater's item template
  - It must call the `RemoveItem` command with the current item's `Id` as argument
    - Use the `_root` binding variable to access the ViewModel from within the Repeater
  - Pick whatever you deem appropriate as the value of the `Text` property
- Add a `<dot:TextBox>` below the `Repeater` and bind it to the `NewItemText` property.
- Add a `<dot:Button>` next to the `TextBox`. It must call the `AddItem` command.
