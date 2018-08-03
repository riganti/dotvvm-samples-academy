#load "./view_stub.dothtml.csx"

Unit.SetViewModelPath("./CalculatorViewModel_properties.cs");
Unit.SetDefaultCodePath("./calculator_stub.dothtml");
Unit.SetCorrectCodePath("./calculator_textboxes.dothtml");

Unit.GetProperties("/html/body/dot:Literal/@Text")
    .CountEquals(1)
    .HasBinding("Result");

Unit.GetProperties("/html/body/dot:TextBox[1]/@Text")
    .CountEquals(1)
    .HasBinding("LeftOperand");

Unit.GetProperties("/html/body/dot:TextBox[2]/@Text")
    .CountEquals(1)
    .HasBinding("RightOperand");