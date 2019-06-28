public const string ViewModelName = "DotvvmAcademy.Course.ProfileDetail.ProfileDetailViewModel";
public const string ProfileName = "DotvvmAcademy.Course.ProfileDetail.Profile";
public const string FirstNameProperty = "FirstName";
public const string LastNameProperty = "LastName";
public const string ProfileProperty = "Profile";
public const string CreateMethod = "Load";
public const string DeleteMethod = "Unload";

public const string InitializedEarlyDiagnosticMessage 
    = "The Profile property must not be initialized before the Load method is called.";
public const string NotInitializedDiagnosticMessage 
    = "The Profile property must be initialized in the Load method.";
public const string NotDeletedDiagnosticMessage 
    = "The Profile property must be set to null after the Unload method is called.";