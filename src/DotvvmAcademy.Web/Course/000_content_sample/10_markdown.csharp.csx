#load "00_constants.csx"

using DotvvmAcademy.Validation.CSharp.Unit;
using DotvvmAcademy.Validation.Unit;

public CSharpUnit Unit { get; set; } = new CSharpUnit();

Unit.GetType<int>()
    .Allow();

var sampleType = Unit.GetType("Sample");
sampleType.GetProperty("Property")
    .RequireType<int>()
    .RequireGetter()
    .RequireSetter();

sampleType.GetMethod("Method")
    .RequireReturnType<int>()
    .RequireParameterless();

Unit.Run(c => {
    var vm = c.Instantiate("Sample");
    if (vm.Method() != 42)
    {
        c.Report("The method must return 42.");
    }
})

