Partial Validation II
=====================
You can also disable the validation completely by adding `Validation.Enabled="false"`. Again, this can be set on any element or control, and it disables the
validation on this element and all its children.

Please note that the validation is triggered by the `{command: something}` bindings.

That's why you have to apply `Validation.Enabled` or `Validation.Target` always to a button or the control with a command binding.
Turning the validation off only on the form fields doesn't do anything.