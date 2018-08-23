﻿#load "./constants.csx"

Unit.SetFileName("todo.dothtml");
Unit.SetDefaultCodePath("./todo_stub.dothtml");
Unit.SetCorrectCodePath("./todo_repeater.dothtml");
Unit.SetSourcePath("ToDoViewModel.cs", "./ToDoViewModel_ctor.cs");

Unit.GetDirective("/attribute::*")
    .IsViewModelDirective(ToDoViewModel);

Unit.GetControl("/child::*[1]")
    .IsOfType<RawLiteral>()
    .HasRawText("<!DOCTYPE html>", false);

var body = Unit.GetControl("/html/body");

var repeater = body.GetControl("dot:Repeater");

repeater.GetProperty("@DataSource")
    .HasBinding("Items");

var template = repeater.GetProperty("@ItemTemplate");
{
    var div = template.GetControl("div");
    var literal = div.GetControl("dot:Literal");
    literal.GetProperty("@Text")
        .HasBinding("Text");
}