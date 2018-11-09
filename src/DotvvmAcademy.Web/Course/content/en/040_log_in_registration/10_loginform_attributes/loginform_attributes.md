---
Title: "Log-In Form: Validation Attributes"
CodeTask: /resources/040_login_registration/10_loginform_attributes.csharp.csx
---

# Log-In Form: Validation Attributes

Greetings, traveller! If you want to learn how to use DotVVM validation by building a Log-In and Registration form, you've come to the right place.

Please look at the ViewModel. You may notice it has a constructor parameter of type `AccountService`. This type is used to simulate database access. You may also notice that it extends `DotvvmViewModelBase`. This base type allows you to hook into the ViewModel's life-cycle; here by overriding the `Init` method.

The easiest way to validate anything in DotVVM is by using attributes from the `System.ComponentModel.DataAnnotations` namespace. Add `RequiredAttribute` and `EmailAddressAttribute` to the `Email` property and `RequiredAddressAttribute` to the `Password` property.