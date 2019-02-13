public const string ViewModelName = "DotvvmAcademy.Course.ProfileDetail.ProfileDetailViewModel";
public const string ProfileName = "DotvvmAcademy.Course.ProfileDetail.Profile";
public const string FirstNameProperty = "FirstName";
public const string LastNameProperty = "LastName";
public const string ProfileProperty = "Profile";
public const string CreateMethod = "Create";
public const string DeleteMethod = "Delete";

public const string InitializedEarlyDiagnosticMessage 
    = "The Profile property must not be initialized before the Create method is called.";
public const string NotInitializedDiagnosticMessage 
    = "The Profile property must be initialized in the Create method.";
public const string NotDeletedDiagnosticMessage 
    = "The Profile property must be set to null after the Delete method is called.";