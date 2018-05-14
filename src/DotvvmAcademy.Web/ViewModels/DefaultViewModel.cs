using DotVVM.Framework.ViewModel;
using DotvvmAcademy.BL;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.Web.ViewModels
{
    public class DefaultViewModel : SiteViewModel
    {
        private readonly LessonFacade facade;

        public DefaultViewModel(LessonFacade facade)
        {
            this.facade = facade;
        }

        [Bind(Direction.ServerToClientFirstRequest)]
        public List<LessonDTO> Lessons { get; set; }

        public override async Task Load()
        {
            Lessons = (await facade.GetLessons(Language)).ToList();
        }
    }
}