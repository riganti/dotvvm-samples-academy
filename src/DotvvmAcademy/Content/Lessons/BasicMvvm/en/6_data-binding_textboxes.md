Data-Binding TextBoxes
======================
Now we need to connect the textboxes with properties in the viewmodel. When the user enters a value in the textbox, it will appear in the
viewmodel property automatically. Similarly, when you change a property value in the viewmodel, this change should be propagated in the page.

This mechanism is called the **data-binding**. To bind the textbox to a property in the viewmodel, we use the following syntax:

```DOTHTML
<dot:TextBox Text="{value: Number1}" />
```
Bind the textboxes to corresponding viewmodel properties.

[<DothtmlExercise Initial="samples/CalculatorView_Stage2.dothtml"
                  Final="samples/CalculatorView_Stage3.dothtml"
                  ViewModel="samples/CalculatorViewModel_Stage2.cs"
                  DisplayName="CalculatorView.dothtml"
                  ValidatorId="Lesson1Step6Validator"/>]