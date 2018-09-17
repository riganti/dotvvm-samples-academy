using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Binding.Properties;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Runtime.Filters;
using DotVVM.Framework.Security;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.ViewModel.Serialization;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using DotVVM.Framework.Utils;
using Newtonsoft.Json.Linq;
using DotVVM.Framework.Runtime;

namespace DotvvmAcademy.Web.Hosting
{
    public class PostbackLifecycleStrategy : RegularLifecycleStrategy
    {
        public PostbackLifecycleStrategy(IDotvvmRequestContext context) : base(context)
        {
        }

        protected IEnumerable<ICommandActionFilter> CommandFilters { get; set; }

        protected ActionInfo ActionInfo { get; set; }

        protected object CommandResult { get; set; }

        public override bool CanProcess()
        {
            return Context.HttpContext.Request.Method == "POST"
                && Context.HttpContext.Request.Headers.ContainsKey(HostingConstants.SpaPostBackHeaderName);
        }

        public override Task PreInit()
        {
            Context.IsPostBack = true;
            return base.PreInit();
        }

        public override async Task Load()
        {
            using (var reader = new StreamReader(Context.HttpContext.Request.Body))
            {
                var body = await reader.ReadToEndAsync();
                var serializer = Context.Services.GetRequiredService<IViewModelSerializer>();
                serializer.PopulateViewModel(Context, body);
                ActionInfo = serializer.ResolveCommand(Context, Context.View);
            }
            await Task.WhenAll(ViewModelFilters.Select(f => f.OnViewModelDeserializedAsync(Context)));
            var protector = Context.Services.GetRequiredService<ICsrfProtector>();
            try
            {
                protector.VerifyToken(Context, Context.CsrfToken);
            }
            catch (SecurityException exception)
            {
                Context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await Context.HttpContext.Response.WriteAsync(exception.Message);
                Context.InterruptRequest();
            }
            await base.Load();
            CommandFilters = GetCommandFilters();
            await ExecuteCommand();
        }

        public override async Task Render()
        {
            if (string.IsNullOrEmpty(Context.CsrfToken))
            {
                var protector = Context.Services.GetRequiredService<ICsrfProtector>();
                Context.CsrfToken = protector.GenerateToken(Context);
            }

            await Task.WhenAll(ViewModelFilters.Select(f => f.OnViewModelSerializingAsync(Context)));
            var serializer = Context.Services.GetRequiredService<IViewModelSerializer>();
            serializer.BuildViewModel(Context);
            if (CommandResult != null)
            {
                Context.ViewModelJson["commandResult"] = JToken.FromObject(CommandResult);
            }
            var renderer = Context.Services.GetRequiredService<IOutputRenderer>();
            var patch = renderer.RenderPostbackUpdatedControls(Context, Context.View);
            serializer.AddPostBackUpdatedControls(Context, patch);
            await renderer.WriteViewModelResponse(Context, Context.View);
            await Task.WhenAll(PageFilters.Select(f => f.OnPageRenderedAsync(Context)));
            Log(RequestTracingConstants.OutputRendered);
        }

        protected virtual async Task ExecuteCommand()
        {
            await Task.WhenAll(CommandFilters.Select(f => f.OnCommandExecutingAsync(Context, ActionInfo)));
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

                Context.CommandException = exception;
            }
            if (CommandResult is Task task)
            {
                CommandResult = TaskUtils.GetResult(task);
            }

            await Task.WhenAll(CommandFilters.Select(f => f.OnCommandExecutedAsync(Context, ActionInfo, Context.CommandException)));
            Log(RequestTracingConstants.CommandExecuted);
        }

        protected virtual IEnumerable<ICommandActionFilter> GetCommandFilters()
        {
            var filters = GetFilters<ICommandActionFilter>();
            if (ActionInfo == null)
            {
                return filters;
            }

            var filtersProperty = ActionInfo.Binding.GetProperty<ActionFiltersBindingProperty>(ErrorHandlingMode.ReturnNull);
            if (filtersProperty != null)
            {
                filters = filters.Concat(filtersProperty.Filters.OfType<ICommandActionFilter>());
            }
            return filters;
        }
    }
}
