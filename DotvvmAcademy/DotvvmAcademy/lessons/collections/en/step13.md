Completing The Task
===================
The last thing we want to do, is to strike through tasks which are completed.

If the task is completed, we'd like to render it as `<div class="task-completed"></div>`.

We need to use data-binding to specify the `class` property of the `<div>`.

You will need to use the `expression ? truePart : falsePart` operator to do it.
Also note that you can use single quotes (apostrophes) instead of double quotes to use strings in data-bindings.

[<sample Correct="CompleteTask2Correct.dothtml"
         Incorrect="CompleteTaskCorrect.dothtml"
         Validator="Lesson2Step13Validator">]