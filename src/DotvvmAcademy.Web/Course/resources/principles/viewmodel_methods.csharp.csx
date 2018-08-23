#load "./viewmodel_properties.csharp.csx"

Unit.SetDefaultCodePath("./CalculatorViewModel_properties.cs");
Unit.SetCorrectCodePath("./CalculatorViewModel_methods.cs");

Unit.GetType(WellKnownTypes.Void)
    .Allow();

var random = new Random();

void ValidateMethod(string name, Func<int, int, int> operation)
{
    calculatorViewModel.GetMethod(name)
        .HasAccessibility(Accessibility.Public)
        .HasParameters();

    Unit.Run(c =>
    {
        var vm = c.Instantiate(CalculatorViewModel);
        for (int i = 0; i < 3; i++)
        {
            vm.LeftOperand = random.Next(1, 1024);
            vm.RightOperand = random.Next(1, 1024);
            var result = operation(vm.LeftOperand, vm.RightOperand);
            c.Invoke(vm, name);
            if (vm.Result != result)
            {
                c.Report($"Your '{name}' method doesn't work with operands '{vm.LeftOperand}' and '{vm.RightOperand}'.");
            }
        }

    });
}

ValidateMethod("Add", (l, r) => l + r);
ValidateMethod("Subtract", (l, r) => l - r);
ValidateMethod("Multiply", (l, r) => l * r);
ValidateMethod("Divide", (l, r) => l / r);