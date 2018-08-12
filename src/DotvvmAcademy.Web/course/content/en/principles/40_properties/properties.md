# Properties

ViewModels get send around a lot. That's why they need to be [JSON]-serializable. Properties in the ViewModel need to be public and have a public [getter] and [setter] in order to be serialized and deserialized without errors.

---

## Your Task

Add public properties of type `int` to our ViewModel called:

- `Result`
- `LeftOperand`
- `RightOperand`

[json]: https://json.org/
[getter]: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/get
[setter]: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/set

[CodeTask](/resources/principles/viewmodel_properties.csharp.csx)