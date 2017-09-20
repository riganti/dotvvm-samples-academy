Свойства ViewModel
==================
В DotVVM, каждая страница имеет объект ViewModel. Он хранит данные из элементов управления страницы, или чего-нибудь на странице, что является динамическим и может измениться, когда пользователь взаимодействует со страницей.

ViewModel это класс C# и данные хранятся в публичных свойствах.

Объявите 3 свойства – `Number1`, `Number2` и `Result`, все они должны быть типа `int`.

[<CSharpExercise Initial="samples/CalculatorViewModel_Stage1.cs"
                 Final="samples/CalculatorViewModel_Stage2.cs"
                 DisplayName="CalculatorViewModel.cs"
                 ValidatorId="Lesson1Step4Validator" />]

> Значения, введеные пользователем будут храниться в свойствах `Number1` и `Number2`, сумму этих чисел мы поместим в свойство `Result`, когда пользователь нажмет на кнопку.
