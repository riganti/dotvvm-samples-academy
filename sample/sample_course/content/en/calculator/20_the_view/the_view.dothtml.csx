using DotVVM.Framework.Controls;

CorrectCode = "/resources/calculator_stub.dothtml";

GetDirectives("/attribute::*")
    .CountEquals(1)
    .IsViewModelDirective("CourseFormat.CalculatorViewModel");

GetControls("/child::node()")
    .CountEquals(2);

GetControls("/child::node()[1]")
    .IsOfType<RawLiteral>();

GetControls("/RawLiteral[1]/@EncodedText")
    .TextEquals("<!doctype html>");

GetControls("/html")
    .CountEquals(1);

GetControls("/html/child::node()")
    .CountEquals(1);

GetControls("/html/body")
    .CountEquals(1);