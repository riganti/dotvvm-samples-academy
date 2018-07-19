#load "../20_the_view"

DefaultCode = "/resources/calculator_stub.dothtml";
CorrectCode = "/resources/calculator_textboxes.dothtml";

GetControls("/html/body/child::node()")
    .CountEquals(3);

GetProperties("/html/body/dot:Literal/@Text")
    .CountEquals(1)
    .HasBinding("Result");

GetProperties("/html/body/dot:TextBox[1]/@Text")
    .CountEquals(1)
    .HasBinding("LeftOperand");

GetProperties("/html/body/dot:TextBox[2]/@Text")
    .CountEquals(1)
    .HasBinding("RightOperand");