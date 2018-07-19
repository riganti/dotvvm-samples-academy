#load "../40_textboxes"

DefaultCode = "/resources/calculator_textboxes.dothtml";
CorrectCode = "/resources/calculator_buttons.dothtml";

GetControls("/html/body/dot:Button")
    .CountEquals(4);

GetProperties("/html/body/dot:Button[1]/@Click")
    .HasBinding("Add()", BindingKind.Command);

GetProperties("/html/body/dot:Button[1]/@Text")
    .StringEquals("+");

GetProperties("/html/body/dot:Button[2]/@Click")
    .HasBinding("Subtract()", BindingKind.Command);

GetProperties("/html/body/dot:Button[2]/@Text")
    .StringEquals("-");

GetProperties("/html/body/dot:Button[3]/@Click")
    .HasBinding("Multiply()", BindingKind.Command);

GetProperties("/html/body/dot:Button[3]/@Text")
    .StringEquals("*");
    
GetProperties("/html/body/dot:Button[4]/@Click")
    .HasBinding("Divide()", BindingKind.Command);

GetProperties("/html/body/dot:Button[4]/@Text")
    .StringEquals("/");