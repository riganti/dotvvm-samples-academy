CorrectCode = "/resources/CalculatorViewModel_stub.cs";

var viewModel = GetType("CourseFormat.CalculatorViewModel")
    .CountEquals(1)
    .IsTypeKind(CSharpTypeKind.Class)
    .HasAccessibility(CSharpAccessibility.Public);