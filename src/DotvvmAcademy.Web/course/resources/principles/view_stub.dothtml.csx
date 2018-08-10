#load "./constants.csx"

Unit.AddSourcePath("./CalculatorViewModel_stub.cs");
Unit.SetCorrectCodePath("./calculator_stub.dothtml");

Unit.GetDirective("/attribute::*")
    .IsViewModelDirective(CalculatorViewModel);

Unit.GetControl("/child::node()[1]")
    .IsOfType<RawLiteral>()
    .HasRawContent("<!doctype html>", false);

Unit.GetControl("/html/body");