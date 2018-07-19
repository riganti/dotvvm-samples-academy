#load "../30_properties"

DefaultCode = "/resources/CalculatorViewModel_properties.cs";
CorrectCode = "/resources/CalculatorViewModel_methods.cs";

GetMethod("System.Void SampleCourse.CalculatorViewModel::Add()")
    .CountEquals(1);

Run(c =>
{
    var vm = c.Instantiate(viewModel);
    vm.LeftOperand = 5;
    vm.RightOperand = 11;
    vm.Add();
    if (vm.Result != 16)
    {
        c.Report("Your Add method doesn't add correctly.");
    }
});

GetMethod("System.Void SampleCourse.CalculatorViewModel::Subtract()")
    .CountEquals(1);

Run(c =>
{
    var vm = c.Instantiate(viewModel);
    vm.LeftOperand = 7;
    vm.RightOperand = 4;
    vm.Subtract();
    if (vm.Result != 3)
    {
        c.Report("Your Subtract method doesn't subtract correctly.");
    }
});

GetMethod("System.Void SampleCourse.CalculatorViewModel::Multiply()")
    .CountEquals(1);

Run(c =>
{
    var vm = c.Instantiate(viewModel);
    vm.LeftOperand = 4;
    vm.RightOperand = 5;
    vm.Multiply();
    if (vm.Result != 20)
    {
        c.Report("Your Multiply method doesn't multiply correctly.");
    }
});

GetMethod("System.Void SampleCourse.CalculatorViewModel::Divide()")
    .CountEquals(1);

Run(c =>
{
    var vm = c.Instantiate(viewModel);
    vm.LeftOperand = 21;
    vm.RightOperand = 7;
    vm.Divide();
    if (vm.Result != 3)
    {
        c.Report("Your Divide method doesn't divide correctly.");
    }
});