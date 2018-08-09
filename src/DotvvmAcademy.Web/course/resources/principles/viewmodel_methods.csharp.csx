#load "./viewmodel_properties.csharp.csx"

Unit.SetDefaultCodePath("./CalculatorViewModel_properties.cs");
Unit.SetCorrectCodePath("./CalculatorViewModel_methods.cs");

Unit.GetType(WellKnownTypes.Void)
    .Allow();

calculatorViewModel.GetMethod("Add")
    .HasAccessibility(Accessibility.Public)
    .HasParameters();

calculatorViewModel.GetMethod("Subtract")
    .HasAccessibility(Accessibility.Public)
    .HasParameters();

calculatorViewModel.GetMethod("Multiply")
    .HasAccessibility(Accessibility.Public)
    .HasParameters();

calculatorViewModel.GetMethod("Divide")
    .HasAccessibility(Accessibility.Public)
    .HasParameters();

Unit.Run(c =>
{
    var vm = c.Instantiate(CalculatorViewModel);
    vm.LeftOperand = 5;
    vm.RightOperand = 11;
    vm.Add();
    if (vm.Result != 16)
    {
        c.Report("Your Add method doesn't add correctly.");
    }
});

Unit.Run(c =>
{
    var vm = c.Instantiate(CalculatorViewModel);
    vm.LeftOperand = 7;
    vm.RightOperand = 4;
    vm.Subtract();
    if (vm.Result != 3)
    {
        c.Report("Your Subtract method doesn't subtract correctly.");
    }
});

Unit.Run(c =>
{
    var vm = c.Instantiate(CalculatorViewModel);
    vm.LeftOperand = 4;
    vm.RightOperand = 5;
    vm.Multiply();
    if (vm.Result != 20)
    {
        c.Report("Your Multiply method doesn't multiply correctly.");
    }
});

Unit.Run(c =>
{
    var vm = c.Instantiate(CalculatorViewModel);
    vm.LeftOperand = 21;
    vm.RightOperand = 7;
    vm.Divide();
    if (vm.Result != 3)
    {
        c.Report("Your Divide method doesn't divide correctly.");
    }
});