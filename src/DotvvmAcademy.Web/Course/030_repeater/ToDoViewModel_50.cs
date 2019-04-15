using System.Collections.Generic;

namespace DotvvmAcademy.Course.ToDoList
{
    public class ToDoItem
    {
        public string Text { get; set; }
    }

    public class ToDoViewModel
    {
        public List<ToDoItem> Items { get; set; } = new List<ToDoItem>();

        public string NewItem { get; set; }

        public void Add()
        {
            Items.Add(new ToDoItem {Text = NewItem});
        }

        public void Remove(ToDoItem item)
        {

        }
    }
}
