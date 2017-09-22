Partial Validation
==================
By default, all buttons run validation on the whole viewmodel and if there is any error, the command in the viewmodel is not executed at all.

However, the viewmodel often contains some child objects and you don't want to validate everything.

In such case you can use the `Validation.Target="{value: Property}"`. You can apply this property to any element or DotVVM control.

Inside this element or control, the validation rules will be checked only on the `Validation.Target` you have specified.