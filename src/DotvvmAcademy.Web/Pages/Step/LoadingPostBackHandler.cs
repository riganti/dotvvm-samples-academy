using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using System.Collections.Generic;

namespace DotvvmAcademy.Web.Pages.Step
{
    public class LoadingPostBackHandler : PostBackHandler
    {
        public static readonly DotvvmProperty AddedClassesProperty
            = DotvvmProperty.Register<string, LoadingPostBackHandler>(c => c.AddedClasses, null);

        public static readonly DotvvmProperty RemovedClassesProperty
            = DotvvmProperty.Register<string, LoadingPostBackHandler>(c => c.RemovedClasses, null);

        public string AddedClasses
        {
            get { return (string)GetValue(AddedClassesProperty); }
            set { SetValue(AddedClassesProperty, value); }
        }

        public string RemovedClasses
        {
            get { return (string)GetValue(RemovedClassesProperty); }
            set { SetValue(RemovedClassesProperty, value); }
        }

        protected override string ClientHandlerName { get; } = "academy-loading";

        protected override Dictionary<string, object> GetHandlerOptions()
        {
            return new Dictionary<string, object>
            {
                ["added-classes"] = AddedClasses,
                ["removed-classes"] = RemovedClasses
            };
        }
    }
}