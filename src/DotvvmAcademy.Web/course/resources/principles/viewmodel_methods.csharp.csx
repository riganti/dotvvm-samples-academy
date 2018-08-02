#load "./viewmodel_properties.csharp.csx"

Unit.SetDefaultCodePath(ViewModelWithPropertiesPath);
Unit.SetCorrectCodePath(ViewModelWithMethodsPath);

Unit.GetTypes("System.Void")
    .Allow();

Unit.GetMethods($"{ViewModelFullName}::Add")
    .CountEquals(1)
    .HasAccessibility(CSharpAccessibility.Public);

Unit.Run(c =>
{
    var vm = c.Instantiate(ViewModelFullName);
    vm.LeftOperand = 5;
    vm.RightOperand = 11;
    vm.Add();
    if (vm.Result != 16)
    {
        c.Report("Your Add method doesn't add correctly.");
    }
});

Unit.GetMethods($"{ViewModelFullName}::Subtract")
    .CountEquals(1)
    .HasAccessibility(CSharpAccessibility.Public);

Unit.Run(c =>
{
    var vm = c.Instantiate(ViewModelFullName);
    vm.LeftOperand = 7;
    vm.RightOperand = 4;
    vm.Subtract();
    if (vm.Result != 3)
    {
        c.Report("Your Subtract method doesn't subtract correctly.");
    }
});

Unit.GetMethods($"{ViewModelFullName}::Multiply")
    .CountEquals(1)
    .HasAccessibility(CSharpAccessibility.Public);

Unit.Run(c =>
{
    var vm = c.Instantiate(ViewModelFullName);
    vm.LeftOperand = 4;
    vm.RightOperand = 5;
    vm.Multiply();
    if (vm.Result != 20)
    {
        c.Report("Your Multiply method doesn't multiply correctly.");
    }
});

Unit.GetMethods($"{ViewModelFullName}::Divide")
    .CountEquals(1)
    .HasAccessibility(CSharpAccessibility.Public);

Unit.Run(c =>
{
    var vm = c.Instantiate(ViewModelFullName);
    vm.LeftOperand = 21;
    vm.RightOperand = 7;
    vm.Divide();
    if (vm.Result != 3)
    {
        c.Report("Your Divide method doesn't divide correctly.");
    }
});