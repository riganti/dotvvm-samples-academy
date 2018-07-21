#load "../20_the_view"

Unit.SetViewModelPath("/resources/CalculatorViewModel_properties.cs");
Unit.SetDefaultCodePath("/resources/calculator_stub.dothtml");
Unit.SetCorrectCodePath("/resources/calculator_textboxes.dothtml");

Unit.GetControls("/html/body/child::node()")
    .CountEquals(3);

Unit.GetProperties("/html/body/dot:Literal/@Text")
    .CountEquals(1)
    .HasBinding("Result");

Unit.GetProperties("/html/body/dot:TextBox[1]/@Text")
    .CountEquals(1)
    .HasBinding("LeftOperand");

Unit.GetProperties("/html/body/dot:TextBox[2]/@Text")
    .CountEquals(1)
    .HasBinding("RightOperand");