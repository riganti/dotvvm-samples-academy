using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotvvmAcademy.Course.ToDo
{
    public class ToDoFacade
    {
        private static List<ToDoItem> storage = new List<ToDoItem>
        {
            new ToDoItem(1, "Wash dishes"),
            new ToDoItem(2, "Buy eggs"),
            new ToDoItem(3, "Take over the World")
        };

        private static int currentIndex = 4;

        public Task<IReadOnlyList<ToDoItem>> GetToDoItems()
        {
            return Task.FromResult<IReadOnlyList<ToDoItem>>(storage);
        }

        public Task AddItem(string text)
        {
            storage.Add(new ToDoItem(currentIndex, text));
            currentIndex++;
            return Task.CompletedTask;
        }

        public Task RemoveItem(int id)
        {
            var item = storage.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                storage.Remove(item);
            }

            return Task.CompletedTask;
        }
    }
}