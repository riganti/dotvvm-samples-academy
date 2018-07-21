#load "../40_textboxes/textboxes.dothtml.csx"

Unit.SetViewModelPath("/resources/CalculatorViewModel_methods.cs");
Unit.SetDefaultCodePath("/resources/calculator_textboxes.dothtml");
Unit.SetCorrectCodePath("/resources/calculator_buttons.dothtml");

Unit.GetControls("/html/body/dot:Button")
    .CountEquals(4);

Unit.GetProperties("/html/body/dot:Button[1]/@Click")
    .HasBinding("Add()", BindingKind.Command);

Unit.GetProperties("/html/body/dot:Button[1]/@Text")
    .StringEquals("+");

Unit.GetProperties("/html/body/dot:Button[2]/@Click")
    .HasBinding("Subtract()", BindingKind.Command);

Unit.GetProperties("/html/body/dot:Button[2]/@Text")
    .StringEquals("-");

Unit.GetProperties("/html/body/dot:Button[3]/@Click")
    .HasBinding("Multiply()", BindingKind.Command);

Unit.GetProperties("/html/body/dot:Button[3]/@Text")
    .StringEquals("*");
    
Unit.GetProperties("/html/body/dot:Button[4]/@Click")
    .HasBinding("Divide()", BindingKind.Command);

Unit.GetProperties("/html/body/dot:Button[4]/@Text")
    .StringEquals("/");