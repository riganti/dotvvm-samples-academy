---
Title: Controls
CodeTask: /resources/10_calculator/30_controls.dothtml.csx
---

# Controls

DotVVM contains quite a number of reusable components that we call Controls. For instance a `TextBox` control can be used like this:

```dothtml
<span>Account Name: </span>
<dot:TextBox Text="{value: Name}" />
```

In a View you access DotVVM controls using the `dot` prefix. Why should you use a `TextBox` over a regular `<input>` element? Because `TextBox` allows you to use bindings. In the sample above, every time the user changes the content of the `TextBox` the `Name` property of the ViewModel gets updated to the new value.

Now we let the user set the input values for a calculation. Add two `TextBox` controls into the `<body>` element. Value-bind them to the `Number1` and `Number2` properties respectively.