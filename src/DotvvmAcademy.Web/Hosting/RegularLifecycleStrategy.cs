using DotVVM.Framework.Controls;
using DotVVM.Framework.Controls.Infrastructure;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Runtime;
using DotVVM.Framework.Runtime.Filters;
using DotVVM.Framework.Runtime.Tracing;
using DotVVM.Framework.Security;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.ViewModel.Serialization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DotvvmAcademy.Web.Hosting
{
    public class RegularLifecycleStrategy : ILifecycleStrategy
    {
        public RegularLifecycleStrategy(IDotvvmRequestContext context)
        {
            Context = context;
        }

        protected IDotvvmRequestContext Context { get; }

        protected IEnumerable<IPageActionFilter> PageFilters { get; set; }

        protected IEnumerable<IViewModelActionFilter> ViewModelFilters { get; set; }

        public virtual bool CanProcess()
        {
            return Context.HttpContext.Request.Method == "GET";
        }

        public virtual async Task PreInit()
        {
            Context.View = await GetView();
            Context.View.SetValue(Internal.RequestContextProperty, Context);
            Context.ViewModel = await GetViewModel();
            PageFilters = GetPageFilters();
            ViewModelFilters = GetViewModelFilters();
            await Task.WhenAll(PageFilters.Select(f => f.OnPageInitializedAsync(Context)));
            InvokeLifecycleEvent(LifeCycleEventType.PreInit);
            Log(RequestTracingConstants.ViewInitialized);
        }

        public virtual async Task Init()
        {
            Context.View.DataContext = Context.ViewModel;
            await Task.WhenAll(ViewModelFilters.Select(f => f.OnViewModelCreatedAsync(Context)));
            Log(RequestTracingConstants.ViewModelCreated);
            BindParameters();
            if (Context.ViewModel is IDotvvmViewModel viewModel)
            {
                viewModel.Context = Context;
                ChildViewModelsCache.SetViewModelClientPath(viewModel, ChildViewModelsCache.RootViewModelPath);
                await viewModel.Init();
            }
            InvokeLifecycleEvent(LifeCycleEventType.Init);
            Log(RequestTracingConstants.InitCompleted);
        }

        public virtual async Task Load()
        {
            if (Context.ViewModel is IDotvvmViewModel viewModel)
            {
                await viewModel.Load();
            }
            InvokeLifecycleEvent(LifeCycleEventType.Load);
            Log(RequestTracingConstants.LoadCompleted);
        }

        public virtual async Task PreRender()
        {
            if (Context.ViewModel is IDotvvmViewModel viewModel)
            {
                await viewModel.PreRender();
            }
            InvokeLifecycleEvent(LifeCycleEventType.PreRender);
        }

        public virtual Task PreRenderCompleted()
        {
            InvokeLifecycleEvent(LifeCycleEventType.PreRenderComplete);
            Log(RequestTracingConstants.PreRenderCompleted);
            return Task.CompletedTask;
        }

        public virtual async Task Render()
        {
            if (string.IsNullOrEmpty(Context.CsrfToken))
            {
                var protector = Context.Services.GetRequiredService<ICsrfProtector>();
                Context.CsrfToken = protector.GenerateToken(Context);
            }

            await Task.WhenAll(ViewModelFilters.Select(f => f.OnViewModelSerializingAsync(Context)));
            var serializer = Context.Services.GetRequiredService<IViewModelSerializer>();
            serializer.BuildViewModel(Context);
            var renderer = Context.Services.GetRequiredService<IOutputRenderer>();
            await renderer.WriteHtmlResponse(Context, Context.View);
            await Task.WhenAll(PageFilters.Select(f => f.OnPageRenderedAsync(Context)));
            Log(RequestTracingConstants.OutputRendered);
        }

        public virtual async Task OnException(Exception exception)
        {
            if (!(exception is DotvvmInterruptRequestExecutionException || exception is DotvvmHttpException))
            {
                foreach (var pageFilter in PageFilters)
                {
                    await pageFilter.OnPageExceptionAsync(Context, exception);
                    if (Context.IsPageExceptionHandled)
                    {
                        Context.InterruptRequest();
                    }
                }
            }
            switch (exception)
            {
                case UnauthorizedAccessException unauthorised:
                    Context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;

                case DotvvmControlException control:
                    if (control.FileName != null)
                    {
                        control.FileName = Path.Combine(Context.Configuration.ApplicationPhysicalPath, control.FileName);
                    }

                    throw control;
            }
        }

        protected virtual void BindParameters()
        {
            var binder = Context.Services.GetRequiredService<IViewModelParameterBinder>();
            binder.BindParameters(Context, Context.ViewModel);
            if (Context.ViewModel is IDotvvmViewModel viewModel)
            {
                foreach (var child in GetChildViewModels(viewModel))
                {
                    binder.BindParameters(Context, child);
                }
            }
        }

        protected virtual IEnumerable<IPageActionFilter> GetPageFilters()
        {
            return GetFilters<IPageActionFilter>();
        }

        protected virtual Task<DotvvmView> GetView()
        {
            var viewBuilder = Context.Services.GetRequiredService<IDotvvmViewBuilder>();
            return Task.FromResult(viewBuilder.BuildView(Context));
        }

        protected virtual Task<object> GetViewModel()
        {
            var viewModelLoader = Context.Services.GetRequiredService<IViewModelLoader>();
            return Task.FromResult(viewModelLoader.InitializeViewModel(Context, Context.View));
        }

        protected virtual IEnumerable<IViewModelActionFilter> GetViewModelFilters()
        {
            return GetFilters<IViewModelActionFilter>();
        }

        protected virtual void InvokeLifecycleEvent(LifeCycleEventType eventType)
        {
            DotvvmControlCollection.InvokePageLifeCycleEventRecursive(Context.View, eventType);
        }

        protected virtual void Log(string eventName)
        {
            var tracer = Context.Services.GetRequiredService<AggregateRequestTracer>();
            // fire-and-forget logging
            tracer.TraceEvent(eventName, Context);
        }

        private IEnumerable<IDotvvmViewModel> GetChildViewModels(IDotvvmViewModel viewModel)
        {
            // TODO: This is basically copied from DotvvmViewModelBase. Do something about it.
            var type = GetType();
            var properties = ChildViewModelsCache.GetChildViewModelsProperties(type)
                .Select(p => (IDotvvmViewModel)p.GetValue(this, null));
            var collection = ChildViewModelsCache.GetChildViewModelsCollection(type)
                .SelectMany(p => (IEnumerable<IDotvvmViewModel>)p.GetValue(this, null) ?? new IDotvvmViewModel[0]);

            return properties.Concat(collection).Where(c => c != null).ToArray();
        }

        protected IEnumerable<TActionFilter> GetFilters<TActionFilter>()
            where TActionFilter : IActionFilter
        {
            var filters = Context.Configuration.Runtime.GlobalFilters.OfType<TActionFilter>();
            if (Context.ViewModel != null)
            {
                filters = filters.Concat(ActionFilterHelper.GetActionFilters<TActionFilter>(Context.ViewModel.GetType()));
            }

            return filters;
        }
    }
}