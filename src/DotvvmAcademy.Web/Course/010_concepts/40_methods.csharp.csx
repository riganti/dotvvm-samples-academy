#load "10_viewmodel.csharp.csx"

using System;
using DotvvmAcademy.Meta;
using DotvvmAcademy.Validation.Unit;
using DotvvmAcademy.Validation.CSharp;
using DotvvmAcademy.Validation.CSharp.Unit;

Unit.GetType(typeof(void))
    .Allow();

var random = new Random();

void ValidateMethod(string name, Func<int, int, int> operation)
{
    viewModel.GetMethod(name)
        .RequireAccess(AllowedAccess.Public)
        .RequireParameterless()
        .RequireReturnType(typeof(void));

    Unit.Run(c =>
    {
        var vm = c.Instantiate(ViewModelName);
        vm.Result = random.Next(1, 100);
        vm.Difference = random.Next(1, 100);
        var result = operation(vm.Result, vm.Difference);
        c.Invoke(vm, name);
        if (vm.Result != result)
        {
            c.Report(string.Format(MethodDiagnosticMessage, name, vm.Difference, vm.Result));
        }
    });
}

ValidateMethod(IncrementName, (l, r) => l + r);
ValidateMethod(DecrementName, (l, r) => l - r);