using System;
using System.Collections.Generic;
using System.Text;

namespace DotvvmAcademy.BL.DTO.Components
{
    public class HtmlLiteralComponent : SourceComponent
    {
        public HtmlLiteralComponent(int lessonIndex, string language, int stepIndex) : base(lessonIndex, language, stepIndex)
        {
        }

        public string Source { get; internal set; }
    }
}
