# Properties

ViewModels need to be JSON-serializable. In practice that usually means that all data in the ViewModel needs to be
contained in public properties with a public getter and a public setter.

Our calculator is going to need three of them. One for the result and two for the operands.

---

Add public properties of type `int` called:

- Result
- LeftOperand
- RightOperand

[CodeTask](/resources/principles/viewmodel_properties.csharp.csx)