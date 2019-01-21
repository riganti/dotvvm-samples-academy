using DotvvmAcademy.Validation.Unit;
using DotvvmAcademy.Validation.CSharp;
using DotvvmAcademy.Validation.CSharp.Unit;

public CSharpUnit Unit { get; set; } = new CSharpUnit();

Unit.SetDefault("Sample_10.cs");
Unit.SetCorrect("Sample_20.cs");

Unit.GetType<int>()
    .Allow();
var sampleType = Unit.GetType("DotvvmAcademy.Course.ContentSample.Sample");
sampleType.GetProperty("Property")
    .RequireType<int>()
    .RequireAccess(AllowedAccess.Public)
    .RequireGetter()
    .RequireSetter();