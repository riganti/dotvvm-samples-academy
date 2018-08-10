#load "./view_stub.dothtml.csx"

Unit.AddSourcePath("./CalculatorViewModel_properties.cs");
Unit.SetDefaultCodePath("./calculator_stub.dothtml");
Unit.SetCorrectCodePath("./calculator_controls.dothtml");

Unit.GetProperty("/html/body/dot:Literal/@Text").HasBinding("Result");
Unit.GetProperty("/html/body/dot:TextBox[1]/@Text").HasBinding("LeftOperand");
Unit.GetProperty("/html/body/dot:TextBox[2]/@Text").HasBinding("RightOperand");