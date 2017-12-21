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
@"public cass Test
{
}";
        public List<MonacoMarker> Markers { get; set; } = new List<MonacoMarker>
        {
            new MonacoMarker
            {
                Code = "CS1234",
                StartColumn = 7,
                EndColumn = 10,
                Severity = MonacoSeverity.Error,
                Message = "Test Error message"
            }
        };
    }
}
