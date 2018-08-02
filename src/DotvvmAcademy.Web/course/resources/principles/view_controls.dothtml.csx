#load "./view_stub.dothtml.csx"

Unit.SetViewModelPath(ViewModelWithPropertiesPath);
Unit.SetDefaultCodePath(ViewStubPath);
Unit.SetCorrectCodePath(ViewWithControlsPath);

Unit.GetProperties("/html/body/dot:Literal/@Text")
    .CountEquals(1)
    .HasBinding("Result");

Unit.GetProperties("/html/body/dot:TextBox[1]/@Text")
    .CountEquals(1)
    .HasBinding("LeftOperand");

Unit.GetProperties("/html/body/dot:TextBox[2]/@Text")
    .CountEquals(1)
    .HasBinding("RightOperand");