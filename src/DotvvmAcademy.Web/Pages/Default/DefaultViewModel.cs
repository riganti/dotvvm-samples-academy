using DotVVM.Framework.ViewModel;
using DotvvmAcademy.CourseFormat;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmAcademy.Web.Pages.Default
{
    public class DefaultViewModel : SiteViewModel
    {
        private readonly CourseWorkspace workspace;

        public DefaultViewModel(CourseWorkspace workspace)
        {
            this.workspace = workspace;
        }

        [Bind(Direction.ServerToClientFirstRequest)]
        public List<Lesson> Lessons { get; set; }

        public override async Task Load()
        {
            var variant = await workspace.LoadVariant(LanguageMoniker);
            var lessonTasks = variant.Lessons.Select(l => workspace.LoadLesson(LanguageMoniker, l));
            var lessons = await Task.WhenAll(lessonTasks);
            Lessons = lessons.ToList();
            await base.Load();
        }

        protected override async Task<IEnumerable<string>> GetAvailableLanguageMonikers()
        {
            var root = await workspace.LoadRoot();
            return root.Variants;
        }
    }
}