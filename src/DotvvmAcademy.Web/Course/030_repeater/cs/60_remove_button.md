---
Title: Tlačítko Remove
Moniker: remove-button
CodeTask:
    Path: 60_remove_button.dothtml.csx
    Default: ToDo_30.dothtml
    Correct: ToDo_40.dothtml
---

# Tlačítko Remove

Nakonec potřebujeme pro každý úkol přidat tlačítko "Smazat".

Vzhledem k tomu, že metoda `Remove` není definována ve třídě `ToDoItem`, ale ve ViewModelu, potřebujeme použít speciální proměnnou `_root`. 
Budeme ji ale také potřebovat předat aktuální prvek jako parameter. K tomu slouží speciální proměnná `_this`, která obsahuje aktuální binding context - to je naše položka, kterou chceme smazat.

---

## Tasks

- Přidejte do šablony uvnitř komponenty `Repeater` tlačítko `Button`.
- Po kliknutí na tlačítko zavolejte metodu `Remove` a předejte jí `_this` jako parametr. Nezapomeňte, že metoda je definována ve ViewModelu a je tedy třeba použít `_root`.
