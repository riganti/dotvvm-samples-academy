#load "./viewmodel_stub.csharp.csx"

Unit.SetDefaultCodePath("./CalculatorViewModel_stub.cs");
Unit.SetCorrectCodePath("./CalculatorViewModel_properties.cs");

Unit.GetType<int>()
    .Allow();

calculatorViewModel.GetProperty("Result")
    .HasAccessibility(Accessibility.Public)
    .HasGetter()
    .HasSetter()
    .Allow();

calculatorViewModel.GetProperty("LeftOperand")
    .HasAccessibility(Accessibility.Public)
    .HasGetter()
    .HasSetter()
    .Allow();

calculatorViewModel.GetProperty("RightOperand")
    .HasAccessibility(Accessibility.Public)
    .HasGetter()
    .HasSetter()
    .Allow();