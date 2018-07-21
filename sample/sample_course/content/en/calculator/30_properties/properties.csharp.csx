#load "../10_a_classic"

Unit.SetDefaultCodePath("/resources/CalculatorViewModel_stub.cs");
Unit.SetCorrectCodePath("/resources/CalculatorViewModel_properties.cs");

Unit.GetTypes("System.Int32")
    .Allow();

Unit.GetProperties("System.Int32 SampleCourse.CalculatorViewModel::Result()")
    .CountEquals(1)
    .HasAccessibility(CSharpAccessibility.Public);

Unit.GetProperties("System.Int32 SampleCourse.CalculatorViewModel::LeftOperand()")
    .CountEquals(1)
    .HasAccessibility(CSharpAccessibility.Public);

Unit.GetProperties("System.Int32 SampleCourse.CalculatorViewModel::RightOperand()")
    .CountEquals(1)
    .HasAccessibility(CSharpAccessibility.Public);