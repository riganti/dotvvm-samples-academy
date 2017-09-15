Data-Binding Button
===================
Now we want to connect the button to the `Calculate` method we have declared in one of the previous steps.

We use the following syntax for command bindings: 

```DOTHTML
<dot:Button Click="{command: Calculate()}" />
```

Also, we'd like to change the text on the button – it should say "Calculate!". We can use the `Text` property of the button and because this text never changes,
we don't have to store it in the viewmodel. We can declare it directly in DOTHTML code like this: 

```DOTHTML
<dot:Button Text="Calculate!" />
```
[<DothtmlExercise Initial="samples/CalculatorView_Stage3.dothtml"
                  Final="samples/CalculatorView_Stage3.dothtml"
                  ViewModel="samples/CalculatorViewModel_Stage4.dothtml"
                  DisplayName="CalculatorView.dothtml"
                  ValidatorId="Lesson1Step7Validator" />]