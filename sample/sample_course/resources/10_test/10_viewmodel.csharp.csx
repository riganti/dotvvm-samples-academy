Unit.SetFileName("ViewModel.cs")
Unit.SetDefault("./ViewModel_10.cs")
Unit.SetCorrect("./ViewModel_20.cs")

Unit.GetType<string>().Allow();

Unit.GetType("ViewModel")
    .GetProperty("Text")
    .IsOfType<string>()
    .HasGetter()
    .HasSetter()
    .HasAccessibility(Accessibility.Public);