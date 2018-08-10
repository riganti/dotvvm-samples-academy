#load "./constants.csx"

Unit.GetDirective("/attribute::*")
    .IsViewModelDirective(ToDoViewModel);

Unit.GetControl("/child::*[1]")
    .IsOfType<RawLiteral>()
    .HasRawContent("<!DOCTYPE html>", false);

Unit.GetProperty("/body/html/dot:Repeater/@DataSource")
    .HasBinding("Items");

Unit.GetProperty("/body/html/dot:Repeater/@ItemTemplate/div/dot:Literal/@Text")
    .HasBinding("this");