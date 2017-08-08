Completing The Task
===================
When the user clicks the `LinkButton`, we need to trigger a method in the viewmodel.

The method needs to know, on which task it has been executed. We can supply the task to the method as a parameter.

Declare the `CompleteTask` method which accepts one parameter of type `TaskData`, and sets its `IsCompleted` property to `true`.

[<sample Correct="../samples/CompleteTaskViewModelCorrect.cs"
         Incorrect="../samples/CompleteTaskViewModelIncorrect.cs"
         Validator="Lesson2Step11Validator">
    <dependencies>
        <dependency>../samples/TaskDataCorrect.cs</dependency>
    </dependecies>
    <allowedTypes>
        <allowedType>System.Collections.Generic.List<DotvvmAcademy.Tutorial.ViewModels.TaskData></allowedType>
        <allowedType>DotvvmAcademy.Tutorial.ViewModels.TaskData</allowedType>
        <allowedType></allowedType>
    </allowedTypes>
    <allowedMethods>
        <allowedMethod>System.Collections.Generic.List<DotvvmAcademy.Tutorial.ViewModels.TaskData>.Add</allowedMethod>
    </allowedMethods>
</sample>]
