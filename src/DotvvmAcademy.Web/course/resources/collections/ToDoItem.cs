using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotvvmAcademy.Course
{
    public class ToDoItem
    {
        public ToDoItem(int id, string text)
        {
            Id = id;
            Text = text;
        }

        public int Id { get; }

        public string Text { get; }
    }
}