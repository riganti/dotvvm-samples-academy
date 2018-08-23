#load "./view_stub.dothtml.csx"

Unit.SetDefaultCodePath("./calculator_directive.dothtml");
Unit.SetCorrectCodePath("./calculator_controls.dothtml");
Unit.SetSourcePath("CalculatorViewModel.cs", "./CalculatorViewModel_properties.cs");

body.GetControl("div/dot:Literal")
    .GetProperty("@Text")
    .HasBinding("Result");

body.GetControl("dot:TextBox[1]")
    .GetProperty("@Text")
    .HasBinding("LeftOperand");

body.GetControl("dot:TextBox[2]")
    .GetProperty("@Text")
    .HasBinding("RightOperand");