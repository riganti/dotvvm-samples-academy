#load "./view_stub.dothtml.csx"

Unit.SetViewModelPath(ViewModelWithPropertiesPath);
Unit.SetDefaultCodePath(ViewStubPath);
Unit.SetCorrectCodePath(ViewWithControlsPath);

Unit.GetProperties("/html/body/dot:Literal/@Text")
    .CountEquals(1)
    .HasBinding("Result")
    .IsOfType<int>();

Unit.GetProperties("/html/body/dot:TextBox[1]/@Text")
    .CountEquals(1)
    .HasBinding("LeftOperand")
    .IsOfType<int>();

Unit.GetProperties("/html/body/dot:TextBox[2]/@Text")
    .CountEquals(1)
    .HasBinding("RightOperand")
    .IsOfType<int>();