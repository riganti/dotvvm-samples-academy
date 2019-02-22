---
Title: Markdown
CodeTask: 
    Path: 10_markdown.csharp.csx
    Default: Sample_10.cs
    Correct: Sample_20.cs
---

# Markdown

Welcome to the Content Sample, a development lesson for testing purposes. This is a paragraph and should be formatted as such. If it looks weird or unreadable, something is seriously wrong. There may be symbol references in paragraphs like `Property` or `<dot:Button>`. Some words can also be in __bold__ or _italics_.

```csharp
using System;

public class Test
{
    public const string Constant = "This is a C# code snippet. Its syntax should be highlighted.";
}
```

```dothtml
@viewModel A.ViewModel

<html>
    <body>
        <p>This is a dothtml snippet. It's syntax should be highlighted at least as html.</p>
        <dot:Button Text="{value: Property}"
                    Click="{command: Work()}"/>
    </body>
</html>
```

---

## Tasks

- There should be a divider above. If there isn't one, something is styled wrong.
- This is a To-Do list.
- A user has to fulfill these tasks in order to continue.
- The tasks should be validated by the server upon clicking the 'Next' button.
- Create a public property of type `int` called `Property`.
- Create a public method called `Method` that returns `42` as type `int`.

> Note: There might be some notes at the end of a step.