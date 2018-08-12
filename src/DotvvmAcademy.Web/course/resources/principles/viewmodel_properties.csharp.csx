#load "./viewmodel_stub.csharp.csx"

Unit.SetDefaultCodePath("./CalculatorViewModel_stub.cs");
Unit.SetCorrectCodePath("./CalculatorViewModel_properties.cs");

Unit.GetType<int>()
    .Allow();

var names = new[] { "Result", "LeftOperand", "RightOperand" };
foreach (var name in names)
{
    calculatorViewModel.GetProperty(name)
        .HasAccessibility(Accessibility.Public)
        .HasGetter()
        .HasSetter()
        .Allow();
}