#load "10_viewmodel.csharp.csx"

using System;
using DotvvmAcademy.Meta;
using DotvvmAcademy.Validation.Unit;
using DotvvmAcademy.Validation.CSharp;
using DotvvmAcademy.Validation.CSharp.Unit;

Unit.SetDefault("CalculatorViewModel_30.cs");
Unit.SetCorrect("CalculatorViewModel_40.cs");

Unit.GetType(typeof(void))
    .Allow();

var random = new Random();

void ValidateMethod(string name, Func<int, int, int> operation)
{
    viewModelType.GetMethod(name)
        .RequireAccess(AllowedAccess.Public)
        .RequireParameterless()
        .RequireReturnType(typeof(void));

    Unit.Run(c =>
    {
        var vm = c.Instantiate(CalculatorViewModel);
        vm.Number1 = random.Next(1, 100);
        vm.Number2 = random.Next(1, 100);
        var result = operation(vm.Number1, vm.Number2);
        c.Invoke(vm, name);
        if (vm.Result != result)
        {
            c.Report($"Your '{name}' method produced an incorrect result with operands '{vm.Number1}' and '{vm.Number2}'.");
        }
    });
}

ValidateMethod("Add", (l, r) => l + r);
ValidateMethod("Subtract", (l, r) => l - r);
ValidateMethod("Multiply", (l, r) => l * r);
ValidateMethod("Divide", (l, r) => l / r);