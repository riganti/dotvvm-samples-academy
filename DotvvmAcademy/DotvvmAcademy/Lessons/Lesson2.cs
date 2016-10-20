using DotvvmAcademy.Steps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using Microsoft.CodeAnalysis.CSharp;
using DotvvmAcademy.Steps.Validation;
using DotVVM.Framework.Controls;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Reflection;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.Parser;
using DotVVM.Framework.Compilation.Parser.Dothtml.Parser;
using DotVVM.Framework.Utils;

namespace DotvvmAcademy.Lessons
{
    public class Lesson2 : LessonBase
    {
        public InfoStep Step0 { get; private set; }
        public DothtmlStep Step1 { get; private set; }
        public CodeStep Step2 { get; private set; }
        public DothtmlStep Step3 { get; private set; }
        public CodeStep Step4 { get; private set; }
        public CodeStep Step5 { get; private set; }
        public CodeStep Step6 { get; private set; }
        public DothtmlStep Step7 { get; private set; }
        public DothtmlStep Step8 { get; private set; }
        public DothtmlStep Step9 { get; private set; }
        public CodeStep Step10 { get; private set; }
        public DothtmlStep Step11 { get; private set; }
        public DothtmlStep Step12 { get; private set; }
        public InfoStep Step13 { get; private set; }


