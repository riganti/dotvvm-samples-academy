using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;
using DotvvmAcademy.Controls;

namespace DotvvmAcademy.ViewModels
{
    public class MonacoTestViewModel : DotvvmViewModelBase
    {
        public string Code { get; set; } =
@"NopeNopeNopeNopeNope
YeahYeahYeahYeahYeah
NopeNopeNopeNopeNope";
        public List<MonacoMarker> Markers { get; set; } = new List<MonacoMarker>
        {
            new MonacoMarker
            {
                Code = "CS1234",
                StartColumn = 8,
                EndColumn = 11,
                StartLineNumber = 1,
                EndLineNumber = 2,
                Severity = MonacoSeverity.Info,
                Message = "Test info message"
            }
        };
    }
}
