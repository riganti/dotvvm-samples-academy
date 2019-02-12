using System.Collections.Generic;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;

namespace DotvvmAcademy.Course.ToDoList
{
    public class ToDoViewModel : DotvvmViewModelBase
    {
        public List<string> Items { get; set; }

        public string NewItem { get; set; }

        public override Task Init()
        {
            if (!Context.IsPostBack)
            {
                Items = new List<string>();
            }

            return base.Init();
        }

        public void Add()
        {
            Items.Add(NewItem);
        }

        public void Remove(string item) {
            Items.Remove(item);
        }
    }
}