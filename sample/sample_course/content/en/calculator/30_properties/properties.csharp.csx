#load "../10_a_classic/a_classic.csharp.csx"

Unit.SetDefaultCodePath("/resources/CalculatorViewModel_stub.cs");
Unit.SetCorrectCodePath("/resources/CalculatorViewModel_properties.cs");

Unit.GetTypes("System.Int32")
    .Allow();

Unit.GetProperties("SampleCourse.CalculatorViewModel::Result")
    .CountEquals(1)
    .HasAccessibility(CSharpAccessibility.Public)
    .Allow();

Unit.GetProperties("SampleCourse.CalculatorViewModel::LeftOperand")
    .CountEquals(1)
    .HasAccessibility(CSharpAccessibility.Public)
    .Allow();

Unit.GetProperties("SampleCourse.CalculatorViewModel::RightOperand")
    .CountEquals(1)
    .HasAccessibility(CSharpAccessibility.Public)
    .Allow();