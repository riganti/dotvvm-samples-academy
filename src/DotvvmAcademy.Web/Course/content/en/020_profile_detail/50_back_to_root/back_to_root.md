---
Title: Back to Root
CodeTask: /resources/020_profile_detail/50_back_to_root.dothtml.csx
---

# Back to Root

But what if we need to reference the ViewModel in a different DataContext? We use the `_root` keyword. 

For instance, to reference ViewModel's `NewLastName` property in any DataContext you'd use: `{value: _root.NewLastName}`.

- Add a `<dot:TextBox>` to the outer `<div>` element. Value-bind its `Text` property to `NewLastName` on the ViewModel.
- Add a `<dot:Button>` to the outer `<div>` element. Command-bind it to the `Rename` method on the ViewModel.