---
Title: Profile
CodeTask: /resources/020_profile_detail/10_profile.csharp.csx
---

# Profile

Hello and, again, welcome to DotVVM Academy! This time, we're going to build a simple Profile Detail Form that allows the user to change its last name. We'll also look closely at the concept of `DataContext`.

In the editor you see a `ProfileDetailViewModel`. It's grown a bit too complex. Move Profile related properties to the empty `Profile` class (all of them except for `NewLastName`). Also, add a `Profile` property to the ViewModel; don't forget to initialize it; and fix the `Rename` method.

> Don't worry about the default values. They're there to simulate database access.