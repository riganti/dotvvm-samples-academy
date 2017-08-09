Representing Tasks
==================
Now let's go back to our viewmodel. We need to add a list of `TaskData` objects in the viewmodel,
so we can render it in the page.

Add the `Tasks` property to the viewmodel. Its type should be `List<TaskData>` and it should be initialized
to `new List<TaskData>()`.

[<sample Correct="../samples/RepresentingTasksViewModelCorrect.cs"
         Incorrect="../samples/RepresentingTasksViewModelIncorrect.cs"
         Validator="Lesson2Step6Validator">
    <dependencies>
        <dependency>../samples/TaskDataCorrect.cs</dependency>
    </dependencies>
    <allowedTypes>
        <allowedType><![CDATA[System.Collections.Generic.List<DotvvmAcademy.Tutorial.ViewModels.TaskData>]]></allowedType>
    </allowedTypes>
</sample>]