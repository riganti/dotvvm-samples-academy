#load "20_datacontext.dothtml.csx"

using DotvvmAcademy.Validation.Dothtml.Unit;
using DotvvmAcademy.Validation.Unit;

Unit.SetDefault("ProfileDetail_20.dothtml");
Unit.SetCorrect("ProfileDetail_30.dothtml");

inner.GetProperty("@DataContext")
    .RequireBinding("Address");
