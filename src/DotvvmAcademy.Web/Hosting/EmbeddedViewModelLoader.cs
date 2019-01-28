using DotVVM.Framework.Controls.Infrastructure;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ViewModel.Serialization;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DotvvmAcademy.Web.Hosting
{
    public class EmbeddedViewModelLoader : IViewModelLoader
    {
        public void DisposeViewModel(object instance)
        {
            if (instance is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        public object InitializeViewModel(IDotvvmRequestContext context, DotvvmView view)
        {
            var constructors = view.ViewModelType.GetConstructors();
            if (constructors.Length > 1)
            {
                throw new InvalidOperationException("An EmbeddedView ViewModel must have at most 1 constructor.");
            }
            var constructor = constructors[0];
            var parameters = constructor.GetParameters();
            var arguments = new object[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];
                var argument = context.Services.GetService(parameter.ParameterType);
                if (argument == null && parameter.ParameterType.Assembly == view.ViewModelType.Assembly)
                {
                    argument = ActivatorUtilities.CreateInstance(context.Services, parameter.ParameterType);
                }
                arguments[i] = argument;
            }
            return constructor.Invoke(arguments);
        }
    }
}