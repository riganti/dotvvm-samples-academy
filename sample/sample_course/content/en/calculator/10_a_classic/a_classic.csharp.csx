Unit.SetCorrectCodePath("/resources/CalculatorViewModel_stub.cs");

Unit.GetTypes("SampleCourse.CalculatorViewModel")
    .CountEquals(1)
    .IsTypeKind(CSharpTypeKind.Class)
    .HasAccessibility(CSharpAccessibility.Public);