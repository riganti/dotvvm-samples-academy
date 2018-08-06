# A Form

Finally, we add some item-adding and item-removing controls.

---

- Add a `<dot:Button>` to the Repeater's item template. It must call the `RemoveItem` command with the current item's
`Id` as argument. As for the button's `Text`, pick whatever you like
- Add a `<dot:TextBox>` below the repeater and bind it to the `NewItemText` property
- Add a `<dot:Button>` next to the button. It must call the `AddItem` command