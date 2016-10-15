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

namespace DotvvmAcademy.Lessons
{
    public class Lesson1 : LessonBase
    {
        public InfoStep Step0 { get; private set; }
        public InfoStep Step1 { get; private set; }
        public DothtmlStep Step2 { get; private set; }
        public CodeStep Step3 { get; private set; }
        public CodeStep Step4 { get; private set; }
        public DothtmlStep Step5 { get; private set; }
        public DothtmlStep Step6 { get; private set; }
        public InfoStep Step7 { get; private set; }

        public Lesson1()
        {
            Step0 = new InfoStep()
            {
                StepIndex = 1,
                Title = "Prerequisities",
                Description = @"To build apps in DotVVM, you'll need to know these things:

+ **HTML and CSS** - how to write a static page, how to use CSS to define styles
+ **C# Language** - how to declare a class with properties and methods

If you don't know anything, we recommend [SoloLearn](https://www.sololearn.com/Course/). They have very nice interactive tutorials for absolute beginners."
            };

            Step1 = new InfoStep()
            {
                StepIndex = 2,
                Title = "Introduction",
                Description = @"In this lesson, we'd like to create a simple calculator. 
We'll create a web page with 3 textboxes and 1 button. 

The user can fill numbers in the first and second textbox, and when he presses the button, 
the sum of the numbers will appear in the third textbox.

<TODO: animation>"
            };

            Step2 = new DothtmlStep()
            {
                StepIndex = 3,
                Title = "Basic Controls",
                Description = @"In DotVVM, we have special controls for HTML form fields and controls. 

So instead of `<input type=""text"" />` we use `<dot:TextBox />`. 
Also, we have a `<dot:Button />` control which represents the button.

Now, try to create a page with 3 textboxes and 1 button. ",
                Description2 = @"Don't forget to end the elements with `/>`. It means that the control doesn't have the end tag.
In pure HTML, it is not necessary, but it is a good practice to make sure that all elements are closed.",
                StartupCode = @"<p>
    <!-- place first textbox here -->
    +
    <!-- place second textbox here -->
    =
    <!-- place third textbox here -->
</p>
<p>
    <!-- place the button here -->
</p>",
                FinalCode = @"<p>
    <dot:TextBox />
    +
    <dot:TextBox />
    =
    <dot:TextBox />
</p>
<p>
    <dot:Button />
</p>",
                ValidationFunction = ValidateBasicControls
            };

            Step3 = new CodeStep()
            {
                StepIndex = 4,
                Title = "ViewModel Properties",
                Description = @"In DotVVM, every page has a thing called a viewmodel. It stores data from page controls, or anything in the page that is dynamic and can change when the user interacts with the page.

The viewmodel is a C# class and the data are stored in public properties.

Declare 3 properties – `Number1`, `Number2` and `Result`, all of them should be of type `int`.",
                Description2 = @"The values entered by the user will be stored in `Number1` and `Number2` properties, and we'll put the sum in the `Result` property when the user clicks the button.",
                StartupCode = @"using System;

namespace DotvvmAcademy.Tutorial.ViewModels 
{
    public class Lesson1ViewModel 
    {
        // declare properties here
    }
}",
                FinalCode = @"using System;

namespace DotvvmAcademy.Tutorial.ViewModels 
{
    public class Lesson1ViewModel 
    {
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        public int Result { get; set; }
    }
}",
                ValidationFunction = ValidateViewModelProperties
            };

            Step4 = new CodeStep()
            {
                StepIndex = 5,
                Title = "ViewModel Command",
                Description = @"Now we need to write a method which calculates the sum of the two numbers. This method is also declared in our viewmodel.

So create a method called `Calculate` which calculates the sum of `Number1` and `Number2` and stores the result in the `Result` property.",
                Description2 = @"The method should be public and it does not return any value.",
                StartupCode = @"using System;

namespace DotvvmAcademy.Tutorial.ViewModels 
{
    public class Lesson1ViewModel 
    {
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        public int Result { get; set; }

        // declare the Calculate method here
    }
}",
                FinalCode = @"using System;

namespace DotvvmAcademy.Tutorial.ViewModels 
{
    public class Lesson1ViewModel 
    {
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        public int Result { get; set; }

        public void Calculate() 
        {
            Result = Number1 + Number2;
        }
    }
}",
                ValidationFunction = ValidateCalculateMethod
            };

            Step5 = new DothtmlStep()
            {
                StepIndex = 6,
                Title = "Data-Binding TextBoxes",
                Description = @"Now we need to connect the textboxes with properties in the viewmodel. When the user enters a value in the textbox, it will appear in the 
viewmodel property automatically. Similarly, when you change a property value in the viewmodel, this change should be propagated in the page.

This mechanism is called the **data-binding**. To bind the textbox to a property in the viewmodel, we use the following syntax:

```<dot:TextBox Text=""{value: Number1}"" />```

Bind the textboxes to corresponding viewmodel properties.",
                StartupCode = @"<p>
    <dot:TextBox />
    +
    <dot:TextBox />
    =
    <dot:TextBox />
</p>
<p>
    <dot:Button />
</p>",
                FinalCode = @"<p>
    <dot:TextBox Text=""{value: Number1}"" />
    +
    <dot:TextBox Text=""{value: Number2}"" />
    =
    <dot:TextBox Text=""{value: Result}"" />
</p>
<p>
    <dot:Button />
</p>",
                ValidationFunction = ValidateTextBoxBindings,
            };

            Step6 = new DothtmlStep()
            {
                StepIndex = 7,
                Title = "Data-Binding Button",
                Description = @"Now we want to connect the button to the Calculate method we have declared in one of the previous steps.

We use the following syntax for command bindings: `Click=""{command: Calculate()}""`.
 
Also, we'd like to change the text on the button – it should say ""Calculate!"". We can use the `Text` property of the button and because this text never changes, 
we don't have to store it in the viewmodel. We can declare it directly in DOTHTML code like this: `Text=""Calculate!""`.",
                StartupCode = @"<p>
    <dot:TextBox Text=""{value: Number1}"" />
    +
    <dot:TextBox Text=""{value: Number2}"" />
    =
    <dot:TextBox Text=""{value: Result}"" />
</p>
<p>
    <dot:Button />
</p>",
                FinalCode = @"<p>
    <dot:TextBox Text=""{value: Number1}"" />
    +
    <dot:TextBox Text=""{value: Number2}"" />
    =
    <dot:TextBox Text=""{value: Result}"" />
</p>
<p>
    <dot:Button Text=""Calculate!"" Click=""{command: Calculate()}"" />
</p>",
                ValidationFunction = ValidateButtonBinding
            };

            Step7 = new InfoStep()
            {
                StepIndex = 8,
                Title = "Congratulations!",
                Description = @"You have finished the first lesson!

You have learned how to use DotVVM controls and data-binding of properties and commands!"
            };
        }

