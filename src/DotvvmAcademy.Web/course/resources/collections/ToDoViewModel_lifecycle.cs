using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;

namespace DotvvmAcademy.Course.ToDo
{
    public class ToDoViewModel : DotvvmViewModelBase
    {
        private readonly ToDoFacade facade;

        public ToDoViewModel(ToDoFacade facade)
        {
            this.facade = facade;
        }

        public List<ToDoItem> Items { get; set; }

        public override async Task PreRender()
        {
            var items = await facade.GetToDoItems();
            Items = items.ToList();
        }
    }
}
