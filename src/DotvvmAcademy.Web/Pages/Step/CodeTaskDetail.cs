using DotVVM.Framework.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace DotvvmAcademy.Web.Pages.Step
{
    public class CodeTaskDetail
    {
        public string Code { get; set; }

        [Bind(Direction.None)]
        public string CodeLanguage { get; set; }

        [Bind(Direction.ServerToClient)]
        public bool IsCodeCorrect { get; set; }

        [Bind(Direction.ServerToClient)]
        public List<MonacoMarker> Markers { get; set; } = new List<MonacoMarker>();

        [Bind(Direction.None)]
        public string SourcePath { get; set; }
    }
}