        public Lesson2()
        {
            Step0 = new InfoStep()
            {
                StepIndex = 1,
                Title = "Objective",
                Description = @"In this lesson, we'll build a simple to-do list.

<img src=""/img/lesson2_step1.gif"" alt=""Animation"" />"
            };

            Step1 = new DothtmlStep()
            {
                StepIndex = 2,
                Title = "Adding New Task",
                Description = @"First, we should create the controls which add a new task.

Add the `TextBox` and the `Button` controls to the page. The button should say ""Add Task"".",
                StartupCode = @"<p>
    <!-- place textbox and button for adding new task here -->
</p>
<div>
    <!-- we'll display a list of tasks here later -->
</p>",
                FinalCode = @"<p>
    <dot:TextBox />
    <dot:Button Text=""Add Task"" />
</p>
<div>
    <!-- we'll display a list of tasks here later -->
</p>",
                ValidationFunction = ValidateAddTaskControls
            };

            Step2 = new CodeStep()
            {
                StepIndex = 3,
                Title = "Adding New Task",
                Description = @"Now we should add a property which will represent the title of the new task. Let's name it `AddedTaskTitle`.
Don't remember that every `TextBox` must have its property in the viewmodel, otherwise, the value entered by the user would be lost.

Also, we will need the `AddTask()` method in the viewmodel. For now, make it just empty. It should not return any value.
",
                StartupCode = @"using System;

namespace DotvvmAcademy.Tutorial.ViewModels 
{
    public class Lesson2ViewModel 
    {
        // declare the AddedTaskTitle property and the AddTask() method here
    }
}",
                FinalCode = @"using System;

namespace DotvvmAcademy.Tutorial.ViewModels 
{
    public class Lesson2ViewModel 
    {
        public string AddedTaskTitle { get; set; }

        public void AddTask() 
        {
        }
    }
}",
                ValidationFunction = ValidateAddTaskProperties
            };

            Step3 = new DothtmlStep()
            {
                StepIndex = 4,
                Title = "Adding New Task",
                Description = @"Now, we need to bind the text in the `TextBox` to the `AddedTaskTitle` property, and the `Button` to the `AddTask()` method.",
                StartupCode = @"<p>
    <dot:TextBox />
    <dot:Button Text=""Add Task"" />
</p>
<div>
    <!-- we'll display a list of tasks here later -->
</p>",
                FinalCode = @"<p>
    <dot:TextBox Text=""{value: AddedTaskTitle}"" />
    <dot:Button Text=""Add Task"" Click=""{command: AddTask()}"" />
</p>
<div>
    <!-- we'll display a list of tasks here later -->
</p>",
                ValidationFunction = ValidateAddTaskControlBindings
            };

            Step4 = new CodeStep()
            {
                StepIndex = 5,
                Title = "Representing Tasks",
                Description = @"We are ready to create a list of tasks. However, we need a class to represent the task itself.

Each task should have the `Title` property of `string`, and the `IsCompleted` property of `bool`. 

Create a class named `TaskData` and declare the two properties.",
                Description2 = @"In Visual Studio, this class would be declared in separate file.",
                StartupCode = @"using System;

namespace DotvvmAcademy.Tutorial.ViewModels 
{
    // declare the class here
}",
                FinalCode = @"using System;

namespace DotvvmAcademy.Tutorial.ViewModels 
{
    public class TaskData
    {
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}",
                ValidationFunction = ValidateTaskDataClass
            };

            Step5 = new CodeStep()
            {
                StepIndex = 6,
                Title = "Representing Tasks",
                Description = @"Now let's go back to our viewmodel. We need to add a list of `TaskData` objects in the viewmodel,
so we can render it in the page.

Add the `Tasks` property to the viewmodel. Its type should be `List<TaskData>` and it should be initialized
to `new List<TaskData>()`.",
                StartupCode = @"using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Tutorial.ViewModels 
{
    public class Lesson2ViewModel 
    {
        public string AddedTaskTitle { get; set; }

        // declare the Tasks property here

        public void AddTask() 
        {
        }
    }
}",
                FinalCode = @"using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Tutorial.ViewModels 
{
    public class Lesson2ViewModel 
    {
        public string AddedTaskTitle { get; set; }

        public List<TaskData> Tasks { get; set; } = new List<TaskData>();

        public void AddTask() 
        {
        }
    }
}",
                ValidationFunction = ValidateTasksProperty
            };

            Step6 = new CodeStep()
            {
                StepIndex = 7,
                Title = "Adding new Task",
                Description = @"Now, we can implement the `AddTask()` method. It should add a new `TaskData` object with the `Title` property set to `AddedTaskTitle` value.

Also, we'd like to reset the `AddedTaskTitle` property, so after the task is created, assign an empty string in it.",
                StartupCode = @"using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Tutorial.ViewModels 
{
    public class Lesson2ViewModel 
    {
        public string AddedTaskTitle { get; set; }

        public List<TaskData> Tasks { get; set; } = new List<TaskData>();

        public void AddTask() 
        {
            // add the new task with the title set to AddedTaskTitle here
            
            // reset the AddedTaskTitle to an empty string
        }
    }
}",
                FinalCode = @"using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Tutorial.ViewModels 
{
    public class Lesson2ViewModel 
    {
        public string AddedTaskTitle { get; set; }

        public List<TaskData> Tasks { get; set; } = new List<TaskData>();

        public void AddTask() 
        {
            Tasks.Add(new TaskData() { Title = AddedTaskTitle });
            AddedTaskTitle = """";
        }
    }
}",
                ValidationFunction = ValidateAddTaskMethod
            };

            Step7 = new DothtmlStep()
            {
                StepIndex = 8,
                Title = "Render The Tasks",
                Description = @"We are ready to render a list of tasks right now. For each task, we'd like to render this HTML snippet:

```
<div class=""task"">
    ...
</div>
```

To do this, we'll use the `<dot:Repeater>` control. Add it to the page, bind its `DataSource` property to the `Tasks` property in the viewmodel,
and inside the `<dot:Repeater>`, place the `<div class=""task""></div>` element. It will repeat the `div` for each object in the collection.",
                StartupCode = @"<p>
    <dot:TextBox Text=""{value: AddedTaskTitle}"" />
    <dot:Button Text=""Add Task"" Click=""{command: AddTask()}"" />
</p>
<div>
    <!-- place the Repeater control here -->
</p>",
                FinalCode = @"<p>
    <dot:TextBox Text=""{value: AddedTaskTitle}"" />
    <dot:Button Text=""Add Task"" Click=""{command: AddTask()}"" />
</p>
<div>
    <dot:Repeater DataSource=""{value: Tasks}"">
        <div class=""task""></div>
    </dot:Repeater>
</p>",
                ValidationFunction = ValidateRepeaterControl
            };

            Step8 = new DothtmlStep()
            {
                StepIndex = 9,
                Title = "Rendering The Tasks",
                Description = @"Inside the `<div>`, we'd like to display the task title. If you want to output text directly in the page,
you can use the data-binding syntax with double curly braces, like this: `{{value: Title}}`.

Alternatively, you can use the `<dot:Literal Text=""{value: Title}"" />` to write a text.

So, render the `Title` of the task inside the `<div>`. Also, add the `<dot:LinkButton>` inside the `<div>`. We'll use it to mark tasks as completed.",
                Description2 = @"The `LinkButton` control works the same way as the `Button`, but it renders a hyperlink.",
                StartupCode = @"
<p>
    <dot:TextBox Text=""{value: AddedTaskTitle}"" />
    <dot:Button Text=""Add Task"" Click=""{command: AddTask()}"" />
</p>
<div>
    <dot:Repeater DataSource=""{value: Tasks}"">
        <div class=""task"">
            <!-- render task title and LinkButton here -->
        </div>
    </dot:Repeater>
</p>",
                FinalCode = @"
<p>
    <dot:TextBox Text=""{value: AddedTaskTitle}"" />
    <dot:Button Text=""Add Task"" Click=""{command: AddTask()}"" />
</p>
<div>
    <dot:Repeater DataSource=""{value: Tasks}"">
        <div class=""task"">
            {{value: Title}}
            <dot:LinkButton />
        </div>
    </dot:Repeater>
</p>",
                ValidationFunction = ValidateRepeaterTemplate1
            };

            Step9 = new DothtmlStep()
            {
                StepIndex = 10,
                Title = "Hiding The LinkButton",
                Description = @"The LinkButton should be visible only for tasks which are not completed.

In DotVVM, there is the `Visible` property which can show or hide content. 

Bind the `Visible` property to the `LinkButton` to the `IsCompleted` property of the task. Use the `!` operator to negate the value of `IsCompleted`.",
                Description2 = @"Please note that the `Visible` property can be used also on any HTML element.",
                StartupCode = @"
<p>
    <dot:TextBox Text=""{value: AddedTaskTitle}"" />
    <dot:Button Text=""Add Task"" Click=""{command: AddTask()}"" />
</p>
<div>
    <dot:Repeater DataSource=""{value: Tasks}"">
        <div class=""task"">
            {{value: Title}}
            <dot:LinkButton />
        </div>
    </dot:Repeater>
</p>",
                FinalCode = @"
<p>
    <dot:TextBox Text=""{value: AddedTaskTitle}"" />
    <dot:Button Text=""Add Task"" Click=""{command: AddTask()}"" />
</p>
<div>
    <dot:Repeater DataSource=""{value: Tasks}"">
        <div class=""task"">
            {{value: Title}}
            <dot:LinkButton Visible=""{value: !IsCompleted}"" />
        </div>
    </dot:Repeater>
</p>",
                ValidationFunction = ValidateRepeaterTemplate2
            };

            Step10 = new CodeStep()
            {
                StepIndex = 11,
                Title = "Completing The Task",
                Description = @"When the user clicks the `LinkButton`, we need to trigger a method in the viewmodel.

The method needs to know, on which task it has been executed. We can supply it the task as a parameter.

Declare the `CompleteTask` method which accepts one parameter of type `TaskData` and sets it `IsCompleted` property to `true`.",
                StartupCode = @"using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Tutorial.ViewModels 
{
    public class Lesson2ViewModel 
    {
        public string AddedTaskTitle { get; set; }

        public List<TaskData> Tasks { get; set; } = new List<TaskData>();

        public void AddTask() 
        {
            Tasks.Add(new TaskData() { Title = AddedTaskTitle });
            AddedTaskTitle = """";
        }

        // place the CompleteTask method here
    }
}",
                FinalCode = @"using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Tutorial.ViewModels 
{
    public class Lesson2ViewModel 
    {
        public string AddedTaskTitle { get; set; }

        public List<TaskData> Tasks { get; set; } = new List<TaskData>();

        public void AddTask() 
        {
            Tasks.Add(new TaskData() { Title = AddedTaskTitle });
            AddedTaskTitle = """";
        }

        public void CompleteTask(TaskData task) 
        {
            task.IsCompleted = true;
        }
    }
}",
                ValidationFunction = ValidateCompleteTaskMethod
            };

            Step11 = new DothtmlStep()
            {
                StepIndex = 12,
                Title = "Completing The Task",
                Description = @"Now we need to set the `Click` property of the link button to the `CompleteTask` method.

Please note that all bindings inside `Repeater` are not evaluated on viewmodel, but on the corresponding `TaskData` object.
But the `CompleteTask` method is in the parent scope - we have declared it in the viewmodel. 

You can use the `_parent.CompleteTask` to reference the method from the parent scope.
When supplying arguments to the method, you begin also in the context of `TaskData`, so you can use `_this` to pass the whole
`TaskData` object to the method.",
                StartupCode = @"<p>
    <dot:TextBox Text=""{value: AddedTaskTitle}"" />
    <dot:Button Text=""Add Task"" Click=""{command: AddTask()}"" />
</p>
<div>
    <dot:Repeater DataSource=""{value: Tasks}"">
        <div class=""task"">
            {{value: Title}}
            <dot:LinkButton Visible=""{value: !IsCompleted}"" />
        </div>
    </dot:Repeater>
</p>",
                FinalCode = @"<p>
    <dot:TextBox Text=""{value: AddedTaskTitle}"" />
    <dot:Button Text=""Add Task"" Click=""{command: AddTask()}"" />
</p>
<div>
    <dot:Repeater DataSource=""{value: Tasks}"">
        <div class=""task"">
            {{value: Title}}
            <dot:LinkButton Visible=""{value: !IsCompleted}""
                 Click=""{command: _parent.CompleteTask(_this)}"" />
        </div>
    </dot:Repeater>
</p>",
                ValidationFunction = ValidateRepeaterTemplate3
            };

            Step12 = new DothtmlStep()
            {
                StepIndex = 13,
                Title = "Completing The Task",
                Description = @"The last thing we want to do, is to strike through tasks which are completed.

If the task is completed, we'd like to render it as `<div class=""task-completed""></div>. 

We need to use data-binding to specify the `class` property of the `<div>`. 

You will need to use the `expression ? truePart : falsePart` operator to do it.
Also note that you can use single quotes (apostrophes) instead of double quotes to use strings in data-binidngs.",
                StartupCode = @"<p>
    <dot:TextBox Text=""{value: AddedTaskTitle}"" />
    <dot:Button Text=""Add Task"" Click=""{command: AddTask()}"" />
</p>
<div>
    <dot:Repeater DataSource=""{value: Tasks}"">
        <div class=""task"">
            {{value: Title}}
            <dot:LinkButton Visible=""{value: !IsCompleted}""
                 Click=""{command: _parent.CompleteTask(_this)}"" />
        </div>
    </dot:Repeater>
</p>",
                FinalCode = @"<p>
    <dot:TextBox Text=""{value: AddedTaskTitle}"" />
    <dot:Button Text=""Add Task"" Click=""{command: AddTask()}"" />
</p>
<div>
    <dot:Repeater DataSource=""{value: Tasks}"">
        <div class=""{value: IsCompleted ? 'task-completed' : 'task'}"">
            {{value: Title}}
            <dot:LinkButton Visible=""{value: !IsCompleted}""
                 Click=""{command: _parent.CompleteTask(_this)}"" />
        </div>
    </dot:Repeater>
</p>",
                ValidationFunction = ValidateRepeaterTemplate4
            };

            Step13 = new InfoStep()
            {
                StepIndex = 14,
                Title = "Congratulations!",
                Description = @"You have finished the second lesson!

You have learned how to use the `Repeater` control and collections in the viewmodel!"
            };
        }


        private void ValidateAddTaskControls(ResolvedTreeRoot root)
        {
            if (root.GetDescendantControls<TextBox>().Count() != 1)
            {
                throw new CodeValidationException(Lesson2Texts.AddTaskTextBoxControlError);
            }
            if (root.GetDescendantControls<Button>().Count() != 1)
            {
                throw new CodeValidationException(Lesson2Texts.AddTaskButtonControlError);
            }

            var buttonTextBinding = root.GetDescendantControls<Button>()
                .Select(c => c.GetValue(ButtonBase.TextProperty))
                .SingleOrDefault();
            if (buttonTextBinding == null)
            {
                throw new CodeValidationException(Lesson2Texts.ButtonDoesNotHaveText);
            }
        }

        private void ValidateAddTaskProperties(CSharpCompilation compilation, CSharpSyntaxTree tree, SemanticModel model, Assembly assembly)
        {
            var properties = tree.GetCompilationUnitRoot().DescendantNodes().OfType<PropertyDeclarationSyntax>()
                .Select(p => model.GetDeclaredSymbol(p))
                .ToList();

            if (properties.Count(p => p.CheckNameAndType("AddedTaskTitle", "string")) != 1)
            {
                throw new CodeValidationException(string.Format(GenericTexts.PropertyNotFound, "AddedTaskTitle"));
            }

            var methods = tree.GetCompilationUnitRoot().DescendantNodes().OfType<MethodDeclarationSyntax>()
                .Select(p => model.GetDeclaredSymbol(p))
                .ToList();

            if (methods.Count(m => m.CheckNameAndVoid("AddTask")) != 1)
            {
                throw new CodeValidationException(string.Format(GenericTexts.MethodNotFound, "Calculate"));
            }
        }

        private void ValidateAddTaskControlBindings(ResolvedTreeRoot root)
        {
            ValidateAddTaskControls(root);

            var propertyBindings = root.GetDescendantControls<TextBox>()
                .Select(c => c.GetValueBindingText(TextBox.TextProperty))
                .ToList();
            if (!propertyBindings.Contains("AddedTaskTitle"))
            {
                throw new CodeValidationException(Lesson2Texts.TextBoxBindingError);
            }

            root.GetDescendantControls<Button>().Single()
                .ValidateCommandBindingExpression(ButtonBase.ClickProperty, "AddTask()");
        }

        private void ValidateTaskDataClass(CSharpCompilation compilation, CSharpSyntaxTree tree, SemanticModel model, Assembly assembly)
        {
            var classDeclarations = tree.GetCompilationUnitRoot().DescendantNodes().OfType<ClassDeclarationSyntax>()
                .Select(c => model.GetDeclaredSymbol(c))
                .ToList();
            if (classDeclarations.Count(c => c.Name == "TaskData") != 1)
            {
                throw new CodeValidationException(string.Format(GenericTexts.ClassNotFound, "TaskData"));
            }

            var properties = tree.GetCompilationUnitRoot().DescendantNodes().OfType<PropertyDeclarationSyntax>()
                .Select(p => model.GetDeclaredSymbol(p))
                .ToList();
            if (properties.Count(p => p.CheckNameAndType("Title", "string")) != 1)
            {
                throw new CodeValidationException(string.Format(GenericTexts.PropertyNotFound, "Title"));
            }
            if (properties.Count(p => p.CheckNameAndType("IsCompleted", "bool")) != 1)
            {
                throw new CodeValidationException(string.Format(GenericTexts.PropertyNotFound, "IsCompleted"));
            }
        }

        private void ValidateTasksProperty(CSharpCompilation compilation, CSharpSyntaxTree tree, SemanticModel model, Assembly assembly)
        {
            ValidateAddTaskProperties(compilation, tree, model, assembly);

            var properties = tree.GetCompilationUnitRoot().DescendantNodes().OfType<PropertyDeclarationSyntax>()
                .Select(p => model.GetDeclaredSymbol(p))
                .ToList();
            if (properties.Count(p => p.CheckNameAndType("Tasks", "System.Collections.Generic.List<DotvvmAcademy.Tutorial.ViewModels.TaskData>")) != 1)
            {
                throw new CodeValidationException(string.Format(GenericTexts.PropertyNotFound, "Tasks"));
            }
        }

        private void ValidateAddTaskMethod(CSharpCompilation compilation, CSharpSyntaxTree tree, SemanticModel model, Assembly assembly)
        {
            ValidateTasksProperty(compilation, tree, model, assembly);

            this.ExecuteSafe(() =>
            {
                var viewModel = (dynamic)assembly.CreateInstance("DotvvmAcademy.Tutorial.ViewModels.Lesson2ViewModel");
                viewModel.AddedTaskTitle = "test";
                viewModel.AddTask();

                if (viewModel.Tasks.Count != 1)
                {
                    throw new CodeValidationException("The AddTask() method should add one task!");
                }
                if (viewModel.Tasks[0].Title != "test")
                {
                    throw new CodeValidationException("When creating a task, use the AddedTaskTitle as a title of the task!");
                }
                if (viewModel.AddedTaskTitle != "")
                {
                    throw new CodeValidationException("You need to reset the AddedTaskTitle property to an empty string after you create a task!");
                }
            });
        }

        private void ValidateRepeaterControl(ResolvedTreeRoot root)
        {
            ValidateAddTaskControlBindings(root);

            if (root.GetDescendantControls<Repeater>().Count() != 1)
            {
                throw new CodeValidationException(Lesson2Texts.RepeaterControlError);
            }

            var repeater = root.GetDescendantControls<Repeater>().Single();
            if (repeater.GetValueBindingText(ItemsControl.DataSourceProperty) != "Tasks")
            {
                throw new CodeValidationException(Lesson2Texts.RepeaterBindingError);
            }

            IAbstractPropertySetter setter;
            if (!repeater.TryGetProperty(Repeater.ItemTemplateProperty, out setter)
                || !(setter is ResolvedPropertyTemplate))
            {
                throw new CodeValidationException(Lesson2Texts.RepeaterTemplateMissingDivError);
            }
            var template = (ResolvedPropertyTemplate) setter;

            var div = template.GetDescendantControls<HtmlGenericControl>().ToList();
            if (div.Count(d => d.DothtmlNode.As<DothtmlElementNode>()?.TagName == "div") != 1)
            {
                throw new CodeValidationException(Lesson2Texts.RepeaterTemplateMissingDivError);
            }
        }

        private void ValidateRepeaterTemplate1(ResolvedTreeRoot root)
        {
            ValidateRepeaterControl(root);

            var template = root.GetDescendantControls<Repeater>().Single();

            var literals = template.GetDescendantControls<Literal>()
                .Select(l => l.GetValueBindingOrNull(Literal.TextProperty))
                .Where(l => l != null)
                .ToList();
            if (literals.Count(b => b.Binding.Value == "Title") != 1)
            {
                throw new CodeValidationException(Lesson2Texts.TitleBindingNotFound);
            }

            var linkButtons = template.GetDescendantControls<LinkButton>().ToList();
            if (linkButtons.Count != 1)
            {
                throw new CodeValidationException(Lesson2Texts.LinkButtonNotFound);
            }
        }

        private void ValidateRepeaterTemplate2(ResolvedTreeRoot root)
        {
            ValidateRepeaterTemplate1(root);

            var linkButton = root.GetDescendantControls<Repeater>()
                .Single()
                .GetDescendantControls<LinkButton>()
                .Single();
            if (linkButton.GetValueBindingText(HtmlGenericControl.VisibleProperty) != "!IsCompleted")
            {
                throw new CodeValidationException(Lesson2Texts.InvalidVisibleBinding);
            }
        }

        private void ValidateCompleteTaskMethod(CSharpCompilation compilation, CSharpSyntaxTree tree, SemanticModel model, Assembly assembly)
        {
            ValidateAddTaskMethod(compilation, tree, model, assembly);

            this.ExecuteSafe(() =>
            {
                var viewModel = (dynamic)assembly.CreateInstance("DotvvmAcademy.Tutorial.ViewModels.Lesson2ViewModel");
                var task = (dynamic)assembly.CreateInstance("DotvvmAcademy.Tutorial.ViewModels.TaskData");
                task.Title = "New Task";
                task.IsCompleted = false;
                viewModel.CompleteTask(task);

                if (!task.IsCompleted)
                {
                    throw new CodeValidationException("The CompleteTask() method should set the IsCompleted to true!");
                }
            });
        }

        private void ValidateRepeaterTemplate3(ResolvedTreeRoot root)
        {
            ValidateRepeaterTemplate2(root);

            var linkButton = root.GetDescendantControls<Repeater>()
                .Single()
                .GetDescendantControls<LinkButton>()
                .Single();
            linkButton.ValidateCommandBindingExpression(ButtonBase.ClickProperty, "_parent.CompleteTask(_this)");
        }

        private void ValidateRepeaterTemplate4(ResolvedTreeRoot root)
        {
            ValidateRepeaterTemplate3(root);

            var divs = root.GetDescendantControls<Repeater>()
                .Single()
                .GetDescendantControls<HtmlGenericControl>()
                .ToList();
            var div = divs.First(d => d.DothtmlNode.As<DothtmlElementNode>()?.TagName == "div");

            var classProperties = div.Properties.OfType<ResolvedPropertyBinding>()
                .Where(p => p.Property.Name == "class")
                .ToList();
            if (classProperties.Count != 1)
            {
                throw new CodeValidationException(Lesson2Texts.InvalidClassBinding);
            }
            var classBinding = classProperties[0].Binding.Value.Replace(" ", "").Replace("\"", "'");
            if (classBinding != "IsCompleted?'task-completed':'task'"
                && classBinding != "IsCompleted==true?'task-completed':'task'"
                && classBinding != "!IsCompleted?'task':'task-completed'"
                && classBinding != "IsCompleted==false?'task':'task-completed'")
            {
                throw new CodeValidationException(Lesson2Texts.InvalidClassBinding);
            }
        }
        

        public override StepBase[] GetAllSteps()
        {
            return new StepBase[] { Step0, Step1, Step2, Step3, Step4, Step5, Step6, Step7, Step8, Step9, Step10, Step11, Step12, Step13 };
        }
    }
}
