Unit.SetDefaultCodePath("./Sample_10.cs");
Unit.SetCorrectCodePath("./Sample_20.cs");
Unit.SetFileName("Sample.cs");

var sampleType = Unit.GetType("DotvvmAcademy.Course.ContentSample.Sample");
sampleType.GetProperty("Property")
    .IsOfType<int>();