        private void ValidateBasicControls(ResolvedTreeRoot root)
        {
            if (root.GetDescendantControls<TextBox>().Count() != 3)
            {
                throw new CodeValidationException(Lesson1Texts.ThreeTextBoxControlsError);
            }
            if (root.GetDescendantControls<Button>().Count() != 1)
            {
                throw new CodeValidationException(Lesson1Texts.OneButtonControlError);
            }
        }

        private void ValidateViewModelProperties(CSharpCompilation compilation, CSharpSyntaxTree tree, SemanticModel model, Assembly assembly)
        {
            var properties = tree.GetCompilationUnitRoot().DescendantNodes().OfType<PropertyDeclarationSyntax>()
                .Select(p => model.GetDeclaredSymbol(p))
                .ToList();

            if (properties.Count(p => p.CheckNameAndType("Number1", "int")) != 1)
            {
                throw new CodeValidationException(string.Format(Lesson1Texts.PropertyNotFound, "Number1"));
            }
            if (properties.Count(p => p.CheckNameAndType("Number2", "int")) != 1)
            {
                throw new CodeValidationException(string.Format(Lesson1Texts.PropertyNotFound, "Number2"));
            }
            if (properties.Count(p => p.CheckNameAndType("Result", "int")) != 1)
            {
                throw new CodeValidationException(string.Format(Lesson1Texts.PropertyNotFound, "Result"));
            }
        }

        private void ValidateCalculateMethod(CSharpCompilation compilation, CSharpSyntaxTree tree, SemanticModel model, Assembly assembly)
        {
            ValidateViewModelProperties(compilation, tree, model, assembly);

            var methods = tree.GetCompilationUnitRoot().DescendantNodes().OfType<MethodDeclarationSyntax>()
                .Select(m => model.GetDeclaredSymbol(m))
                .ToList();
            if (methods.Count(m => m.CheckNameAndVoid("Calculate")) != 1)
            {
                throw new CodeValidationException(Lesson1Texts.CommandNotFound);
            }

            this.ExecuteSafe(() =>
            {
                var viewModel = (dynamic)assembly.CreateInstance("DotvvmAcademy.Tutorial.ViewModels.Lesson1ViewModel");
                viewModel.Number1 = 15;
                viewModel.Number2 = 30;
                viewModel.Calculate();

                if (viewModel.Result != 45)
                {
                    throw new Exception("Invalid result!");
                }
            });
        }

        private void ValidateTextBoxBindings(ResolvedTreeRoot root)
        {
            ValidateBasicControls(root);

            var propertyBindings = root.GetDescendantControls<TextBox>()
                .Select(c => c.GetValueBindingText(TextBox.TextProperty))
                .ToList();

            if (!propertyBindings.Contains("Number1") || !propertyBindings.Contains("Number2") || !propertyBindings.Contains("Result"))
            {
                throw new CodeValidationException(Lesson1Texts.TextBoxBindingsError);
            }
        }

        private void ValidateButtonBinding(ResolvedTreeRoot root)
        {
            ValidateTextBoxBindings(root);

            var buttonClickBinding = root.GetDescendantControls<Button>()
                .Select(c => c.GetCommandBindingText(ButtonBase.ClickProperty))
                .Single();

            if (buttonClickBinding == "Calculate")
            {
                throw new CodeValidationException("You need to add empty parenthesis after the Calculate method.");
            }
            if (buttonClickBinding.Replace(" ", "") != "Calculate()")
            {
                throw new CodeValidationException("You must call the Calculate() method in the command binding!");
            }

            var buttonTextBinding = root.GetDescendantControls<Button>()
                .Select(c => c.GetValue(ButtonBase.TextProperty))
                .SingleOrDefault();
            if (buttonTextBinding == null)
            {
                throw new CodeValidationException("You must set the Text property of the Button!");
            }
        }

        public override StepBase[] GetAllSteps()
        {
            return new StepBase[] { Step0, Step1, Step2, Step3, Step4, Step5, Step6, Step7 };
        }
    }
}
