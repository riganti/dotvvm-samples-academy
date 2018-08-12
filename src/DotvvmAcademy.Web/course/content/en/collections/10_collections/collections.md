﻿# Collections

In this lesson we're gonna build a to-do list, because what is a to-do list, but a collection of to-do items.

You should definitely know a few things before we start:

- [HTML] - write static documents
- [C#] - write classes, properties and methods
- DotVVM - on the level of the [Principles of DotVVM][principles] lesson

---

At the end of this lesson we'll have a ViewModel that:

- Loads items from a pre-made business layer
- Adds new items from the user
- Removes items if the user desires so

...and a View that:

- Uses the [`<dot:Repeater>`][repeater] control to display the items
- Lets the user add and remove items

[principles]: ../../principles/10_principles/principles.md
[html]: https://developer.mozilla.org/en-US/docs/Learn/Getting_started_with_the_web/HTML_basics
[C#]: https://docs.microsoft.com/en-us/dotnet/csharp/quick-starts/
[repeater]: https://www.dotvvm.com/docs/controls/builtin/Repeater