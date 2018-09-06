#load "./00_constants.csx"

Unit.SetFileName("CalculatorViewModel.cs");
Unit.SetDefault("./CalculatorViewModel_10.cs");
Unit.SetCorrect("./CalculatorViewModel_20.cs");

Unit.GetType<int>()
    .Allow();

var viewModelType = Unit.GetType(CalculatorViewModel);
viewModelType.GetProperty("Result")
    .IsOfType<int>()
    .HasGetter()
    .HasSetter()
    .Allow();

viewModelType.GetProperty("Number1")
    .IsOfType<int>()
    .HasGetter()
    .HasSetter()
    .Allow();

viewModelType.GetProperty("Number2")
    .IsOfType<int>()
    .HasGetter()
    .HasSetter()
    .Allow();