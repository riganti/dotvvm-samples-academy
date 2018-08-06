# Facades and DTOs

The business layer typically consists of a number of [Facades][facade] and [Data Transfer Objects][dto].
Our To-do list already has a business layer, which will serve as the model for our DotVVM application.

## ToDoFacade

```csharp
using System;
using System.Threading.Tasks;

namespace DotvvmAcademy.Course
{
    public class ToDoFacade
    {
        public Task<IReadOnlyList<ToDoItemDTO>> GetToDoItems();

        public Task AddItem(string text);

        public Task RemoveItem(int id)
    }
}
```

`ToDoFacade` is an interface between our DotVVM application and whatever handles 
the storage of the to-do items. That could be a database, but it might as well be a trained monkey.
The caller of the facade's methods doesn't need to know.

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
from _somewhere_ and is virtually always safe to use in ViewModels as it's [JSON]-serializable.


[facade]: https://en.wikipedia.org/wiki/Facade_pattern
[dto]: https://en.wikipedia.org/wiki/Data_transfer_object
[repeater]: https://www.dotvvm.com/docs/controls/builtin/Repeater
[json]: https://json.org/