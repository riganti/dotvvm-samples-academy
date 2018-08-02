#load "./viewmodel_stub.csharp.csx"

Unit.SetDefaultCodePath(ViewModelStubPath);
Unit.SetCorrectCodePath(ViewModelWithPropertiesPath);

Unit.GetTypes("System.Int32")
    .Allow();

Unit.GetProperties($"{ViewModelFullName}::Result")
    .CountEquals(1)
    .HasAccessibility(CSharpAccessibility.Public)
    .Allow();

Unit.GetProperties($"{ViewModelFullName}::LeftOperand")
    .CountEquals(1)
    .HasAccessibility(CSharpAccessibility.Public)
    .Allow();

Unit.GetProperties($"{ViewModelFullName}::RightOperand")
    .CountEquals(1)
    .HasAccessibility(CSharpAccessibility.Public)
    .Allow();