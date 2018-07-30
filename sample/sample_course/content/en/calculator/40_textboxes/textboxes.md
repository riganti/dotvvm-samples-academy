# TextBoxes

Our View needs a mechanism that would allow the user to type in the values for `LeftOperand` and `RightOperand`
properties as well as display the `Result`

Add two `<dot:TextBox>`es to the `<body>` element (`value`-bound to the `LeftOperand` and `RightOperand` property
respectively).

To display the `Result`, use either a `<dot:Literal>` with a binding on its `Text` property, or just write 
`{{value: Result}}`, which generates the `<dot:Literal>` for you.