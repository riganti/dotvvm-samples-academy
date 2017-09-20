Data-Binding textových polí
===========================
Teď musíme propojit naše textová pole s vlastnostmi ve viewmodelu. Poté, co uživatel zadá hodnotu do textového pole, se hodnota
automaticky promítne do vlastnosti ve viewmodelu. Stejně tak se změní i hodnota na stránce, pokud změníme hodnotu vlastnosti ve viewmodelu.

Tento mechanismus se nazývá **data-binding**. K propojení textového pole a vlastosti ve viewmodelu používáme následující syntaxi:

```DOTHTML
<dot:TextBox Text="{value: Number1}" />
```
Propojte jednotlivá textová pole s příslušnými vlastnostmi.

[<DothtmlExercise Initial="samples/CalculatorView_Stage2.dothtml"
                  Final="samples/CalculatorView_Stage3.dothtml"
                  ViewModel="samples/CalculatorViewModel_Stage2.cs"
                  DisplayName="CalculatorView.dothtml"
                  ValidatorId="Lesson1Step6Validator"/>]