#load "./constants.csx"

Unit.GetDirectives("/attribute::*")
    .CountEquals(1)
    .IsViewModelDirective(ToDoViewModel);

Unit.GetControls("/child::*[1]")
    .IsOfType<RawLiteral>()
    .HasRawContent("<!DOCTYPE html>", false);

Unit.GetControls("/body/html/dot:Repeater")
    .CountEquals(1);

Unit.GetProperties("/body/html/dot:Repeater/@DataSource")
    .CountEquals(1)
    .HasBindings("Items");

Unit.GetControls("/body/html/dot:Repeater/@ItemTemplate/div/dot:Literal")
    .CountEquals(1);

Unit.GetControls("/body/html/dot:Repeater/@ItemTemplate/div/dot:Literal/@Text")
    .CountEquals(1)
    .HasBindings("this");