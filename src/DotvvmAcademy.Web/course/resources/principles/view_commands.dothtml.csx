#load "./view_controls.dothtml.csx"

Unit.SetViewModelPath(ViewModelWithMethodsPath);
Unit.SetDefaultCodePath(ViewWithControlsPath);
Unit.SetCorrectCodePath(ViewWithCommandsPath);

Unit.GetControls("/html/body/dot:Button")
    .CountEquals(4);

Unit.GetProperties("/html/body/dot:Button[1]/@Click")
    .CountEquals(1)
    .HasBinding("Add()", BindingKind.Command);

Unit.GetProperties("/html/body/dot:Button[1]/@Text")
    .CountEquals(1)
    .HasValue("+");

Unit.GetProperties("/html/body/dot:Button[2]/@Click")
    .CountEquals(1)
    .HasBinding("Subtract()", BindingKind.Command);

Unit.GetProperties("/html/body/dot:Button[2]/@Text")
    .CountEquals(1)
    .HasValue("-");

Unit.GetProperties("/html/body/dot:Button[3]/@Click")
    .CountEquals(1)
    .HasBinding("Multiply()", BindingKind.Command);

Unit.GetProperties("/html/body/dot:Button[3]/@Text")
    .CountEquals(1)
    .HasValue("*");
    
Unit.GetProperties("/html/body/dot:Button[4]/@Click")
    .CountEquals(1)
    .HasBinding("Divide()", BindingKind.Command);

Unit.GetProperties("/html/body/dot:Button[4]/@Text")
    .CountEquals(1)
    .HasValue("/");