Data-Binding textových polí
===========================
Теперь нам нужно соединить текстовые поля со свойствами во ViewModel. Когда пользователь вводит значение в текстовом поле, оно появится в свойстве во
ViewModel автоматически. Кроме того, при изменении значения свойства во ViewModel, это изменение должно отобразиться на странице.

Этот механизм называется **data-binding** или привязка данных. Для того, чтобы связать текстовое поле со свойством во ViewModel, мы используем следующий синтаксис:

```DOTHTML
<dot:TextBox Text="{value: Number1}" />
```
Свяжите текстовые поля с соответствующими свойствами во ViewModel. 

[<DothtmlExercise Initial="samples/CalculatorView_Stage2.dothtml"
                  Final="samples/CalculatorView_Stage3.dothtml"
                  ViewModel="samples/CalculatorViewModel_Stage2.cs"
                  DisplayName="CalculatorView.dothtml"
                  ValidatorId="Lesson1Step6Validator"/>]