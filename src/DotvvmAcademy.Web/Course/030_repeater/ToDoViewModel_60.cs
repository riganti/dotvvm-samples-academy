using System.Collections.Generic;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;

namespace DotvvmAcademy.Course.ToDoList
{
    public class ToDoViewModel : DotvvmViewModelBase
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

        public override Task Load()
        {
            if (Items == null)
            {
                Items = new List<string>();
            }
            return base.Load();
        }
    }
}