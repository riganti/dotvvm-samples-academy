Rendering the Tasks
===================
Inside the `<div>`, we'd like to display the task title. If you want to output text directly in the page,
you can use the data-binding syntax with double curly braces, like this: `{{value: Title}}`.

Alternatively, you can use the `<dot:Literal Text="{value: Title}" />` to write a text.

So, render the `Title` of the task inside the `<div>`. Also, add the `<dot:LinkButton>` inside the `<div>`. We'll use it to mark tasks as completed.

[<sample Correct="../samples/RenderTasks2Correct.dothtml"
         Incorrect="../samples/RenderTasks2Incorrect.dothtml"
         Validator="Lesson2Step9Validator" />]

> The `LinkButton` control works the same way as the `Button`, but it renders a hyperlink.
