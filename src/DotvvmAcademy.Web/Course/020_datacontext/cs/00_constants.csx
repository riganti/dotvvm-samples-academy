public const string ViewModelName = "DotvvmAcademy.Course.ProfileDetail.ProfileDetailViewModel";
public const string ProfileName = "DotvvmAcademy.Course.ProfileDetail.Profile";
public const string FirstNameProperty = "FirstName";
public const string LastNameProperty = "LastName";
public const string ProfileProperty = "Profile";
public const string CreateMethod = "Load";
public const string DeleteMethod = "Unload";

public const string InitializedEarlyDiagnosticMessage 
    = "Vlastnost Profile nesmí být inicializována dříve než je zavolána metoda Loa.";
public const string NotInitializedDiagnosticMessage 
    = "Vlastnost Profile musí být inicializována v metodě Load.";
public const string NotDeletedDiagnosticMessage 
    = "Vlastnost Profile musí být v metodě Unload nastavena na null.";