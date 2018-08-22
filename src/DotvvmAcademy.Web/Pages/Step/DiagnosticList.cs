using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using System.Collections.Generic;

namespace DotvvmAcademy.Web.Pages.Step
{
    public class DiagnosticList : DotvvmMarkupControl
    {
        public static readonly DotvvmProperty DiagnosticsProperty
            = DotvvmProperty.Register<IEnumerable<MonacoMarker>, DiagnosticList>(c => c.Diagnostics, null);

        [MarkupOptions(Required = true, AllowBinding = true, AllowHardCodedValue = false)]
        public IEnumerable<MonacoMarker> Diagnostics
        {
            get { return (IEnumerable<MonacoMarker>)GetValue(DiagnosticsProperty); }
            set { SetValue(DiagnosticsProperty, value); }
        }
    }
}