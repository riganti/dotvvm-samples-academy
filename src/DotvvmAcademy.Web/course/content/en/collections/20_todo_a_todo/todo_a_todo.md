# TODO: A To-Do List

This time around we'll make a To-Do list and learn how to use the [`<dot:Repeater>`][repeater] control in the process.

Our application already has a business layer, which will serve as the
model for our DotVVM application.

## ToDoFacade

```csharp
using System;
using System.Threading.Tasks;

namespace DotvvmAcademy.Course
{
    public class ToDoFacade
    {
        public Task<ToDoItemDTO> GetToDoItem(int id);

        public Task<IReadOnlyList<ToDoItemDTO>> GetToDoItems();

        public Task AddItem(ToDoItemDTO dto);

        public Task RemoveItem(ToDoItemDTO dto)
    }
}
```

[The Facade pattern][facade], in our case, works as an interface between our DotVVM application and whatever handles 
the storage of the to-do items. That could be a database, but it might as well be a printer, a scanner and some
OCR software. The user of the facade doesn't need to know.

## ToDoItemDTO

```csharp
using System;

namespace DotvvmAcademy.Course
{
   public class ToDoItemDTO
   {
       public int Id { get; }

       public int Text { get; }
   }
}
```

What you see in the snippet above is a [Data Transfer Object][dto]. Essentially, it represents a piece of data
from _somewhere_ and is virtually always safe to use in ViewModels as it usually is JSON-serializable.

---

To summarize, throughout this lesson we'll make a ViewModel that:

- Loads items from `ToDoFacade`
- Adds new items from the user
- Removes items if the user desires so

...and a View that:

- Uses the [`<dot:Repeater>`][repeater] control to display the items
- Has a mechanism for adding and removing items


[facade]: https://en.wikipedia.org/wiki/Facade_pattern
[dto]: https://en.wikipedia.org/wiki/Data_transfer_object
[repeater]: https://www.dotvvm.com/docs/controls/builtin/Repeater