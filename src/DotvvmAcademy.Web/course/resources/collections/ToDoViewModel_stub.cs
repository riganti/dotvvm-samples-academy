using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotvvmAcademy.Course
{
    public class ToDoViewModel
    {
        private readonly ToDoFacade facade;

        public ToDoViewModel(ToDoFacade facade)
        {
            this.facade = facade;
        }

        public List<ToDoItem> Items { get; set; }
    }
}
