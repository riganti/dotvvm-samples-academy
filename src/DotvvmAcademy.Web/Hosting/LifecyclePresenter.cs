using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace DotvvmAcademy.Web.Hosting
{
    public class LifecyclePresenter : IDotvvmPresenter
    {
        private readonly IServiceProvider provider;
        private readonly List<Type> registeredStategies = new List<Type>();

        public LifecyclePresenter(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public async Task ProcessRequest(IDotvvmRequestContext context)
        {
            EnsureRequestMethod(context);
            ILifecycleStrategy strategy = null;
            foreach (var registeredStrategy in registeredStategies)
            {
                var instance = (ILifecycleStrategy)provider.GetRequiredService(registeredStrategy);
                if (instance.CanProcess())
                {
                    strategy = instance;
                    break;
                }
            }
            if (strategy == null)
            {
                throw new NotSupportedException($"Request cannot be processed by any registered {nameof(ILifecycleStrategy)}");
            }

            try
            {
                await strategy.PreInit();
                await strategy.Init();
                await strategy.Load();
                await strategy.PreRender();
                await strategy.PreRenderCompleted();
                await strategy.Render();
            }
            catch(Exception exception)
            {
                await strategy.OnException(exception);
            }
            finally
            {
                if (strategy is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
        }

        public LifecyclePresenter RegisterStrategy<TStrategy>()
            where TStrategy : ILifecycleStrategy
        {
            registeredStategies.Add(typeof(TStrategy));
            return this;
        }

        private void EnsureRequestMethod(IDotvvmRequestContext context)
        {
            if (context.HttpContext.Request.Method != "GET" && context.HttpContext.Request.Method != "POST")
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                throw new DotvvmHttpException("Only GET and POST methods are supported!");
            }
        }
    }
}