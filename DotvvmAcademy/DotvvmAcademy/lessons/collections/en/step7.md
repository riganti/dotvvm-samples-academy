Adding New Task
===============
Now, we can implement the `AddTask()` method. It should add a new `TaskData` object with the `Title` property set to `AddedTaskTitle` value.

Also, we'd like to reset the `AddedTaskTitle` property, so after the task is created, assign an empty string in it.

[<sample Correct="../samples/AddingNewTask2ViewModelCorrect.cs"
         Incorrect="../samples/AddingNewTask2ViewModelIncorrect.cs"
         Validator="Lesson2Step7Validator">
    <dependencies>
        <dependency>../samples/TaskDataCorrect.cs</dependency>
    </dependencies>
    <allowedTypes>
        <allowedType><![CDATA[System.Collections.Generic.List<DotvvmAcademy.Tutorial.ViewModels.TaskData>]]></allowedType>
        <allowedType>DotvvmAcademy.Tutorial.ViewModels.TaskData</allowedType>
    </allowedTypes>
    <allowedMethods>
        <allowedMethod><![CDATA[System.Collections.Generic.List<DotvvmAcademy.Tutorial.ViewModels.TaskData>.Add]]></allowedMethod>
    </allowedMethods>
</sample>]