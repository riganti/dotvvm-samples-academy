#load "./constants.csx"

Unit.SetFileName("CalculatorViewModel.cs");
Unit.SetCorrectCodePath("./CalculatorViewModel_stub.cs");

var calculatorViewModel = Unit.GetType(CalculatorViewModel)
    .IsTypeKind(TypeKind.Class)
    .HasAccessibility(Accessibility.Public);