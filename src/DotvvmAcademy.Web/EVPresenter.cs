using DotVVM.Framework.Binding;
using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Binding.Properties;
using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Controls.Infrastructure;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Runtime;
using DotVVM.Framework.Runtime.Filters;
using DotVVM.Framework.Runtime.Tracing;
using DotVVM.Framework.Security;
using DotVVM.Framework.Utils;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.ViewModel.Serialization;
using DotvvmAcademy.CourseFormat;
using DotvvmAcademy.Meta;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Loader;
using System.Security;
using System.Threading.Tasks;

namespace DotvvmAcademy.Web
{
    public class EVPresenter : IDotvvmPresenter
    {
        public virtual async Task ProcessRequest(IDotvvmRequestContext dotvvmContext)
        {
            // TODO: Shouldn't tracer be fire-and-forget style?
            // this method should only call the appropriate most specialized method for this request
            try
            {
                var context = new PresenterContext(dotvvmContext);
                context.EnsureRequestMethod();
                if (context.IsStaticCommand())
                {
                    await ProcessStaticCommand(context);
                }
                else if (context.IsPostback())
                {
                    await ProcessPostback(context);
                }
                else
                {
                    await ProcessRegular(context);
                }
            }
            catch (UnauthorizedAccessException)
            {
                dotvvmContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            catch (DotvvmControlException ex)
            {
                if (ex.FileName != null)
                {
                    ex.FileName = Path.Combine(dotvvmContext.Configuration.ApplicationPhysicalPath, ex.FileName);
                }
                throw;
            }
        }

        protected virtual async Task ProcessPostback(PresenterContext context)
        {
            context.DotvvmContext.IsPostBack = true;
            await context.InitializeView();
            await context.InitializeViewModel();
            context.ViewModelFilters = context.GetFilters<IViewModelActionFilter>();
            context.PageFilters = context.GetFilters<IPageActionFilter>();
            await context.OnPageInitialized();
            try
            {
                // PRE-INIT
                context.InvokePageLifeCycle(LifeCycleEventType.PreInit);

                // INIT
                context.DotvvmContext.View.DataContext = context.DotvvmContext.ViewModel;
                await context.OnViewModelCreated();
                context.BindParameters();
                await context.InvokeViewModelInit();
                context.InvokePageLifeCycle(LifeCycleEventType.Init);
                await context.TraceEvent(RequestTracingConstants.InitCompleted);

                // LOAD
                await context.DeserializeViewModel();
                await context.OnViewModelDeserialized();
                await context.ValidateCsrfToken();
                await context.InvokeViewModelLoad();
                context.InvokePageLifeCycle(LifeCycleEventType.Load);
                await context.TraceEvent(RequestTracingConstants.LoadCompleted);
                context.CommandFilters = context.GetCommandFilters();
                await context.ExecuteCommand();
                await context.TraceEvent(RequestTracingConstants.CommandExecuted);

                // PRE-RENDER
                await context.InvokeViewModelPreRender();
                context.InvokePageLifeCycle(LifeCycleEventType.PreRender);

                // PRE-RENDER COMPLETE
                context.InvokePageLifeCycle(LifeCycleEventType.PreRenderComplete);
                await context.TraceEvent(RequestTracingConstants.PreRenderCompleted);

                // Write response
                context.EnsureCsrfToken();
                await context.OnViewModelSerializing();
                var serializer = context.DotvvmContext.Services.GetRequiredService<IViewModelSerializer>();
                serializer.BuildViewModel(context.DotvvmContext);
                if (context.CommandResult != null)
                {
                    context.DotvvmContext.ViewModelJson["commandResult"] = JToken.FromObject(context.CommandResult);
                }
                var renderer = context.DotvvmContext.Services.GetRequiredService<IOutputRenderer>();
                var patch = renderer.RenderPostbackUpdatedControls(context.DotvvmContext, context.DotvvmContext.View);
                serializer.AddPostBackUpdatedControls(context.DotvvmContext, patch);
                await renderer.WriteViewModelResponse(context.DotvvmContext, context.DotvvmContext.View);
                await context.OnPageRendered();
            }
            catch (Exception exception) when (!(exception is DotvvmInterruptRequestExecutionException || exception is DotvvmHttpException))
            {
                await context.OnPageException(exception);
                throw;
            }
            finally
            {
                context.DisposeViewModel();
            }
        }

        protected virtual async Task ProcessRegular(PresenterContext context)
        {
            await context.InitializeView();
            await context.InitializeViewModel();
            context.ViewModelFilters = context.GetFilters<IViewModelActionFilter>();
            context.PageFilters = context.GetFilters<IPageActionFilter>();
            await context.OnPageInitialized();
            try
            {
                // PRE-INIT
                context.InvokePageLifeCycle(LifeCycleEventType.PreInit);

                // INIT
                context.DotvvmContext.View.DataContext = context.DotvvmContext.ViewModel;
                await context.OnViewModelCreated();
                context.BindParameters();
                await context.InvokeViewModelInit();
                context.InvokePageLifeCycle(LifeCycleEventType.Init);
                await context.TraceEvent(RequestTracingConstants.InitCompleted);

                // LOAD
                await context.InvokeViewModelLoad();
                context.InvokePageLifeCycle(LifeCycleEventType.Load);
                await context.TraceEvent(RequestTracingConstants.LoadCompleted);

                // PRE-RENDER
                await context.InvokeViewModelPreRender();
                context.InvokePageLifeCycle(LifeCycleEventType.PreRender);

                // PRE-RENDER COMPLETE
                context.InvokePageLifeCycle(LifeCycleEventType.PreRenderComplete);
                await context.TraceEvent(RequestTracingConstants.PreRenderCompleted);

                // Write response
                context.EnsureCsrfToken();
                await context.OnViewModelSerializing();
                var serializer = context.DotvvmContext.Services.GetRequiredService<IViewModelSerializer>();
                serializer.BuildViewModel(context.DotvvmContext);
                var renderer = context.DotvvmContext.Services.GetRequiredService<IOutputRenderer>();
                await renderer.WriteHtmlResponse(context.DotvvmContext, context.DotvvmContext.View);
                await context.OnPageRendered();
            }
            catch (Exception exception) when (!(exception is DotvvmInterruptRequestExecutionException || exception is DotvvmHttpException))
            {
                await context.OnPageException(exception);
                throw;
            }
            finally
            {
                context.DisposeViewModel();
            }
        }

        protected virtual async Task ProcessStaticCommand(PresenterContext context)
        {
            await context.DeserializeStaticCommand();
            await context.ValidateCsrfToken();
            context.CommandFilters = context.GetCommandFilters();
            await context.ExecuteCommand();

            var serializer = context.DotvvmContext.Services.GetRequiredService<IViewModelSerializer>();
            var renderer = context.DotvvmContext.Services.GetRequiredService<IOutputRenderer>();
            await renderer.WriteStaticCommandResponse(
                context: context.DotvvmContext,
                json: serializer.BuildStaticCommandResponse(context.DotvvmContext, context.CommandResult));
            await context.TraceEvent(RequestTracingConstants.StaticCommandExecuted);
        }

        public class PresenterContext
        {
            public PresenterContext(IDotvvmRequestContext dotvvmContext)
            {
                DotvvmContext = dotvvmContext;
            }

            public ActionInfo ActionInfo { get; set; }

            public IEnumerable<ICommandActionFilter> CommandFilters { get; set; }

            public object CommandResult { get; set; }

            public IDotvvmRequestContext DotvvmContext { get; }

            public StaticCommandInvocationPlan InvocationPlan { get; set; }

            public IEnumerable<IPageActionFilter> PageFilters { get; set; }

            public JObject StaticCommandJson { get; set; }

            public IEnumerable<IViewModelActionFilter> ViewModelFilters { get; set; }

            public virtual void BindParameters()
            {
                var binder = DotvvmContext.Services.GetRequiredService<IViewModelParameterBinder>();
                binder.BindParameters(DotvvmContext, DotvvmContext.ViewModel);
                if (DotvvmContext.ViewModel is IDotvvmViewModel viewModel)
                {
                    foreach (var child in GetChildViewModels(viewModel))
                    {
                        binder.BindParameters(DotvvmContext, child);
                    }
                }
            }

            public virtual async Task DeserializeStaticCommand()
            {
                using (var jsonReader = new JsonTextReader(new StreamReader(DotvvmContext.HttpContext.Request.Body)))
                {
                    StaticCommandJson = await JObject.LoadAsync(jsonReader);
                }
                DotvvmContext.CsrfToken = StaticCommandJson["$csrfToken"].Value<string>();
                var command = StaticCommandJson["command"].Value<string>();
                InvocationPlan = StaticCommandBindingCompiler.DecryptJson(
                    data: Convert.FromBase64String(command),
                    protector: DotvvmContext.Services.GetRequiredService<IViewModelProtector>())
                    .Apply(StaticCommandBindingCompiler.DeserializePlan);
                var arguments = new Queue<JToken>(StaticCommandJson["args"] as JArray);
                ActionInfo = new ActionInfo
                {
                    Action = () => ExecuteInvocationPlan(InvocationPlan, arguments, DotvvmContext)
                };
            }

            public virtual async Task DeserializeViewModel()
            {
                using (var reader = new StreamReader(DotvvmContext.HttpContext.Request.Body))
                {
                    var body = await reader.ReadToEndAsync();
                    var serializer = DotvvmContext.Services.GetRequiredService<IViewModelSerializer>();
                    serializer.PopulateViewModel(DotvvmContext, body);
                    ActionInfo = serializer.ResolveCommand(DotvvmContext, DotvvmContext.View);
                }
            }

            public virtual void DisposeViewModel()
            {
            }

            public virtual void EnsureCsrfToken()
            {
                if (string.IsNullOrEmpty(DotvvmContext.CsrfToken))
                {
                    var protector = DotvvmContext.Services.GetRequiredService<ICsrfProtector>();
                    DotvvmContext.CsrfToken = protector.GenerateToken(DotvvmContext);
                }
            }

            public virtual void EnsureRequestMethod()
            {
                if (DotvvmContext.HttpContext.Request.Method != "GET" && DotvvmContext.HttpContext.Request.Method != "POST")
                {
                    DotvvmContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                    throw new DotvvmHttpException("Only GET and POST methods are supported!");
                }
            }

            public virtual async Task OnCommandExecuting()
            {
                foreach (var filter in CommandFilters)
                {
                    await filter.OnCommandExecutingAsync(DotvvmContext, ActionInfo);
                }
            }

            public virtual async Task OnCommandExecuted()
            {
                foreach (var filter in CommandFilters.Reverse())
                {
                    await filter.OnCommandExecutedAsync(DotvvmContext, ActionInfo, DotvvmContext.CommandException);
                }
                if (DotvvmContext.CommandException != null && !DotvvmContext.IsCommandExceptionHandled)
                {
                    throw new Exception("Unhandled exception occurred in the command!", DotvvmContext.CommandException);
                }
            }

            public virtual async Task ExecuteCommand()
            {
                await OnCommandExecuting();
                try
                {
                    CommandResult = ActionInfo.Action();
                    if (CommandResult is Task taskResult)
                    {
                        await taskResult;
                    }
                }
                catch (Exception exception)
                {
                    if (exception is TargetInvocationException)
                    {
                        exception = exception.InnerException;
                    }

                    if (exception is DotvvmInterruptRequestExecutionException)
                    {
                        throw new DotvvmInterruptRequestExecutionException("The request execution was interrupted in the command!", exception);
                    }

                    DotvvmContext.CommandException = exception;
                }

                // run OnCommandExecuted on action filters
                await OnCommandExecuted();

                if (CommandResult is Task task)
                {
                    CommandResult = TaskUtils.GetResult(task);
                }
            }

            public virtual IEnumerable<ICommandActionFilter> GetCommandFilters()
            {
                var commandFilters = GetFilters<ICommandActionFilter>();
                var property = ActionInfo.Binding.GetProperty<ActionFiltersBindingProperty>(ErrorHandlingMode.ReturnNull);
                if (property != null)
                {
                    commandFilters = commandFilters.Concat(property.Filters.OfType<ICommandActionFilter>());
                }

                if (InvocationPlan != null)
                {
                    var planFilters = InvocationPlan.GetAllMethods()
                        .SelectMany(m => ActionFilterHelper.GetActionFilters<ICommandActionFilter>(m))
                        .ToArray();
                    commandFilters = commandFilters.Concat(planFilters);
                }

                return commandFilters;
            }

            public virtual IEnumerable<TFilter> GetFilters<TFilter>()
                where TFilter : IActionFilter
            {
                return ActionFilterHelper.GetActionFilters<TFilter>(DotvvmContext.ViewModel.GetType())
                    .Concat(DotvvmContext.Configuration.Runtime.GlobalFilters.OfType<TFilter>());
            }

            public virtual async Task InitializeView()
            {
                var hack = DotvvmContext.Services.GetRequiredService<EVHackService>();
                DotvvmContext.View = await hack.BuildView(this);
                DotvvmContext.View.SetValue(Internal.RequestContextProperty, DotvvmContext);
                await TraceEvent(RequestTracingConstants.ViewInitialized);
            }

            public virtual Task InitializeViewModel()
            {
                var loader = DotvvmContext.Services.GetRequiredService<IViewModelLoader>();
                DotvvmContext.ViewModel = loader.InitializeViewModel(DotvvmContext, DotvvmContext.View);
                return Task.CompletedTask;
            }

            public virtual void InvokePageLifeCycle(LifeCycleEventType eventType)
            {
                DotvvmControlCollection.InvokePageLifeCycleEventRecursive(DotvvmContext.View, eventType);
            }

            public virtual async Task InvokeViewModelInit()
            {
                if (DotvvmContext.ViewModel is IDotvvmViewModel viewModel)
                {
                    viewModel.Context = DotvvmContext;
                    ChildViewModelsCache.SetViewModelClientPath(viewModel, ChildViewModelsCache.RootViewModelPath);
                    await viewModel.Init();
                }
            }

            public virtual async Task InvokeViewModelLoad()
            {
                if (DotvvmContext.ViewModel is IDotvvmViewModel viewModel)
                {
                    await viewModel.Load();
                }
            }

            public virtual async Task InvokeViewModelPreRender()
            {
                if (DotvvmContext.ViewModel is IDotvvmViewModel viewModel)
                {
                    await viewModel.PreRender();
                }
            }

            public virtual bool IsPostback()
            {
                return DotvvmContext.HttpContext.Request.Method == "POST"
                    && DotvvmContext.HttpContext.Request.Headers.ContainsKey(HostingConstants.SpaPostBackHeaderName);
            }

            public virtual bool IsStaticCommand()
            {
                return DotvvmContext.HttpContext.Request.Headers["X-PostbackType"] == "StaticCommand";
            }

            public virtual async Task OnPageException(Exception exception)
            {
                foreach (var pageFilter in PageFilters)
                {
                    await pageFilter.OnPageExceptionAsync(DotvvmContext, exception);
                    if (DotvvmContext.IsPageExceptionHandled)
                    {
                        DotvvmContext.InterruptRequest();
                    }
                }
            }

            public virtual async Task OnPageInitialized()
            {
                foreach (var pageFilter in PageFilters)
                {
                    await pageFilter.OnPageInitializedAsync(DotvvmContext);
                }
                await TraceEvent(RequestTracingConstants.ViewInitialized);
            }

            public virtual async Task OnPageRendered()
            {
                foreach (var pageFilter in PageFilters)
                {
                    await pageFilter.OnPageRenderedAsync(DotvvmContext);
                }
                await TraceEvent(RequestTracingConstants.OutputRendered);
            }

            public virtual async Task OnViewModelCreated()
            {
                foreach (var viewModelFilter in ViewModelFilters)
                {
                    await viewModelFilter.OnViewModelCreatedAsync(DotvvmContext);
                }
                await TraceEvent(RequestTracingConstants.ViewModelCreated);
            }

            public virtual async Task OnViewModelDeserialized()
            {
                foreach (var viewModelFilter in ViewModelFilters)
                {
                    await viewModelFilter.OnViewModelDeserializedAsync(DotvvmContext);
                }
                await TraceEvent(RequestTracingConstants.ViewModelDeserialized);
            }

            public virtual async Task OnViewModelSerializing()
            {
                foreach (var viewModelFilter in ViewModelFilters)
                {
                    await viewModelFilter.OnViewModelSerializingAsync(DotvvmContext);
                }
                await TraceEvent(RequestTracingConstants.ViewModelSerialized);
            }

            public virtual Task SerializeViewModel()
            {


                return Task.CompletedTask;
            }

            public virtual Task SerializeViewModelPatch()
            {

                return Task.CompletedTask;
            }

            public virtual Task TraceEvent(string @event)
            {
                return DotvvmContext.Services.GetRequiredService<AggregateRequestTracer>().TraceEvent(@event, DotvvmContext);
            }

            public virtual async Task ValidateCsrfToken()
            {
                var protector = DotvvmContext.Services.GetRequiredService<ICsrfProtector>();
                try
                {
                    protector.VerifyToken(DotvvmContext, DotvvmContext.CsrfToken);
                }
                catch (SecurityException exception)
                {
                    DotvvmContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    await DotvvmContext.HttpContext.Response.WriteAsync(exception.Message);
                    DotvvmContext.InterruptRequest();
                }
            }

            private static object ExecuteInvocationPlan(
                StaticCommandInvocationPlan invocationPlan,
                Queue<JToken> jsonArguments,
                IDotvvmRequestContext context)
            {
                var arguments = new object[invocationPlan.Arguments.Length];
                for (int i = 0; i < invocationPlan.Arguments.Length; i++)
                {
                    var argumentPlan = invocationPlan.Arguments[i];
                    switch (argumentPlan.Type)
                    {
                        case StaticCommandParameterType.Argument:
                            arguments[i] = jsonArguments.Dequeue().ToObject((Type)argumentPlan.Arg);
                            break;

                        case StaticCommandParameterType.Constant:
                        case StaticCommandParameterType.DefaultValue:
                            arguments[i] = argumentPlan.Arg;
                            break;

                        case StaticCommandParameterType.Invocation:
                            arguments[i] = ExecuteInvocationPlan(
                                invocationPlan: (StaticCommandInvocationPlan)argumentPlan.Arg,
                                jsonArguments: jsonArguments,
                                context: context);
                            break;

                        case StaticCommandParameterType.Inject:
                        default:
                            throw new NotSupportedException();
                    }
                }
                var instance = invocationPlan.Method.IsStatic ? null : arguments.First();
                arguments = invocationPlan.Method.IsStatic ? arguments.Skip(1).ToArray() : arguments;
                return invocationPlan.Method.Invoke(instance, arguments);
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
        }
    }
}