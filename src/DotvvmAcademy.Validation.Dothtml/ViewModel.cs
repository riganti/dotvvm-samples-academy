﻿using DotVVM.Framework.Configuration;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Security;
using DotVVM.Framework.ViewModel;
using DotvvmAcademy.Validation.CSharp;
using Microsoft.Extensions.DependencyInjection;

namespace DotvvmAcademy.Validation.Dothtml
{
    public static class ViewModel
    {
        public static dynamic Instantiate(CSharpDynamicContext context, string typeName, params object[] arguments)
        {
            var instance = context.Instantiate(typeName, arguments);
            if (!(instance is IDotvvmViewModel viewModel))
            {
                return instance;
            }
            var configuration = DotvvmConfiguration.CreateDefault(c => c.AddSingleton<IViewModelProtector, ValidationViewModelProtector>());
            var httpContext = new ValidationHttpContext();
            var request = new ValidationHttpRequest(httpContext);
            var response = new ValidationHttpResponse(httpContext, new ValidationHeaderCollection());
            httpContext.Request = request;
            httpContext.Response = response;
            var requestContext = new DotvvmRequestContext(httpContext, configuration, null);
            viewModel.Context = requestContext;
            // TODO: ValidationTarget no longer has public setter, is it a problem?
            //requestContext.ModelState.ValidationTarget = viewModel;
            return viewModel;
        }
    }
}
