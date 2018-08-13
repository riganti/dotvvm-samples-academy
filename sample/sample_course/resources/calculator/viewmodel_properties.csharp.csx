#load "./viewmodel_stub.csharp.csx"

Unit.SetDefaultCodePath("./CalculatorViewModel_stub.cs");
Unit.SetCorrectCodePath("./CalculatorViewModel_properties.cs");

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