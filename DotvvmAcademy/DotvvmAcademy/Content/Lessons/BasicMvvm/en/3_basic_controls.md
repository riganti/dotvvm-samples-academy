Basic Controls
==============
In DotVVM, we have special controls for HTML form fields and controls.

So instead of `<input type="text" />` we use `<dot:TextBox />`. Also, we have a `<dot:Button />` control which represents the button.

Now, try to create a page with 3 textboxes and 1 button. 

[<DothtmlExercise Initial="../samples/CalculatorView_Stage1.dothtml"
                  Final="../samples/CalculatorView_Stage2.dothtml"
                  DisplayName="CalculatorView.dothtml"
                  ValidatorId="Lesson1Step3Validator"/>]

> Don't forget to end the elements with ` />`. It means that the control doesn't have the end tag. In pure HTML, it is not necessary, but it is a good practice to make sure that all elements are closed.