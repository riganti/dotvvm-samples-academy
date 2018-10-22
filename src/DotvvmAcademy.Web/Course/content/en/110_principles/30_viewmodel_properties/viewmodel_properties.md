---
Title: ViewModel Properties
CodeTask: /resources/principles/viewmodel_properties.csharp.csx
---

# ViewModel Properties

ViewModels get send around a lot. That's why they need to be [JSON]-serializable. Properties in the ViewModel need to be public and have a public [getter] and [setter] in order to be serialized and deserialized without errors.

---

## Your Task

Add properties to our `CalculatorViewModel` that:

- Are of type `int`
- Have public [getter] and [setter]
- Are called:
  - `Result`,
  - `LeftOperand`, and
  - `RightOperand`

[json]: https://json.org/
[getter]: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/get
[setter]: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/set
