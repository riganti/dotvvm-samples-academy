using DotVVM.Framework.Controls;
using DotVVM.Framework.Controls.Infrastructure;

Unit.SetViewModelPath("./CalculatorViewModel_stub.cs");
Unit.SetCorrectCodePath("./calculator_stub.dothtml");

Unit.GetDirectives("/attribute::*")
    .CountEquals(1)
    .IsViewModelDirective("SampleCourse.CalculatorViewModel");

Unit.GetControls("/child::node()")
    .CountEquals(2);

Unit.GetControls("/child::node()[1]")
    .IsOfType<RawLiteral>()
    .HasRawContent("<!doctype html>");

Unit.GetControls("/html")
    .CountEquals(1);

Unit.GetControls("/html/child::node()")
    .CountEquals(1);

Unit.GetControls("/html/body")
    .CountEquals(1);