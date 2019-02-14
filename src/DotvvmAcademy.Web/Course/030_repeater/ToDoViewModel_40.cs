using System.Collections.Generic;

namespace DotvvmAcademy.Course.ToDoList
{
    public class ToDoViewModel
    {
        public string NewItem { get; set; }

        public List<string> Items { get; set; }

        public void Add()
        {
            Items.Add(NewItem);
        }

        public void Remove(string item)
        {
            Items.Remove(item);
        }
    }
}