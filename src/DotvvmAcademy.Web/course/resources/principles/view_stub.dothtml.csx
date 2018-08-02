#load "./constants.csx"

using DotVVM.Framework.Controls;
using DotVVM.Framework.Controls.Infrastructure;

Unit.SetViewModelPath(ViewModelStubPath);
Unit.SetCorrectCodePath(ViewStubPath);

Unit.GetDirectives("/attribute::*")
    .CountEquals(1)
    .IsViewModelDirective(ViewModelFullName);

Unit.GetControls("/child::node()")
    .CountEquals(2);

Unit.GetControls("/child::node()[1]")
    .IsOfType<RawLiteral>()
    .HasRawContent("<!doctype html>", false);

Unit.GetControls("/html")
    .CountEquals(1);

Unit.GetControls("/html/child::node()")
    .CountEquals(1);

Unit.GetControls("/html/body")
    .CountEquals(1);