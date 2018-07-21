#load "../10_a_classic"

DefaultCode = "/resources/CalculatorViewModel_stub.cs";
CorrectCode = "/resources/CalculatorViewModel_properties.cs";

GetType<int>()
    .Allow();

GetProperty("System.Int32 SampleCourse.CalculatorViewModel::Result()")
    .CountEquals(1)
    .HasAccessibility(CSharpAccessibility.Public);

GetProperty("System.Int32 SampleCourse.CalculatorViewModel::LeftOperand()")
    .CountEquals(1)
    .HasAccessibility(CSharpAccessibility.Public);

GetProperty("System.Int32 SampleCourse.CalculatorViewModel::RightOperand()")
    .CountEquals(1)
    .HasAccessibility(CSharpAccessibility.Public);