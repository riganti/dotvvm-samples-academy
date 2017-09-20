Привязка Button
===============
Теперь мы хотим подключить кнопку к методу Calculate, который мы объявили в одном из предыдущих шагов.

Для этого, мы используем следующий синтаксис:

```DOTHTML
<dot:Button Click="{command: Calculate()}" />
```

Кроме того, мы хотели бы изменить текст на кнопке на "Calculate!". Мы можем использовать свойство кнопки `Text`, и потому, что этот текст никогда не меняется,
мы не должны хранить его в ViewModel. Мы можем объявить его непосредственно в DOTHTML:

```DOTHTML
<dot:Button Text="Calculate!" />
```
[<DothtmlExercise Initial="samples/CalculatorView_Stage3.dothtml"
                  Final="samples/CalculatorView_Stage4.dothtml"
                  ViewModel="samples/CalculatorViewModel_Stage4.cs"
                  DisplayName="CalculatorView.dothtml"
                  ValidatorId="Lesson1Step7Validator" />]