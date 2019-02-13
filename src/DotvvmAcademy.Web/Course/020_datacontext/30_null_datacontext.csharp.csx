#load "10_moving_properties.csharp.csx"

using DotvvmAcademy.Validation.CSharp;
using DotvvmAcademy.Validation.CSharp.Unit;
using DotvvmAcademy.Validation.Unit;

Unit.SetDefault("ProfileDetailViewModel_30.cs");
Unit.SetCorrect("ProfileDetailViewModel_40.cs");

Unit.GetType(typeof(void))
    .Allow();

viewModel.GetMethod(CreateMethod)
    .RequireAccess(AllowedAccess.Public)
    .RequireReturnType(typeof(void))
    .RequireParameterless();

viewModel.GetMethod(DeleteMethod)
    .RequireAccess(AllowedAccess.Public)
    .RequireReturnType(typeof(void))
    .RequireParameterless();

Unit.Run(context => {
    var vm = context.InstantiateViewModel(ViewModelName);
    if (vm.Profile != null) {
        context.Report(InitializedEarlyDiagnosticMessage);
        return;
    }
    context.Invoke(vm, CreateMethod);
    if (vm.Profile == null) {
        context.Report(NotInitializedDiagnosticMessage);
        return;
    }
    context.Invoke(vm, DeleteMethod);
    if (vm.Profile != null) {
        context.Report(NotDeletedDiagnosticMessage);
    }
})