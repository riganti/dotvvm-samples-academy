Data-Binding tlačítka
=====================
Teď potřebujeme propojit tlačítko s metodou `Calculate`, kterou jsme nadeklarovali ve viewmodelu v jednom z předchozích kroků.

Použijte následujicí syntaxi pro propojení příkazu (command binding): 

```DOTHTML
<dot:Button Click="{command: Calculate()}" />
```

Také vychom mohli změnit text tlačítka (např. na "Sečti!"). K tomu můžeme použít vlastnost `Text`.
Jelikož se text na tlačítku nemění v průběhu programu, nepotřebujeme ho ukládat do viewmodelu, ale
můžeme ho nadeklarovat rovnou do DotHTML kódu jako:

```DOTHTML
<dot:Button Text="Sečti!" />
```
[<DothtmlExercise Initial="samples/CalculatorView_Stage3.dothtml"
                  Final="samples/CalculatorView_Stage4.dothtml"
                  ViewModel="samples/CalculatorViewModel_Stage4.cs"
                  DisplayName="CalculatorView.dothtml"
                  ValidatorId="Lesson1Step7Validator" />]