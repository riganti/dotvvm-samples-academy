---
Title: "Log-In Form: Validation Attributes"
CodeTask: /resources/040_login_registration/10_loginform_attributes.csharp.csx
---

# Log-In Form: Validation Attributes

Greetings, traveller! If you want to learn how to use DotVVM's form validation, you've come to the right place, as we're gonna build a Log-In and Registration form.

Please tak a look at the ViewModel. You may notice its constructor has a parameter of type `AccountService`. This type is used to simulate database access. You may also notice that the ViewModel extends `DotvvmViewModelBase`. This base type allows you to hook into the ViewModel's life-cycle; here by overriding the `Init` method and initializing properties only on the first request, so that data from the client doesn't get lost.

The easiest way to validate forms in DotVVM is by using attributes from the `System.ComponentModel.DataAnnotations` namespace on the ViewModel's properties. Add `RequiredAttribute` and `EmailAddressAttribute` to the `Email` property and `RequiredAddressAttribute` to the `Password` property.