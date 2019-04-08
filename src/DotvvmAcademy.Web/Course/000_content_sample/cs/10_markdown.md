---
Title: Markdown
CodeTask: 
    Path: 10_markdown.csharp.csx
---

# Markdown

Vítejte! Toto je Příkladová lekce určená pro testovací účely. Toto je odstavec a měl by tak být formátovat. Pokud vypadá divně nebo není dobře čitelný, něco je tu zatraceně špatně. V odstavci se mohou vyskytovat reference na symboly např. `Property` nebo `<dot:Button>`. Některá slova mohou být napsána __tučně__ nebo _kurzívou_.

```csharp
using System;

public class Test
{
    public const string Constant = "Tohle je C# code snippet. Syntax highlighter by měl zdůraznit jeho strukturu.";
}
```

```dothtml
@viewModel A.ViewModel

<html>
    <body>
        <p>Tohle je dothtml snippet. Měl by být barevný alespoň tak jako html.</p>
        <dot:Button Text="{value: Property}"
                    Click="{command: Work()}"/>
    </body>
</html>
```

---

## Úkoly

- Nad nadpisem 'Úkoly' by měla být dělicí čára.
- Tohle je seznam úkolů.
- Uživatel by měl tyto úkoly splnit, aby postoupil do dalšího kroku.
- Splněnost úkolů se kontroluje na serveru po stisknutí tlačítka 'Následující'.

> Poznámka: Na konci kroku mohou být poznámky.