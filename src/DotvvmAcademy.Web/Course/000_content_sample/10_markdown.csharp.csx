#load "00_constants.csx"

using DotvvmAcademy.Validation.CSharp.Unit;
using DotvvmAcademy.Validation.Unit;

public CSharpUnit Unit { get; set; } = new CSharpUnit();

Unit.SetDefault("Sample_10.cs");
Unit.SetCorrect("Sample_20.cs");

