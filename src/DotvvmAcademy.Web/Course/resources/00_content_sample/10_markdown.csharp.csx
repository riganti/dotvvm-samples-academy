Unit.SetDefault("./Sample_10.cs");
Unit.SetCorrect("./Sample_20.cs");
Unit.SetFileName("Sample.cs");

Unit.GetType<int>().Allow();

var sampleType = Unit.GetType("DotvvmAcademy.Course.ContentSample.Sample");
sampleType.GetProperty("Property")
    .IsOfType<int>()
    .HasAccessibility(Accessibility.Public)
    .HasGetter()
    .HasSetter();