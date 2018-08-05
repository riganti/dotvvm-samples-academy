# Properties

ViewModels get sent around a lot. That's why they need to be JSON-serializable. In practice that usually means 
all data in the ViewModel needs to be in public properties with a public [getter] and a public [setter].

Our calculator is going to need three of them. One for the result and two for the operands.

---

Add public properties of type `int` to our ViewModel called:

- `Result`
- `LeftOperand`
- `RightOperand`

[getter]: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/get
[setter]: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/set

[CodeTask](/resources/principles/viewmodel_properties.csharp.csx)