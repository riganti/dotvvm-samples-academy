using DotVVM.Framework.Compilation;
using DotVVM.Framework.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace DotVVM.Framework.Hosting
{
    public static class DotvvmRequestContextExtensions
    {
        private static int uniqueIdIndex;

        public static DotvvmControl GetDotControl(this IDotvvmRequestContext context, string path)
        {
            var controlBuilderFactory = context.Services.GetService<IControlBuilderFactory>();
            var (_, builder) = controlBuilderFactory.GetControlBuilder(path);
            var control = builder.Value.BuildControl(controlBuilderFactory, context.Services);
            //control.SetValue(Internal.UniqueIDProperty, GetUniqueID());
            return control;
        }

        private static string GetUniqueID()
        {
            var id = "d" + uniqueIdIndex;
            uniqueIdIndex++;
            return id;
        }
    }
}