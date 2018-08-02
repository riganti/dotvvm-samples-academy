#load "./constants.csx"

Unit.SetCorrectCodePath(ViewModelStubPath);

Unit.GetTypes(ViewModelFullName)
    .CountEquals(1)
    .IsTypeKind(CSharpTypeKind.Class)
    .HasAccessibility(CSharpAccessibility.Public);