#load "./constants.csx"

Unit.SetFileName("CalculatorViewModel.cs");
Unit.SetDefaultCodePath("./CalculatorViewModel_stub.cs");
Unit.SetCorrectCodePath("./CalculatorViewModel_properties.cs");

Unit.GetType<int>()
    .Allow();

var calculatorViewModel = Unit.GetType(CalculatorViewModel)
    .IsTypeKind(TypeKind.Class)
    .HasAccessibility(Accessibility.Public);

var names = new[] { "Result", "LeftOperand", "RightOperand" };
foreach (var name in names)
{
    calculatorViewModel.GetProperty(name)
        .HasAccessibility(Accessibility.Public)
        .HasGetter()
        .HasSetter()
        .Allow();
}